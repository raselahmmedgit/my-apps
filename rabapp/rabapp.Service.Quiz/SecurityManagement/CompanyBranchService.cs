using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rabapp.Repository.Common;
using rabapp.Repository.Quiz.SecurityManagement;
using rabapp.Service.Common;
using rabapp.ViewModel.Quiz.SecurityManagement;

namespace rabapp.Service.Quiz.SecurityManagement
{
    public class CompanyBranchService : BaseService<CompanyBranchViewModel>, ICompanyBranchService
    {
        private readonly ICompanyBranchRepository _iCompanyBranchRepository;
        private readonly AppDbContext _appDbContext;

        public CompanyBranchService(IBaseRepository<CompanyBranchViewModel> iBaseRepository, ICompanyBranchRepository iCompanyBranchRepository, AppDbContext appDbContext)
            : base(iBaseRepository, appDbContext)
        {
            _iCompanyBranchRepository = iCompanyBranchRepository;
            _appDbContext = appDbContext;
        }
    }

    public interface ICompanyBranchService : IBaseService<CompanyBranchViewModel>
    {
    }
}
