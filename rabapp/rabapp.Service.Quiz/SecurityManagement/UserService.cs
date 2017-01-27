using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rabapp.Model.Quiz.ViewModels;
using rabapp.Repository.Common;
using rabapp.Repository.Quiz.SecurityManagement;
using rabapp.Service.Common;

namespace rabapp.Service.Quiz.SecurityManagement
{
    public class UserService : BaseService<UserViewModel>, IUserService
    {
        private readonly IUserRepository _iUserRepository;
        private readonly AppDbContext _appDbContext;

        public UserService(IBaseRepository<UserViewModel> iBaseRepository, IUserRepository iUserRepository, AppDbContext appDbContext)
            : base(iBaseRepository, appDbContext)
        {
            _iUserRepository = iUserRepository;
            _appDbContext = appDbContext;
        }

        public UserViewModel GetUserByUserName(string userName)
        {
            try
            {
                _appDbContext.SqlConnection.Open();

                return _iUserRepository.GetUserByUserName(userName);
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

    public interface IUserService : IBaseService<UserViewModel>
    {
        UserViewModel GetUserByUserName(string userName);
    }
}
