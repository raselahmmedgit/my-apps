using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rabapp.Model.Quiz.ViewModels;
using rabapp.Repository.Common;

namespace rabapp.Repository.Quiz.SecurityManagement
{
    public class CompanyBranchRepository : BaseRepository<CompanyBranchViewModel>, ICompanyBranchRepository
    {
        private readonly AppDbContext _appDbContext;
        public CompanyBranchRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
        }
    }

    public interface ICompanyBranchRepository : IBaseRepository<CompanyBranchViewModel>
    {
    }
}
