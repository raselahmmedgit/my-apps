using Dapper;
using SoftwareGrid.Model.iTestApp.UserManagement;
using SoftwareGrid.Repository.iTestApp.Base;

namespace SoftwareGrid.Repository.iTestApp.UserManagement
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
         private readonly AppDbContext _dbContext;
         public CompanyRepository(AppDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }


    }


    public interface ICompanyRepository : IBaseRepository<Company>
    {
    }

}
