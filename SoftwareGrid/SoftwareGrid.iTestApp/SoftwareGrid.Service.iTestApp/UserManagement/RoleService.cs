using System;
using SoftwareGrid.Model.iTestApp.SecurityManagement;
using SoftwareGrid.Model.iTestApp.Utility;
using SoftwareGrid.Repository.iTestApp.Base;
using SoftwareGrid.Repository.iTestApp.UserManagement;
using SoftwareGrid.Service.iTestApp.Base;

namespace SoftwareGrid.Service.iTestApp.UserManagement
{
    public class RoleService : BaseService<Role>, IRoleService
    {
        private readonly IRoleRepoitory _iRoleRepoitory;
        private readonly DbContext _dbContext;

        public RoleService(IBaseRepository<Role> iBaseRepository, IRoleRepoitory iRoleRepoitory, DbContext dbContext)
            : base(iBaseRepository, dbContext)
        {
            _iRoleRepoitory = iRoleRepoitory;
            _dbContext = dbContext;
        }

    }

    public interface IRoleService : IBaseService<Role>
    {
    }

}
