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
    #region Interface Implement : MenuUserPermission

    public class MenuUserPermissionRepository : IMenuUserPermissionRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<MenuUserPermission> _appMenuPermissionRepository;
        private readonly RepositoryBase<MenuUserPermission> _appMenuPermissionRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public MenuUserPermissionRepository(Repository<MenuUserPermission> appMenuPermissionRepository, IUnitOfWork iUnitOfWork)
        {
            this._appMenuPermissionRepository = appMenuPermissionRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<MenuUserPermissionViewModel> GetAll()
        {
            var appMenuUserPermissionViewModels = new List<MenuUserPermissionViewModel>();
            try
            {

                List<MenuUserPermission> appMenuPermissions = _appMenuPermissionRepository.GetAll();

                foreach (MenuUserPermission appMenuPermission in appMenuPermissions)
                {
                    var appMenuUserPermissionViewModel = appMenuPermission.ConvertModelToViewModel<MenuUserPermission, MenuUserPermissionViewModel>();
                    appMenuUserPermissionViewModels.Add(appMenuUserPermissionViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appMenuUserPermissionViewModels.AsQueryable();
        }

        public MenuUserPermissionViewModel GetById(long id)
        {
            var appMenuUserPermissionViewModel = new MenuUserPermissionViewModel();

            try
            {
                MenuUserPermission appMenuPermission = _appMenuPermissionRepository.GetById(id);
                appMenuUserPermissionViewModel = appMenuPermission.ConvertModelToViewModel<MenuUserPermission, MenuUserPermissionViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appMenuUserPermissionViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(MenuUserPermissionViewModel appMenuUserPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appMenuUserPermissionViewModel != null)
                {
                    //add
                    if (appMenuUserPermissionViewModel.MenuUserPermissionId == default(int))
                    {
                        Create(appMenuUserPermissionViewModel);
                    }
                    else //edit
                    {
                        Update(appMenuUserPermissionViewModel);
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
        public int Create(MenuUserPermissionViewModel appMenuUserPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appMenuUserPermissionViewModel != null)
                {
                    MenuUserPermission appMenuPermission = appMenuUserPermissionViewModel.ConvertViewModelToModel<MenuUserPermissionViewModel, MenuUserPermission>();
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

        public int Update(MenuUserPermissionViewModel appMenuUserPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appMenuUserPermissionViewModel != null)
                {
                    MenuUserPermission appMenuPermission = appMenuUserPermissionViewModel.ConvertViewModelToModel<MenuUserPermissionViewModel, MenuUserPermission>();
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

        public int Delete(MenuUserPermissionViewModel appMenuUserPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appMenuUserPermissionViewModel != null)
                {
                    var viewModel = GetById(appMenuUserPermissionViewModel.MenuUserPermissionId);
                    MenuUserPermission appMenuPermission = viewModel.ConvertViewModelToModel<MenuUserPermissionViewModel, MenuUserPermission>();
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
                var appMenuUserPermissionViewModel = GetById(id);
                if (appMenuUserPermissionViewModel != null)
                {
                    MenuUserPermission appMenuPermission = appMenuUserPermissionViewModel.ConvertViewModelToModel<MenuUserPermissionViewModel, MenuUserPermission>();
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

        public int Delete(List<MenuUserPermissionViewModel> appMenuUserPermissionViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appMenuUserPermissionViewModel in appMenuUserPermissionViewModels)
                {
                    MenuUserPermissionViewModel viewModel = GetById(appMenuUserPermissionViewModel.MenuUserPermissionId);
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

    #region Interface : MenuUserPermission

    public interface IMenuUserPermissionRepository : IGeneric<MenuUserPermissionViewModel>
    {
        int Delete(List<MenuUserPermissionViewModel> appMenuUserPermissionViewModels);
    }

    #endregion
}
