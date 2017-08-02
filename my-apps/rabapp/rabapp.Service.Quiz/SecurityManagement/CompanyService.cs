using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rabapp.Repository.Common;
using rabapp.Repository.Quiz.SecurityManagement;
using rabapp.Service.Common;
using rabapp.ViewModel.Quiz.ViewModels;

namespace rabapp.Service.Quiz.SecurityManagement
{
    public class CompanyService : BaseService<CompanyViewModel>, ICompanyService
    {
        private readonly ICompanyRepository _iCompanyRepository;
        private readonly AppDbContext _appDbContext;

        public CompanyService(IBaseRepository<CompanyViewModel> iBaseRepository, ICompanyRepository iCompanyRepository, AppDbContext appDbContext)
            : base(iBaseRepository, appDbContext)
        {
            _iCompanyRepository = iCompanyRepository;
            _appDbContext = appDbContext;
        }
    }

    public interface ICompanyService : IBaseService<CompanyViewModel>
    {
    }
}
