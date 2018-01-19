using Dapper;
using SoftwareGrid.Model.iTestApp.UserManagement;
using SoftwareGrid.Repository.iTestApp.Base;

namespace SoftwareGrid.Repository.iTestApp.UserManagement
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
         private readonly AppDbContext _dbContext;
         public RoleRepository(AppDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }


    }


    public interface IRoleRepository : IBaseRepository<Role>
    {
    }

}
