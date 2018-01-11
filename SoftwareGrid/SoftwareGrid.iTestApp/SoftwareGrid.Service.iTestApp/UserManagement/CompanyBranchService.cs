using System;
using SoftwareGrid.Model.iTestApp.SecurityManagement;
using SoftwareGrid.Model.iTestApp.Utility;
using SoftwareGrid.Repository.iTestApp.Base;
using SoftwareGrid.Repository.iTestApp.UserManagement;
using SoftwareGrid.Service.iTestApp.Base;

namespace SoftwareGrid.Service.iTestApp.UserManagement
{
    public class CompanyBranchService : BaseService<CompanyBranch>, ICompanyBranchService
    {
        private readonly ICompanyBranchRepoitory _iCompanyBranchRepoitory;
        private readonly DbContext _dbContext;

        public CompanyBranchService(IBaseRepository<CompanyBranch> iBaseRepository, ICompanyBranchRepoitory iCompanyBranchRepoitory, DbContext dbContext)
            : base(iBaseRepository, dbContext)
        {
            _iCompanyBranchRepoitory = iCompanyBranchRepoitory;
            _dbContext = dbContext;
        }

    }

    public interface ICompanyBranchService : IBaseService<CompanyBranch>
    {
    }

}
