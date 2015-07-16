using System;
using System.Collections.Generic;
using System.Linq;
using SPEDU.Domain.Extensions;
using SPEDU.Domain.Models.Application;
using SPEDU.Data.Infrastructure;
using SPEDU.Data.Repositories;
using SPEDU.DomainViewModel.Application;

namespace SPEDU.Business.Application
{
    #region Interface Implement : Role

    public class RoleRepository : IRoleRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<Role> _roleRepository;
        private readonly RepositoryBase<Role> _roleRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public RoleRepository(Repository<Role> roleRepository, IUnitOfWork iUnitOfWork)
        {
            this._roleRepository = roleRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<RoleViewModel> GetAll()
        {
            var roleViewModels = new List<RoleViewModel>();
            try
            {

                List<Role> roles = _roleRepository.GetAll();

                foreach (Role role in roles)
                {
                    var roleViewModel = role.ConvertModelToViewModel<Role, RoleViewModel>();
                    roleViewModels.Add(roleViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return roleViewModels.AsQueryable();
        }

        public RoleViewModel GetById(long id)
        {
            var roleViewModel = new RoleViewModel();

            try
            {
                Role role = _roleRepository.GetById(id);
                roleViewModel = role.ConvertModelToViewModel<Role, RoleViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return roleViewModel;
        }

        public RoleViewModel GetByRoleName(string roleName)
        {
            var roleViewModel = new RoleViewModel();

            try
            {
                Role role = _roleRepository.GetAll().SingleOrDefault(x => x.RoleName == roleName);
                roleViewModel = role.ConvertModelToViewModel<Role, RoleViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return roleViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(RoleViewModel roleViewModel)
        {
            int isSave = 0;
            try
            {
                if (roleViewModel != null)
                {
                    //add
                    if (roleViewModel.RoleId == 0 && roleViewModel.ActionName == "Add")
                    {
                        Create(roleViewModel);
                    }
                    else if (roleViewModel.ActionName == "Edit") //edit
                    {
                        Update(roleViewModel);
                    }
                }
                else
                {
                    throw new ArgumentNullException("RoleViewModel", "Request data is null.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }

        public int Create(RoleViewModel roleViewModel)
        {
            int isSave = 0;
            try
            {
                if (roleViewModel != null)
                {
                    Role role = roleViewModel.ConvertViewModelToModel<RoleViewModel, Role>();
                    _roleRepository.Insert(role);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("RoleViewModel", "Request data is null.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }

        #endregion

        #region Update Method

        public int Update(RoleViewModel roleViewModel)
        {
            int isSave = 0;
            try
            {
                var updateRoleViewModel = GetById(roleViewModel.RoleId);

                if (updateRoleViewModel != null)
                {
                    Role role = roleViewModel.ConvertViewModelToModel<RoleViewModel, Role>();
                    _roleRepository.Update(role);
                    isSave = Save();
                    
                }
                else
                {
                    throw new ArgumentNullException("RoleViewModel", "Request data is null.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        #endregion

        #region Delete Method

        public int Delete(RoleViewModel roleViewModel)
        {
            int isSave = 0;
            try
            {
                var deleteRoleViewModel = GetById(roleViewModel.RoleId);

                if (deleteRoleViewModel != null)
                {
                    var viewModel = GetById(roleViewModel.RoleId);
                    Role role = viewModel.ConvertViewModelToModel<RoleViewModel, Role>();
                    _roleRepository.Delete(role);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("RoleViewModel", "Request data is null.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(long id)
        {
            int isSave = 0;
            try
            {
                var roleViewModel = GetById(id);
                if (roleViewModel != null)
                {
                    Role role = roleViewModel.ConvertViewModelToModel<RoleViewModel, Role>();
                    _roleRepository.Delete(role);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("RoleViewModel", "Request data is null.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<RoleViewModel> roleViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var roleViewModel in roleViewModels)
                {
                    RoleViewModel viewModel = GetById(roleViewModel.RoleId);
                    Delete(viewModel);
                }


                isSave = Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        #endregion

        #region Save By Commit

        public int Save()
        {
            return _iUnitOfWork.Commit();
        }

        #endregion

    }

    #endregion

    #region Interface : Role

    public interface IRoleRepository : IGeneric<RoleViewModel>
    {
        int Delete(List<RoleViewModel> roleViewModels);

        RoleViewModel GetByRoleName(string roleName);
    }

    #endregion
}
