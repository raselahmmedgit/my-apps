using System;
using System.Collections.Generic;
using System.IO;
using SoftwareGrid.Model.iTestApp.QuestionManagement;
using SoftwareGrid.Model.iTestApp.Utility;
using SoftwareGrid.Repository.iTestApp.Base;
using SoftwareGrid.Repository.iTestApp.DocumentManagement;
using SoftwareGrid.Repository.iTestApp.QuestionManagement;
using SoftwareGrid.Service.iTestApp.Base;
using System.Web;
using System.Transactions;
using SoftwareGrid.Service.iTestApp.Helper;
using SoftwareGrid.Model.iTestApp.DocumentManagement;

namespace SoftwareGrid.Service.iTestApp.QuestionManagement
{
    public class QuestionService: BaseService<Question>, IQuestionService
    {
        private readonly IQuestionRepository _iQuestionRepository;
        private readonly IQuestionAnswerOptionRepository _iQuestionAnswerOptionRepository;
        private readonly AppDbContext _dbContext;
        private readonly IDocumentInformationRepository _iDocumentInformationRepository;

        public QuestionService(
              IBaseRepository<Question> iBaseRepository
            , IQuestionRepository iQuestionRepository
            , IQuestionAnswerOptionRepository iQuestionAnswerOptionRepository
            , IDocumentInformationRepository iDocumentInformationRepository
            , AppDbContext dbContext)
            : base(iBaseRepository, dbContext)
        {
            _iQuestionRepository = iQuestionRepository;
            _iQuestionAnswerOptionRepository = iQuestionAnswerOptionRepository;
            _dbContext = dbContext;
            _iDocumentInformationRepository = iDocumentInformationRepository;
        }
        public IEnumerable<Question> Search(string keyword, int currentPage = 0, int take = 50)
        {
            try
            {
                _dbContext.SqlConnection.Open();

                int currentLoggedInUserId = WebHelper.CurrentSession.Content.LoggedInUser.UserId;
                int currentLoggedInUserType = WebHelper.CurrentSession.Content.LoggedInUser.UserType;

                if (currentLoggedInUserType == (int)AppUsers.Admin)
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
                _dbContext.SqlConnection.Close();
            }
        }

        public IEnumerable<Question> SearchByTestId(int testId, string keyword, int currentPage = 0, int take = 50,bool mappedQuestionOnly = false, bool withAnswerOption = false, bool withCorrectAnswerOption = false)
        {
            try
            {
                _dbContext.SqlConnection.Open();

               return  _iQuestionRepository.SearchByTestId(testId, keyword, currentPage, take, mappedQuestionOnly,withAnswerOption,withAnswerOption);
               
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

        public Question GetById(int questionId)
        {
            try
            {
                _dbContext.SqlConnection.Open();

                return _iQuestionRepository.GetById(questionId);
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

        public Message InsertOrUpdateWithoutIdentity(Question question, HttpPostedFileBase httpPostedFileBase)
        {
            Message message;
            try
            {
                using (var scope = new TransactionScope())
                {

                    _dbContext.SqlConnection.Open();

                    var isExistQuestion = _iQuestionRepository.Get(question);
                    var affectedRow = 0;
                    var guid = Guid.NewGuid();
                    
                    if (isExistQuestion == null)
                    {
                        affectedRow = _iQuestionRepository.InsertWithoutIdentity(question);

                        #region Question Answer Option - Insert

                        foreach (var answerOption in question.QuestionAnswerOptionList)
                        {
                            answerOption.QuestionId = question.QuestionId;
                            _iQuestionAnswerOptionRepository.InsertWithoutIdentity(answerOption);
                        }

                        #endregion

                        message = affectedRow > 0
                            ? SetMessage.SetSuccessMessage("Information has been saved successfully.")
                            : SetMessage.SetInformationMessage("No data has been saved.");
                    }
                    else
                    {
                        affectedRow = _iQuestionRepository.Update(question);

                        #region Question Answer Option - Update

                        foreach (var answerOption in question.QuestionAnswerOptionList)
                        {
                            answerOption.QuestionId = question.QuestionId;
                            _iQuestionAnswerOptionRepository.Update(answerOption);
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
                        var documentInformatio = new DocumentInformation();

                        file.InputStream.CopyTo(target);

                        byte[] byteData = target.ToArray();
                        ImageUtility imageUtility = new ImageUtility();

                        if (isExistQuestion == null)
                        {
                            #region Insert Document Information

                            var size = imageUtility.GetPreferredImageSize(ImageDimensions.Common);
                            byte[] byteAfterResize = imageUtility.EnforceResize(size.Width, size.Height, byteData, guid + Path.GetExtension(httpPostedFileBase.FileName));

                            documentInformatio.DocumentName = guid + Path.GetExtension(httpPostedFileBase.FileName);
                            documentInformatio.DocumentByte = byteAfterResize;
                            documentInformatio.DocumentSize = byteAfterResize.Length;
                            _iDocumentInformationRepository.InsertWithoutIdentity(documentInformatio);

                            #region Question - GlobalId Update

                            question.QuestionImageName = (httpPostedFileBase != null ? guid + Path.GetExtension(httpPostedFileBase.FileName) : null);
                            question.GlobalId = documentInformatio.GlobalId > 0 ? documentInformatio.GlobalId : 0;
                            affectedRow = _iQuestionRepository.Update(question);

                            #endregion

                            #endregion
                        }
                        else
                        {
                            #region Update Document Information

                            var isExistDocumentInformatio = _iDocumentInformationRepository.Get(new DocumentInformation { GlobalId = isExistQuestion.GlobalId });

                            if (isExistDocumentInformatio != null)
                            {
                                var size = imageUtility.GetPreferredImageSize(ImageDimensions.Common);
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
                            bool isUpload = imageUtility.ImageSaveToPath(folderPath, byteData, guid + Path.GetExtension(httpPostedFileBase.FileName), ImageDimensions.Common);
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
                _dbContext.SqlConnection.Close();
            }
            return message;
        }
        
    }

    public interface IQuestionService : IBaseService<Question>
    {
        IEnumerable<Question> Search(string keyword, int currentPage = 0, int take = 50);
        IEnumerable<Question> SearchByTestId(int testId, string keyword, int currentPage = 0, int take = 50, bool mappedQuestionOnly = false, bool withAnswerOption = false, bool withCorrectAnswerOption = false);

        Message InsertOrUpdateWithoutIdentity(Question question, HttpPostedFileBase httpPostedFileBase);

        Question GetById(int questionId);
    }
}
