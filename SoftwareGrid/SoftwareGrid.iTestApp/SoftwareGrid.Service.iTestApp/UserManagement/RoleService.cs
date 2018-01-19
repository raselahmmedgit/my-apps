using System;
using SoftwareGrid.Model.iTestApp.UserManagement;
using SoftwareGrid.Model.iTestApp.Utility;
using SoftwareGrid.Repository.iTestApp.Base;
using SoftwareGrid.Repository.iTestApp.UserManagement;
using SoftwareGrid.Service.iTestApp.Base;

namespace SoftwareGrid.Service.iTestApp.UserManagement
{
    public class RoleService : BaseService<Role>, IRoleService
    {
        private readonly IRoleRepository _iRoleRepository;
        private readonly AppDbContext _dbContext;

        public RoleService(IBaseRepository<Role> iBaseRepository, IRoleRepository iRoleRepository, AppDbContext dbContext)
            : base(iBaseRepository, dbContext)
        {
            _iRoleRepository = iRoleRepository;
            _dbContext = dbContext;
        }

    }

    public interface IRoleService : IBaseService<Role>
    {
    }

}
