using System;
using SoftwareGrid.Model.iTestApp.UserManagement;
using SoftwareGrid.Model.iTestApp.Utility;
using SoftwareGrid.Repository.iTestApp.Base;
using SoftwareGrid.Repository.iTestApp.UserManagement;
using SoftwareGrid.Service.iTestApp.Base;

namespace SoftwareGrid.Service.iTestApp.UserManagement
{
    public class UserRoleService : BaseService<UserRole>, IUserRoleService
    {
        private readonly IUserRoleRepoitory _iUserRoleRepoitory;
        private readonly DbContext _dbContext;

        public UserRoleService(IBaseRepository<UserRole> iBaseRepository, IUserRoleRepoitory iUserRoleRepoitory, DbContext dbContext)
            : base(iBaseRepository, dbContext)
        {
            _iUserRoleRepoitory = iUserRoleRepoitory;
            _dbContext = dbContext;
        }

    }

    public interface IUserRoleService : IBaseService<UserRole>
    {
    }

}
