using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rabapp.Model.Quiz.ViewModels;
using rabapp.Repository.Common;

namespace rabapp.Repository.Quiz.SecurityManagement
{

    public class CompanyRepository : BaseRepository<CompanyViewModel>, ICompanyRepository
    {
        private readonly AppDbContext _appDbContext;
        public CompanyRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
        }
    }

    public interface ICompanyRepository : IBaseRepository<CompanyViewModel>
    {
    }
}
