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
    public class RoleService : BaseService<RoleViewModel>, IRoleService
    {
        private readonly IRoleRepository _iRoleRepository;
        private readonly AppDbContext _appDbContext;

        public RoleService(IBaseRepository<RoleViewModel> iBaseRepository, IRoleRepository iRoleRepository, AppDbContext appDbContext)
            : base(iBaseRepository, appDbContext)
        {
            _iRoleRepository = iRoleRepository;
            _appDbContext = appDbContext;
        }
    }

    public interface IRoleService : IBaseService<RoleViewModel>
    {
    }
}
