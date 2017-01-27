using System.IO;
using System.Transactions;
using System.Web;
using rabapp.Model.Common;
using rabapp.Model.Quiz.TestManagement;
using rabapp.Model.Quiz.ViewModels;
using rabapp.Repository.Common;
using rabapp.Repository.Quiz.DocumentManagement;
using rabapp.Repository.Quiz.TestManagement;
using rabapp.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rabapp.Service.Common.Helper;

namespace rabapp.Service.Quiz.TestManagement
{
    public class TestService : BaseService<TestViewModel>, ITestService
    {
        private readonly ITestRepository _iTestRepository;
        private readonly AppDbContext _appDbContext;
        private readonly IDocumentInformationRepository _iDocumentInformationRepository;
        private readonly ITestWiseQuestionRepository _iTestWiseQuestionRepository;

        public TestService(IBaseRepository<TestViewModel> iBaseRepository,
            ITestRepository iTestRepository,
            AppDbContext dbContext,
            IDocumentInformationRepository iDocumentInformationRepository,
            ITestWiseQuestionRepository iTestWiseQuestionRepository)
            : base(iBaseRepository, dbContext)
        {
            _iTestRepository = iTestRepository;
            _appDbContext = dbContext;
            _iDocumentInformationRepository = iDocumentInformationRepository;
            _iTestWiseQuestionRepository = iTestWiseQuestionRepository;
        }

        public Message InsertOrUpdateWithoutIdentity(TestViewModel testViewModel, HttpPostedFileBase httpPostedFileBase)
        {
            Message message;
            try
            {
                using (var scope = new TransactionScope())
                {
                    _appDbContext.SqlConnection.Open();
                    if (testViewModel.NoOfQuestion != testViewModel.TestWiseQuestionViewModelList.Count())
                    {
                        return SetMessage.SetErrorMessage("No of question not match! Please select (" + testViewModel.NoOfQuestion + ")question from question list");
                    }
                    var isExist = _iTestRepository.Get(testViewModel);
                    var affectedRow = 0;
                    var guid = Guid.NewGuid();
                    var file = httpPostedFileBase;
                    var target = new MemoryStream();
                    var documentInformationViewMode = new DocumentInformationViewModel();

                    if (isExist == null)
                    {
                        testViewModel.IsActive = true;
                        affectedRow = _iTestRepository.InsertWithoutIdentity(testViewModel);
                        foreach (var testWiseQuestions in testViewModel.TestWiseQuestionViewModelList)
                        {
                            testWiseQuestions.TestId = testViewModel.TestId;
                            _iTestWiseQuestionRepository.InsertWithoutIdentity(testWiseQuestions);
                        }

                        message = affectedRow > 0
                            ? SetMessage.SetSuccessMessage("Information has been saved successfully.")
                            : SetMessage.SetInformationMessage("No data has been saved.");
                    }
                    else
                    {
                        affectedRow = _iTestRepository.Update(testViewModel);

                        _iTestWiseQuestionRepository.DeleteByTestId(testViewModel.TestId);

                        foreach (var testWiseQuestions in testViewModel.TestWiseQuestionViewModelList)
                        {
                            testWiseQuestions.TestId = testViewModel.TestId;
                            _iTestWiseQuestionRepository.InsertWithoutIdentity(testWiseQuestions);
                        }
                        message = affectedRow > 0
                           ? SetMessage.SetSuccessMessage("Information has been updated successfully.")
                           : SetMessage.SetInformationMessage("No data has been updated.");
                    }

                    if (httpPostedFileBase != null)
                    {
                        file.InputStream.CopyTo(target);
                    }

                    byte[] byteData = target.ToArray();
                    var imageUtility = new rabapp.Service.Common.Helper.ImageUtility();

                    #region Insert Document Information

                    if (httpPostedFileBase != null)
                    {
                        var size = imageUtility.GetPreferredImageSize(Constants.ImageDimensions.Common);
                        byte[] byteAfterResize = imageUtility.EnforceResize(size.Width, size.Height, byteData, guid + Path.GetExtension(httpPostedFileBase.FileName));

                        documentInformationViewMode.DocumentName = guid + Path.GetExtension(httpPostedFileBase.FileName);
                        documentInformationViewMode.DocumentByte = byteAfterResize;
                        documentInformationViewMode.DocumentSize = byteAfterResize.Length;
                        _iDocumentInformationRepository.InsertWithoutIdentity(documentInformationViewMode);

                        if (documentInformationViewMode.GlobalId > 0)
                        {
                            testViewModel.TestIconName = (httpPostedFileBase != null ? guid + Path.GetExtension(httpPostedFileBase.FileName) : null);
                            testViewModel.GlobalId = documentInformationViewMode.GlobalId > 0 ? documentInformationViewMode.GlobalId : 0;
                            affectedRow = _iTestRepository.Update(testViewModel);
                        }
                    }
                    #endregion

                    #region Image Resize and Save To Path

                    string folderPath = HttpContext.Current.Server.MapPath(Constants.ImagePath.ImageFolderPath);

                    if (httpPostedFileBase != null)
                    {
                        bool isUpload = imageUtility.ImageSaveToPath(folderPath, byteData, guid + Path.GetExtension(httpPostedFileBase.FileName), Constants.ImageDimensions.Common);
                    }

                    #endregion


                    scope.Complete();
                }


            }
            catch (Exception exception)
            {
                return SetMessage.SetErrorMessage(exception.Message);
            }
            finally
            {
                _appDbContext.SqlConnection.Close();
            }
            return message;
        }

