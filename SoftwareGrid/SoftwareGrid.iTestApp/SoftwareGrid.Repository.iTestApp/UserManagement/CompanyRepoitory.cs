using Dapper;
using SoftwareGrid.Model.iTestApp.SecurityManagement;
using SoftwareGrid.Repository.iTestApp.Base;

namespace SoftwareGrid.Repository.iTestApp.UserManagement
{
    public class CompanyRepoitory : BaseRepository<Company>, ICompanyRepoitory
    {
         private readonly DbContext _dbContext;
         public CompanyRepoitory(DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }


    }


    public interface ICompanyRepoitory : IBaseRepository<Company>
    {
    }

}
