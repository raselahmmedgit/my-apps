using Dapper;
using SoftwareGrid.Model.iTestApp.SecurityManagement;
using SoftwareGrid.Repository.iTestApp.Base;

namespace SoftwareGrid.Repository.iTestApp.UserManagement
{
    public class UserRoleRepoitory : BaseRepository<UserRole>, IUserRoleRepoitory
    {
         private readonly DbContext _dbContext;
         public UserRoleRepoitory(DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }


    }


    public interface IUserRoleRepoitory : IBaseRepository<UserRole>
    {
    }

}
