using System;
using SoftwareGrid.Model.iTestApp.SecurityManagement;
using SoftwareGrid.Repository.iTestApp.Base;
using SoftwareGrid.Repository.iTestApp.DocumentManagement;
using SoftwareGrid.Repository.iTestApp.UserManagement;
using SoftwareGrid.Service.iTestApp.Base;
using SoftwareGrid.Model.iTestApp.Utility;
using System.Transactions;
using System.Web;
using System.IO;
using SoftwareGrid.Model.iTestApp.DocumentManagement;
using SoftwareGrid.Service.iTestApp.Helper;
using System.Collections.Generic;

namespace SoftwareGrid.Service.iTestApp.UserManagement
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository _iUserRepository;
        private readonly DbContext _dbContext;
        private readonly IDocumentInformationRepository _iDocumentInformationRepository;

        public UserService(IBaseRepository<User> iBaseRepository
            , IUserRepository iUserRepository
            , DbContext dbContext
            , IDocumentInformationRepository iDocumentInformationRepository)
            : base(iBaseRepository, dbContext)
        {
            _iUserRepository = iUserRepository;
            _dbContext = dbContext;
            _iDocumentInformationRepository = iDocumentInformationRepository;
        }

        public IEnumerable<User> Search(string keyword, int currentPage = 0, int take = 50)
        {
            try
            {
                _dbContext.SqlConnection.Open();

                return _iUserRepository.Search(keyword, currentPage, take);
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

        public dynamic DynamicSearch(string keyword, int currentPage = 0, int take = 50)
        {
            try
            {
                _dbContext.SqlConnection.Open();

                return _iUserRepository.DynamicSearch(keyword, currentPage, take);
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

        public User GetUser(string userName)
        {
            try
            {
                _dbContext.SqlConnection.Open();
                return _iUserRepository.GetUser(userName);
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

        public User GetUser(string userName, string password)
        {
            try
            {
                _dbContext.SqlConnection.Open();
                var encPass = CryptographyHelper.Encrypt(password);
                return _iUserRepository.GetUser(userName, encPass);
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

        public Message Register(User user, HttpPostedFileBase httpPostedFileBase)
        {
            Message message;
            try
            {
                using (var scope = new TransactionScope())
                {

                    _dbContext.SqlConnection.Open();

                    var isExist = _iUserRepository.GetUser(user.Email);
                    var affectedRow = 0;
                    Guid guid;
                    guid = Guid.NewGuid();
                    HttpPostedFileBase file = httpPostedFileBase;
                    var target = new MemoryStream();
                    var documentInformatio = new DocumentInformation();
                    int loggedInUserId = WebHelper.CurrentSession.Content.LoggedInUser != null ? WebHelper.CurrentSession.Content.LoggedInUser.UserId : 0;
                    if (isExist == null)
                    {
                        //User password encryption
                        user.Password = CryptographyHelper.Encrypt(user.Password);

                        //By default user will be active
                        user.IsActive = true;

                        // Registration time globalId 0
                        user.GlobalId = 0;

                        if (loggedInUserId > 0)
                        {
                            user.CreatedByUserId = loggedInUserId;
                            user.UserType = user.UserType;
                        }
                        else
                        {
                            user.UserType = Convert.ToInt32(Constants.UserType.PublicUser);
                        }


                        affectedRow = _iUserRepository.InsertWithoutIdentity(user);

                        message = affectedRow > 0
                            ? SetMessage.SetSuccessMessage("Information has been saved successfully.")
                            : SetMessage.SetInformationMessage("No data has been saved.");
                    }
                    else
                    {
                        user.IsActive = true;
                        user.UserId = isExist.UserId;
                        user.Password = isExist.Password;
                        affectedRow = _iUserRepository.Update(user);

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
                            user.PhotoFileName = (httpPostedFileBase != null ? guid + Path.GetExtension(httpPostedFileBase.FileName) : null);
                            user.GlobalId = documentInformatio.GlobalId > 0 ? documentInformatio.GlobalId : 0;
                            affectedRow = _iUserRepository.Update(user);
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

        public Message UpdateUserInformation(User user, HttpPostedFileBase httpPostedFileBase, bool updateImageOnly = false)
        {
            Message message;
            try
            {
                using (var scope = new TransactionScope())
                {

                    _dbContext.SqlConnection.Open();

                    var isExist = _iUserRepository.Get(new User { UserId = user.UserId });
                    var affectedRow = 0;
                    Guid guid;
                    guid = Guid.NewGuid();
                    HttpPostedFileBase file = httpPostedFileBase;
                    var target = new MemoryStream();
                    var documentInformatio = new DocumentInformation();

                    if (isExist == null)
                    {
                        return SetMessage.SetInformationMessage("User information not found.");

                    }
                    if (!updateImageOnly)
                    {
                        isExist.FirstName = user.FirstName;
                        isExist.LastName = user.LastName;
                        isExist.MobileNo = user.MobileNo;
                        affectedRow = _iUserRepository.Update(isExist);
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
                            if (updateImageOnly)
                            {
                                isExist.PhotoFileName = (guid + Path.GetExtension(httpPostedFileBase.FileName));
                                isExist.GlobalId = documentInformatio.GlobalId > 0 ? documentInformatio.GlobalId : 0;
                                affectedRow = _iUserRepository.Update(isExist);
                            }
                            else
                            {
                                user.PhotoFileName = (guid + Path.GetExtension(httpPostedFileBase.FileName));
                                user.GlobalId = documentInformatio.GlobalId > 0 ? documentInformatio.GlobalId : 0;
                                affectedRow = _iUserRepository.Update(user);
                            }

                        }

                        string folderPath = HttpContext.Current.Server.MapPath(Constants.ImagePath.ImageFolderPath);

                        bool isUpload = imageUtility.ImageSaveToPath(folderPath, byteData, guid + Path.GetExtension(httpPostedFileBase.FileName), Constants.ImageDimensions.Common);

                    }
                    #endregion

                    message = SetMessage.SetSuccessMessage("Information has been updated successfully.");

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

        public Message ChangePassword(User user)
        {
            Message message;
            try
            {
                using (var scope = new TransactionScope())
                {

                    _dbContext.SqlConnection.Open();

                    var isExist = _iUserRepository.Get(new User { UserId = user.UserId });
                    var affectedRow = 0;

                    if (isExist == null)
                    {
                        return SetMessage.SetInformationMessage("User information not found.");

                    }

                    var currentPassword = CryptographyHelper.Decrypt(isExist.Password);
                    if (currentPassword != user.CurrentPassword)
                    {
                        return SetMessage.SetInformationMessage("Invalid current password.");
                    }

                    isExist.Password = CryptographyHelper.Decrypt(user.Password);
                    
                    affectedRow = _iUserRepository.Update(isExist);
                    message = affectedRow > 0
                  ? SetMessage.SetSuccessMessage("Information has been updated successfully.")
                  : SetMessage.SetInformationMessage("No data has been updated.");


                    message = SetMessage.SetSuccessMessage("Information has been updated successfully.");

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

    public interface IUserService : IBaseService<User>
    {
        IEnumerable<User> Search(string keyword, int currentPage = 0, int take = 50);

        dynamic DynamicSearch(string keyword, int currentPage = 0, int take = 50);
        User GetUser(string userName);
        User GetUser(string userName, string password);
        Message Register(User user, HttpPostedFileBase httpPostedFileBase);
        Message UpdateUserInformation(User user, HttpPostedFileBase httpPostedFileBase, bool updateImageOnly = false);
        Message ChangePassword(User user);
    }
}
