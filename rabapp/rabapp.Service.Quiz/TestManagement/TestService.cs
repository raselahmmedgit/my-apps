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

namespace rabapp.Service.Quiz.TestManagement
{
    public class TestService : BaseService<TestViewModel>, ITestService
    {
        private readonly ITestRepository _iTestRepository;
        private readonly AppDbContext _dbContext;
        private readonly IDocumentInformationRepository _iDocumentInformationRepository;
        private readonly ITestWiseQuestionRepository _iTestWiseQuestionRepository;

        public TestService(IBaseRepository<Test> iBaseRepository,
            ITestRepository iTestRepository,
            DbContext dbContext,
            IDocumentInformationRepository iDocumentInformationRepository,
            ITestWiseQuestionRepository iTestWiseQuestionRepository)
            : base(iBaseRepository, dbContext)
        {
            _iTestRepository = iTestRepository;
            _dbContext = dbContext;
            _iDocumentInformationRepository = iDocumentInformationRepository;
            _iTestWiseQuestionRepository = iTestWiseQuestionRepository;
        }

        public Message InsertOrUpdateWithoutIdentity(Test test, HttpPostedFileBase httpPostedFileBase)
        {
            Message message;
            try
            {
                using (var scope = new TransactionScope())
                {
                    _dbContext.SqlConnection.Open();
                    if (test.NoOfQuestion != test.TestWiseQuestions.Count())
                    {
                        return SetMessage.SetErrorMessage("No of question not match! Please select (" + test.NoOfQuestion + ")question from question list");
                    }
                    var isExist = _iTestRepository.Get(test);
                    var affectedRow = 0;
                    var guid = Guid.NewGuid();
                    var file = httpPostedFileBase;
                    var target = new MemoryStream();
                    var documentInformatio = new DocumentInformation();

                    if (isExist == null)
                    {
                        test.IsActive = true;
                        affectedRow = _iTestRepository.InsertWithoutIdentity(test);
                        foreach (var testWiseQuestions in test.TestWiseQuestions)
                        {
                            testWiseQuestions.TestId = test.TestId;
                            _iTestWiseQuestionRepository.InsertWithoutIdentity(testWiseQuestions);
                        }

                        message = affectedRow > 0
                            ? SetMessage.SetSuccessMessage("Information has been saved successfully.")
                            : SetMessage.SetInformationMessage("No data has been saved.");
                    }
                    else
                    {
                        affectedRow = _iTestRepository.Update(test);

                        _iTestWiseQuestionRepository.DeleteByTestId(test.TestId);

                        foreach (var testWiseQuestions in test.TestWiseQuestions)
                        {
                            testWiseQuestions.TestId = test.TestId;
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
                    var imageUtility = new ImageUtility();

                    #region Insert Document Information

                    if (httpPostedFileBase != null)
                    {
                        var size = imageUtility.GetPreferredImageSize(Constants.ImageDimensions.Common);
                        byte[] byteAfterResize = imageUtility.EnforceResize(size.Width, size.Height, byteData, guid + Path.GetExtension(httpPostedFileBase.FileName));

                        documentInformatio.DocumentName = guid + Path.GetExtension(httpPostedFileBase.FileName);
                        documentInformatio.DocumentByte = byteAfterResize;
                        documentInformatio.DocumentSize = byteAfterResize.Length;
                        _iDocumentInformationRepository.InsertWithoutIdentity(documentInformatio);

                        if (documentInformatio.GlobalId > 0)
                        {
                            test.TestIconName = (httpPostedFileBase != null ? guid + Path.GetExtension(httpPostedFileBase.FileName) : null);
                            test.GlobalId = documentInformatio.GlobalId > 0 ? documentInformatio.GlobalId : 0;
                            affectedRow = _iTestRepository.Update(test);
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
                _dbContext.SqlConnection.Close();
            }
            return message;
        }

        public IEnumerable<Test> Search(string keyword, int currentPage = 0, int take = 50)
        {
            try
            {
                _dbContext.SqlConnection.Open();

                int currentLoggedInUserId = WebHelper.CurrentSession.Content.LoggedInUser.UserId;
                int currentLoggedInUserType = WebHelper.CurrentSession.Content.LoggedInUser.UserType;

                if (currentLoggedInUserType == (int)Constants.UserType.Administrator)
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
                _dbContext.SqlConnection.Close();
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

                    _dbContext.SqlConnection.Open();

                    if (testId > 0)
                    {
                        var test = _iTestRepository.GetById(testId);
                        test.IsPublished = status;
                        affectedRow = _iTestRepository.Update(test);
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
                _dbContext.SqlConnection.Close();
            }
            return message;
        }
        public IEnumerable<Test> GetTestIconForHomePage(int currentPage = 0, int take = 18)
        {
            try
            {
                _dbContext.SqlConnection.Open();

                return _iTestRepository.GetTestIconForHomePage(currentPage, take);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                _dbContext.SqlConnection.Close();
            }
        }
        public IEnumerable<Test> GetTestByTestCategoryId(int testCategoryId, int currentPage = 0, int take = 15)
        {
            try
            {
                _dbContext.SqlConnection.Open();

                return _iTestRepository.GetTestByTestCategoryId(testCategoryId, currentPage, take);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                _dbContext.SqlConnection.Close();
            }
        }
        public Test GetById(int testId)
        {
            try
            {
                _dbContext.SqlConnection.Open();

                var test = _iTestRepository.GetById(testId);
                if (test != null)
                {
                    var testWiseQuestion = _iTestWiseQuestionRepository.GetAllByTestId(test.TestId);
                    if (testWiseQuestion != null)
                    {
                        test.TestWiseQuestions = testWiseQuestion;
                    }
                }
                return test;

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                _dbContext.SqlConnection.Close();
            }
        }
    }

    public interface ITestService : IBaseService<Test>
    {
        Message InsertOrUpdateWithoutIdentity(Test test, HttpPostedFileBase httpPostedFileBase);
        IEnumerable<Test> Search(string keyword, int currentPage = 0, int take = 50);
        Message ChangeTestStatus(int testId, bool status);
        IEnumerable<Test> GetTestIconForHomePage(int currentPage = 0, int take = 18);
        IEnumerable<Test> GetTestByTestCategoryId(int testCategoryId, int currentPage = 0, int take = 15);
        Test GetById(int testId);

    }
}
