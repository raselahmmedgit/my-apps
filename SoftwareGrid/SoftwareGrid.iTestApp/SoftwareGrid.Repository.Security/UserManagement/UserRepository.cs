using System.Linq;
using Dapper;
using SoftwareGrid.Model.Security.UserManagement;
using SoftwareGrid.Repository.Base;

namespace SoftwareGrid.Repository.Security.UserManagement
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly DbContext _dbContext;
        public UserRepository(DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public User GetUser(string userName)
        {
            return _dbContext.SqlConnection.Query<User>("SELECT *FROM dbo.[User] Where Email =@Email", new { Email = userName }).FirstOrDefault();
           
        }

        public User GetUser(string userName, string password)
        {
            return _dbContext.SqlConnection.Query<User>("SELECT *FROM dbo.[User] Where Email =@Email AND Password=@Password ", new User{Email = userName,Password = password}).FirstOrDefault();
        }
    }

    public interface IUserRepository : IBaseRepository<User>
    {
        User GetUser(string userName);
        User GetUser(string userName, string password);
    }
}
