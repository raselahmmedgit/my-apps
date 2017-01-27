using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using rabapp.Model.Common;
using rabapp.Model.Quiz.ViewModels;
using rabapp.Repository.Common;
using rabapp.Repository.Quiz.DocumentManagement;
using rabapp.Repository.Quiz.QuestionManagement;
using rabapp.Service.Common;
using rabapp.Service.Common.Helper;

namespace rabapp.Service.Quiz.QuestionManagement
{
    public class QuestionService : BaseService<QuestionViewModel>, IQuestionService
    {
        private readonly IQuestionRepository _iQuestionRepository;
        private readonly IQuestionAnswerOptionRepository _iQuestionAnswerOptionRepository;
        private readonly AppDbContext _appDbContext;
        private readonly IDocumentInformationRepository _iDocumentInformationRepository;

        public QuestionService(
              IBaseRepository<QuestionViewModel> iBaseRepository
            , IQuestionRepository iQuestionRepository
            , IQuestionAnswerOptionRepository iQuestionAnswerOptionRepository
            , IDocumentInformationRepository iDocumentInformationRepository
            , AppDbContext appDbContext)
            : base(iBaseRepository, appDbContext)
        {
            _iQuestionRepository = iQuestionRepository;
            _iQuestionAnswerOptionRepository = iQuestionAnswerOptionRepository;
            _appDbContext = appDbContext;
            _iDocumentInformationRepository = iDocumentInformationRepository;
        }
        public IEnumerable<QuestionViewModel> Search(string keyword, int currentPage = 0, int take = 50)
        {
            try
            {
                _appDbContext.SqlConnection.Open();

                int currentLoggedInUserId = WebHelper.CurrentSession.Content.LoggedInUser.UserId;

                bool currentLoggedInUserIsAdministrator = false;

                if (currentLoggedInUserIsAdministrator)
                {
                    return _iQuestionRepository.Search(0, keyword, currentPage, take);
                }
                else
                {
                    return _iQuestionRepository.Search(currentLoggedInUserId, keyword, currentPage, take);
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

        public IEnumerable<QuestionViewModel> SearchByTestId(int testId, string keyword, int currentPage = 0, int take = 50, bool mappedQuestionOnly = false, bool withAnswerOption = false, bool withCorrectAnswerOption = false)
        {
            try
            {
                _appDbContext.SqlConnection.Open();

                return _iQuestionRepository.SearchByTestId(testId, keyword, currentPage, take, mappedQuestionOnly, withAnswerOption, withAnswerOption);

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

        public QuestionViewModel GetById(int questionId)
        {
            try
            {
                _appDbContext.SqlConnection.Open();

                return _iQuestionRepository.GetById(questionId);
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

        public Message InsertOrUpdateWithoutIdentity(QuestionViewModel questionViewModel, HttpPostedFileBase httpPostedFileBase)
        {
            Message message;
            try
            {
                using (var scope = new TransactionScope())
                {

                    _appDbContext.SqlConnection.Open();

                    var isExistQuestion = _iQuestionRepository.Get(questionViewModel);
                    var affectedRow = 0;
                    var guid = Guid.NewGuid();

                    if (isExistQuestion == null)
                    {
                        affectedRow = _iQuestionRepository.InsertWithoutIdentity(questionViewModel);

                        #region Question Answer Option - Insert

                        foreach (var questionAnswerOptionViewModel in questionViewModel.QuestionAnswerOptionViewModelList)
                        {
                            questionAnswerOptionViewModel.QuestionId = questionViewModel.QuestionId;
                            _iQuestionAnswerOptionRepository.InsertWithoutIdentity(questionAnswerOptionViewModel);
                        }

                        #endregion

                        message = affectedRow > 0
                            ? SetMessage.SetSuccessMessage("Information has been saved successfully.")
                            : SetMessage.SetInformationMessage("No data has been saved.");
                    }
                    else
                    {
                        affectedRow = _iQuestionRepository.Update(questionViewModel);

                        #region Question Answer Option - Update

                        foreach (var questionAnswerOptionViewModel in questionViewModel.QuestionAnswerOptionViewModelList)
                        {
                            questionAnswerOptionViewModel.QuestionId = questionViewModel.QuestionId;
                            _iQuestionAnswerOptionRepository.Update(questionAnswerOptionViewModel);
                        }

                        #endregion

                        message = affectedRow > 0
                           ? SetMessage.SetSuccessMessage("Information has been updated successfully.")
                           : SetMessage.SetInformationMessage("No data has been updated.");
                    }

                    #region Question Answer Image

                    if (httpPostedFileBase != null)
                    {
                        var file = httpPostedFileBase;
                        var target = new MemoryStream();
                        var documentInformationViewModel = new DocumentInformationViewModel();

                        file.InputStream.CopyTo(target);

                        byte[] byteData = target.ToArray();
                        var imageUtility = new ImageUtility();

                        if (isExistQuestion == null)
                        {
                            #region Insert Document Information

                            var size = imageUtility.GetPreferredImageSize(Constants.ImageDimensions.Common);
                            byte[] byteAfterResize = imageUtility.EnforceResize(size.Width, size.Height, byteData, guid + Path.GetExtension(httpPostedFileBase.FileName));

                            documentInformationViewModel.DocumentName = guid + Path.GetExtension(httpPostedFileBase.FileName);
                            documentInformationViewModel.DocumentByte = byteAfterResize;
                            documentInformationViewModel.DocumentSize = byteAfterResize.Length;
                            _iDocumentInformationRepository.InsertWithoutIdentity(documentInformationViewModel);

                            #region Question - GlobalId Update

                            questionViewModel.QuestionImageName = (httpPostedFileBase != null ? guid + Path.GetExtension(httpPostedFileBase.FileName) : null);
                            questionViewModel.GlobalId = documentInformationViewModel.GlobalId > 0 ? documentInformationViewModel.GlobalId : 0;
                            affectedRow = _iQuestionRepository.Update(questionViewModel);

                            #endregion

                            #endregion
                        }
                        else
                        {
                            #region Update Document Information

                            var isExistDocumentInformatio = _iDocumentInformationRepository.Get(new DocumentInformationViewModel { GlobalId = isExistQuestion.GlobalId });

                            if (isExistDocumentInformatio != null)
                            {
                                var size = imageUtility.GetPreferredImageSize(Constants.ImageDimensions.Common);
                                byte[] byteAfterResize = imageUtility.EnforceResize(size.Width, size.Height, byteData, guid + Path.GetExtension(httpPostedFileBase.FileName));

                                isExistDocumentInformatio.DocumentName = guid + Path.GetExtension(httpPostedFileBase.FileName);
                                isExistDocumentInformatio.DocumentByte = byteAfterResize;
                                isExistDocumentInformatio.DocumentSize = byteAfterResize.Length;
                                _iDocumentInformationRepository.Update(isExistDocumentInformatio);

                            }

                            #endregion
                        }

                        #region Image Resize and Save To Path

                        string folderPath = HttpContext.Current.Server.MapPath(Constants.ImagePath.ImageFolderPath);

                        if (httpPostedFileBase != null)
                        {
                            bool isUpload = imageUtility.ImageSaveToPath(folderPath, byteData, guid + Path.GetExtension(httpPostedFileBase.FileName), Constants.ImageDimensions.Common);
                        }

                        #endregion

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

    }

    public interface IQuestionService : IBaseService<QuestionViewModel>
    {
        IEnumerable<QuestionViewModel> Search(string keyword, int currentPage = 0, int take = 50);
        IEnumerable<QuestionViewModel> SearchByTestId(int testId, string keyword, int currentPage = 0, int take = 50, bool mappedQuestionOnly = false, bool withAnswerOption = false, bool withCorrectAnswerOption = false);
        Message InsertOrUpdateWithoutIdentity(QuestionViewModel questionViewModel, HttpPostedFileBase httpPostedFileBase);
        QuestionViewModel GetById(int questionId);

    }
}
