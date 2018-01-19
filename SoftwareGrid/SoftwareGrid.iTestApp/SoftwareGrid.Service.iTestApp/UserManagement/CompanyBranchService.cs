using System;
using SoftwareGrid.Model.iTestApp.UserManagement;
using SoftwareGrid.Model.iTestApp.Utility;
using SoftwareGrid.Repository.iTestApp.Base;
using SoftwareGrid.Repository.iTestApp.UserManagement;
using SoftwareGrid.Service.iTestApp.Base;

namespace SoftwareGrid.Service.iTestApp.UserManagement
{
    public class CompanyBranchService : BaseService<CompanyBranch>, ICompanyBranchService
    {
        private readonly ICompanyBranchRepository _iCompanyBranchRepository;
        private readonly AppDbContext _dbContext;

        public CompanyBranchService(IBaseRepository<CompanyBranch> iBaseRepository, ICompanyBranchRepository iCompanyBranchRepository, AppDbContext dbContext)
            : base(iBaseRepository, dbContext)
        {
            _iCompanyBranchRepository = iCompanyBranchRepository;
            _dbContext = dbContext;
        }

    }

    public interface ICompanyBranchService : IBaseService<CompanyBranch>
    {
    }

}
