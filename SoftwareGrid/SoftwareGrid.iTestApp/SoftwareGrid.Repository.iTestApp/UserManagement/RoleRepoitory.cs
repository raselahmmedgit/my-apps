using Dapper;
using SoftwareGrid.Model.iTestApp.UserManagement;
using SoftwareGrid.Repository.iTestApp.Base;

namespace SoftwareGrid.Repository.iTestApp.UserManagement
{
    public class RoleRepoitory : BaseRepository<Role>, IRoleRepoitory
    {
         private readonly DbContext _dbContext;
         public RoleRepoitory(DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }


    }


    public interface IRoleRepoitory : IBaseRepository<Role>
    {
    }

}
