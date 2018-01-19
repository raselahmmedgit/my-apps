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
        private readonly IUserRoleRepository _iUserRoleRepository;
        private readonly AppDbContext _dbContext;

        public UserRoleService(IBaseRepository<UserRole> iBaseRepository, IUserRoleRepository iUserRoleRepository, AppDbContext dbContext)
            : base(iBaseRepository, dbContext)
        {
            _iUserRoleRepository = iUserRoleRepository;
            _dbContext = dbContext;
        }

    }

    public interface IUserRoleService : IBaseService<UserRole>
    {
    }

}
