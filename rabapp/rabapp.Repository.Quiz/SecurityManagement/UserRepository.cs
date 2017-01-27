using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using rabapp.Model.Quiz.ViewModels;
using rabapp.Repository.Common;

namespace rabapp.Repository.Quiz.SecurityManagement
{

    public class UserRepository : BaseRepository<UserViewModel>, IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        public UserRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
        }

        public UserViewModel GetUserByUserName(string userName)
        {
            const string query = @"SELECT * FROM User WHERE UserName = @UserName";
            var userViewModel = _appDbContext.SqlConnection.Query<UserViewModel>(query, new { UserName = userName }).FirstOrDefault();
            return userViewModel;

        }
    }

    public interface IUserRepository : IBaseRepository<UserViewModel>
    {
        UserViewModel GetUserByUserName(string userName);
    }
}
