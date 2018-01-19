using Dapper;
using SoftwareGrid.Model.iTestApp.UserManagement;
using SoftwareGrid.Repository.iTestApp.Base;

namespace SoftwareGrid.Repository.iTestApp.UserManagement
{
    public class CompanyBranchRepository : BaseRepository<CompanyBranch>, ICompanyBranchRepository
    {
         private readonly AppDbContext _dbContext;
         public CompanyBranchRepository(AppDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }


    }


    public interface ICompanyBranchRepository : IBaseRepository<CompanyBranch>
    {
    }

}
