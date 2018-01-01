using Dapper;
using SoftwareGrid.Model.iTestApp.SecurityManagement;
using SoftwareGrid.Repository.iTestApp.Base;

namespace SoftwareGrid.Repository.iTestApp.UserManagement
{
    public class UserLoginInformationRepoitory : BaseRepository<UserLoginInformation>, IUserLoginInformationRepoitory
    {
         private readonly DbContext _dbContext;
         public UserLoginInformationRepoitory(DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

         public int UpdateUserLogoutTime(int userId)
         {
             const string query = @"UPDATE UserLoginInformation SET LogoutDateTime = GETUTCDATE() 
                                    WHERE UserId = @UserId AND 
                                    Sl= (SELECT MAX(SL) FROM UserLoginInformation WHERE UserId = @UserId)";
            return _dbContext.SqlConnection.Execute(query, new { UserId = userId });
         }

    }


    public interface IUserLoginInformationRepoitory : IBaseRepository<UserLoginInformation>
    {
        int UpdateUserLogoutTime(int userId);
    }

}
