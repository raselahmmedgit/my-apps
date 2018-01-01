﻿using System;
using SoftwareGrid.Model.iTestApp.SecurityManagement;
using SoftwareGrid.Model.iTestApp.Utility;
using SoftwareGrid.Repository.iTestApp.Base;
using SoftwareGrid.Repository.iTestApp.UserManagement;
using SoftwareGrid.Service.iTestApp.Base;

namespace SoftwareGrid.Service.iTestApp.UserManagement
{
    public class UserLoginInformationService : BaseService<UserLoginInformation>, IUserLoginInformationService
    {
        private readonly IUserLoginInformationRepoitory _iUserLoginInformationRepoitory;
        private readonly DbContext _dbContext;

        public UserLoginInformationService(IBaseRepository<UserLoginInformation> iBaseRepository, IUserLoginInformationRepoitory iUserLoginInformationRepoitory, DbContext dbContext)
            : base(iBaseRepository, dbContext)
        {
            _iUserLoginInformationRepoitory = iUserLoginInformationRepoitory;
            _dbContext = dbContext;
        }

        public Message UpdateUserLogoutTime(int userId)
        {
            Message message;
            try
            {
                _dbContext.SqlConnection.Open();
               var affectedRow= _iUserLoginInformationRepoitory.UpdateUserLogoutTime(userId);
                message = affectedRow > 0
                       ? SetMessage.SetSuccessMessage("Information has been saved successfully.")
                       : SetMessage.SetInformationMessage("No data has been saved.");
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

    public interface IUserLoginInformationService : IBaseService<UserLoginInformation>
    {
        Message UpdateUserLogoutTime(int userId);
    }

}
