using Dapper;
using SoftwareGrid.Model.iTestApp.UserManagement;
using SoftwareGrid.Repository.iTestApp.Base;

namespace SoftwareGrid.Repository.iTestApp.UserManagement
{
    public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
         private readonly AppDbContext _dbContext;
         public UserRoleRepository(AppDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }


    }


    public interface IUserRoleRepository : IBaseRepository<UserRole>
    {
    }

}
