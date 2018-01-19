using Dapper;
using SoftwareGrid.Model.iTestApp.UserManagement;
using SoftwareGrid.Repository.iTestApp.Base;

namespace SoftwareGrid.Repository.iTestApp.UserManagement
{
    public class UserLoginInformationRepository : BaseRepository<UserLoginInformation>, IUserLoginInformationRepository
    {
         private readonly AppDbContext _dbContext;
         public UserLoginInformationRepository(AppDbContext dbContext)
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


    public interface IUserLoginInformationRepository : IBaseRepository<UserLoginInformation>
    {
        int UpdateUserLogoutTime(int userId);
    }

}
