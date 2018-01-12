using System;
using SoftwareGrid.Model.iTestApp.UserManagement;
using SoftwareGrid.Model.iTestApp.Utility;
using SoftwareGrid.Repository.iTestApp.Base;
using SoftwareGrid.Repository.iTestApp.UserManagement;
using SoftwareGrid.Service.iTestApp.Base;

namespace SoftwareGrid.Service.iTestApp.UserManagement
{
    public class CompanyService : BaseService<Company>, ICompanyService
    {
        private readonly ICompanyRepoitory _iCompanyRepoitory;
        private readonly DbContext _dbContext;

        public CompanyService(IBaseRepository<Company> iBaseRepository, ICompanyRepoitory iCompanyRepoitory, DbContext dbContext)
            : base(iBaseRepository, dbContext)
        {
            _iCompanyRepoitory = iCompanyRepoitory;
            _dbContext = dbContext;
        }

    }

    public interface ICompanyService : IBaseService<Company>
    {
    }

}
