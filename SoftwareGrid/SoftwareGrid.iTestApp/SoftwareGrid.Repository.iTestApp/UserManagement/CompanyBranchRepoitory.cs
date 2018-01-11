using Dapper;
using SoftwareGrid.Model.iTestApp.SecurityManagement;
using SoftwareGrid.Repository.iTestApp.Base;

namespace SoftwareGrid.Repository.iTestApp.UserManagement
{
    public class CompanyBranchRepoitory : BaseRepository<CompanyBranch>, ICompanyBranchRepoitory
    {
         private readonly DbContext _dbContext;
         public CompanyBranchRepoitory(DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }


    }


    public interface ICompanyBranchRepoitory : IBaseRepository<CompanyBranch>
    {
    }

}
