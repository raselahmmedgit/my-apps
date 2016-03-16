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
    #region Interface Implement : MenuRolePermission

    public class MenuRolePermissionRepository : IMenuRolePermissionRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<MenuRolePermission> _appMenuPermissionRepository;
        private readonly RepositoryBase<MenuRolePermission> _appMenuPermissionRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public MenuRolePermissionRepository(Repository<MenuRolePermission> appMenuPermissionRepository, IUnitOfWork iUnitOfWork)
        {
            this._appMenuPermissionRepository = appMenuPermissionRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<MenuRolePermissionViewModel> GetAll()
        {
            var appMenuRolePermissionViewModels = new List<MenuRolePermissionViewModel>();
            try
            {

                List<MenuRolePermission> appMenuPermissions = _appMenuPermissionRepository.GetAll();

                foreach (MenuRolePermission appMenuPermission in appMenuPermissions)
                {
                    var appMenuRolePermissionViewModel = appMenuPermission.ConvertModelToViewModel<MenuRolePermission, MenuRolePermissionViewModel>();
                    appMenuRolePermissionViewModels.Add(appMenuRolePermissionViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appMenuRolePermissionViewModels.AsQueryable();
        }

        public MenuRolePermissionViewModel GetById(long id)
        {
            var appMenuRolePermissionViewModel = new MenuRolePermissionViewModel();

            try
            {
                MenuRolePermission appMenuPermission = _appMenuPermissionRepository.GetById(id);
                appMenuRolePermissionViewModel = appMenuPermission.ConvertModelToViewModel<MenuRolePermission, MenuRolePermissionViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appMenuRolePermissionViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(MenuRolePermissionViewModel appMenuRolePermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appMenuRolePermissionViewModel != null)
                {
                    //add
                    if (appMenuRolePermissionViewModel.MenuRolePermissionId == default(int))
                    {
                        Create(appMenuRolePermissionViewModel);
                    }
                    else //edit
                    {
                        Update(appMenuRolePermissionViewModel);
                    }
                }
                else
                {
                    throw new ArgumentNullException("MenuPermissionViewModel", "Request data is null.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(MenuRolePermissionViewModel appMenuRolePermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appMenuRolePermissionViewModel != null)
                {
                    MenuRolePermission appMenuPermission = appMenuRolePermissionViewModel.ConvertViewModelToModel<MenuRolePermissionViewModel, MenuRolePermission>();
                    _appMenuPermissionRepository.Insert(appMenuPermission);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("MenuPermissionViewModel", "Request data is null.");
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

        public int Update(MenuRolePermissionViewModel appMenuRolePermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appMenuRolePermissionViewModel != null)
                {
                    MenuRolePermission appMenuPermission = appMenuRolePermissionViewModel.ConvertViewModelToModel<MenuRolePermissionViewModel, MenuRolePermission>();
                    _appMenuPermissionRepository.Update(appMenuPermission);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("MenuPermissionViewModel", "Request data is null.");
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

        public int Delete(MenuRolePermissionViewModel appMenuRolePermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appMenuRolePermissionViewModel != null)
                {
                    var viewModel = GetById(appMenuRolePermissionViewModel.MenuRolePermissionId);
                    MenuRolePermission appMenuPermission = viewModel.ConvertViewModelToModel<MenuRolePermissionViewModel, MenuRolePermission>();
                    _appMenuPermissionRepository.Delete(appMenuPermission);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("MenuPermissionViewModel", "Request data is null.");
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
                var appMenuRolePermissionViewModel = GetById(id);
                if (appMenuRolePermissionViewModel != null)
                {
                    MenuRolePermission appMenuPermission = appMenuRolePermissionViewModel.ConvertViewModelToModel<MenuRolePermissionViewModel, MenuRolePermission>();
                    _appMenuPermissionRepository.Delete(appMenuPermission);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("MenuPermissionViewModel", "Request data is null.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<MenuRolePermissionViewModel> appMenuRolePermissionViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appMenuRolePermissionViewModel in appMenuRolePermissionViewModels)
                {
                    MenuRolePermissionViewModel viewModel = GetById(appMenuRolePermissionViewModel.MenuRolePermissionId);
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

    #region Interface : MenuRolePermission

    public interface IMenuRolePermissionRepository : IGeneric<MenuRolePermissionViewModel>
    {
        int Delete(List<MenuRolePermissionViewModel> appMenuRolePermissionViewModels);
    }

    #endregion
}
