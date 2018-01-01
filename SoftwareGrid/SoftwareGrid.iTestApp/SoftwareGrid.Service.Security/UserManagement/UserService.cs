using System;
using EasySoft.Helper;
using SoftwareGrid.Model.Security.UserManagement;
using SoftwareGrid.Repository.Base;
using SoftwareGrid.Repository.Security.UserManagement;
using SoftwareGrid.Service.Base;

namespace SoftwareGrid.Service.Security.UserManagement
{
   public  class UserService: BaseService<User>, IUserService
    {
        private readonly IUserRepository _iUserRepository;
        private readonly DbContext _dbContext;

        public UserService(IBaseRepository<User> iBaseRepository, IUserRepository iUserRepository, DbContext dbContext)
            : base(iBaseRepository, dbContext)
        {
            _iUserRepository = iUserRepository;
            _dbContext = dbContext;
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
    }

    public interface IUserService : IBaseService<User>
    {
        User GetUser(string userName);
        User GetUser(string userName, string password);
    }
}