        public IEnumerable<TestViewModel> Search(string keyword, int currentPage = 0, int take = 50)
        {
            try
            {
                _appDbContext.SqlConnection.Open();

                int currentLoggedInUserId = WebHelper.CurrentSession.Content.LoggedInUser.UserId;
                bool currentLoggedInUserIsAdministrator = false;

                if (currentLoggedInUserIsAdministrator)
                {
                    return _iTestRepository.Search(0, keyword, currentPage, take);
                }
                else
                {
                    return _iTestRepository.Search(currentLoggedInUserId, keyword, currentPage, take);
                }

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                _appDbContext.SqlConnection.Close();
            }
        }
        public Message ChangeTestStatus(int testId, bool status)
        {
            Message message = null;
            var affectedRow = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    _appDbContext.SqlConnection.Open();

                    if (testId > 0)
                    {
                        var testViewModel = _iTestRepository.GetById(testId);
                        testViewModel.IsPublished = status;
                        affectedRow = _iTestRepository.Update(testViewModel);
                        message = affectedRow > 0
                                   ? SetMessage.SetSuccessMessage("Information has been updated successfully.")
                                   : SetMessage.SetInformationMessage("No data has been updated.");
                    }
                    scope.Complete();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                _appDbContext.SqlConnection.Close();
            }
            return message;
        }
        public IEnumerable<TestViewModel> GetTestIconForHomePage(int currentPage = 0, int take = 18)
        {
            try
            {
                _appDbContext.SqlConnection.Open();

                return _iTestRepository.GetTestIconForHomePage(currentPage, take);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                _appDbContext.SqlConnection.Close();
            }
        }
        public IEnumerable<TestViewModel> GetTestByTestCategoryId(int testCategoryId, int currentPage = 0, int take = 15)
        {
            try
            {
                _appDbContext.SqlConnection.Open();

                return _iTestRepository.GetTestByTestCategoryId(testCategoryId, currentPage, take);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                _appDbContext.SqlConnection.Close();
            }
        }

        public IEnumerable<TestViewModel> GetTestTakenByUserId(int userId, int currentPage = 0, int take = 15)
        {
            try
            {
                _appDbContext.SqlConnection.Open();

                return _iTestRepository.GetTestTakenByUserId(userId, currentPage, take);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                _appDbContext.SqlConnection.Close();
            }
        }

        public IEnumerable<TestViewModel> GetFavoriteTestByUserId(int userId, int currentPage = 0, int take = 15)
        {
            try
            {
                _appDbContext.SqlConnection.Open();

                return _iTestRepository.GetFavoriteTestByUserId(userId, currentPage, take);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                _appDbContext.SqlConnection.Close();
            }
        }

        public IEnumerable<TestViewModel> GetTestWithUserByTestCategoryId(int userId, int testCategoryId, int currentPage = 0, int take = 15)
        {
            try
            {
                _appDbContext.SqlConnection.Open();

                return _iTestRepository.GetTestWithUserByTestCategoryId(userId, testCategoryId, currentPage, take);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                _appDbContext.SqlConnection.Close();
            }
        }

        public TestViewModel GetById(int testId)
        {
            try
            {
                _appDbContext.SqlConnection.Open();

                var testViewModel = _iTestRepository.GetById(testId);
                if (testViewModel != null)
                {
                    var testWiseQuestion = _iTestWiseQuestionRepository.GetAllByTestId(testViewModel.TestId);
                    if (testWiseQuestion != null)
                    {
                        testViewModel.TestWiseQuestionViewModelList = testWiseQuestion;
                    }
                }
                return testViewModel;

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                _appDbContext.SqlConnection.Close();
            }
        }
    }

    public interface ITestService : IBaseService<TestViewModel>
    {
        Message InsertOrUpdateWithoutIdentity(TestViewModel testViewModel, HttpPostedFileBase httpPostedFileBase);
        IEnumerable<TestViewModel> Search(string keyword, int currentPage = 0, int take = 50);
        Message ChangeTestStatus(int testId, bool status);
        IEnumerable<TestViewModel> GetTestIconForHomePage(int currentPage = 0, int take = 18);
        IEnumerable<TestViewModel> GetTestByTestCategoryId(int testCategoryId, int currentPage = 0, int take = 15);
        IEnumerable<TestViewModel> GetTestTakenByUserId(int userId, int currentPage = 0, int take = 15);
        IEnumerable<TestViewModel> GetFavoriteTestByUserId(int userId, int currentPage = 0, int take = 15);
        IEnumerable<TestViewModel> GetTestWithUserByTestCategoryId(int userId, int testCategoryId, int currentPage = 0,
            int take = 15);
        TestViewModel GetById(int testId);

    }
}
