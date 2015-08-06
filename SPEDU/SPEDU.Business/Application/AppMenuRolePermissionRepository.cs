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
    #region Interface Implement : AppMenuRolePermission

    public class AppMenuRolePermissionRepository : IAppMenuRolePermissionRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<AppMenuRolePermission> _appMenuPermissionRepository;
        private readonly RepositoryBase<AppMenuRolePermission> _appMenuPermissionRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public AppMenuRolePermissionRepository(Repository<AppMenuRolePermission> appMenuPermissionRepository, IUnitOfWork iUnitOfWork)
        {
            this._appMenuPermissionRepository = appMenuPermissionRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<AppMenuRolePermissionViewModel> GetAll()
        {
            var appMenuRolePermissionViewModels = new List<AppMenuRolePermissionViewModel>();
            try
            {

                List<AppMenuRolePermission> appMenuPermissions = _appMenuPermissionRepository.GetAll();

                foreach (AppMenuRolePermission appMenuPermission in appMenuPermissions)
                {
                    var appMenuRolePermissionViewModel = appMenuPermission.ConvertModelToViewModel<AppMenuRolePermission, AppMenuRolePermissionViewModel>();
                    appMenuRolePermissionViewModels.Add(appMenuRolePermissionViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appMenuRolePermissionViewModels.AsQueryable();
        }

        public AppMenuRolePermissionViewModel GetById(long id)
        {
            var appMenuRolePermissionViewModel = new AppMenuRolePermissionViewModel();

            try
            {
                AppMenuRolePermission appMenuPermission = _appMenuPermissionRepository.GetById(id);
                appMenuRolePermissionViewModel = appMenuPermission.ConvertModelToViewModel<AppMenuRolePermission, AppMenuRolePermissionViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appMenuRolePermissionViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(AppMenuRolePermissionViewModel appMenuRolePermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appMenuRolePermissionViewModel != null)
                {
                    //add
                    if (appMenuRolePermissionViewModel.AppMenuRolePermissionId == 0 && appMenuRolePermissionViewModel.ActionName == "Add")
                    {
                        Create(appMenuRolePermissionViewModel);
                    }
                    else if (appMenuRolePermissionViewModel.ActionName == "Edit") //edit
                    {
                        Update(appMenuRolePermissionViewModel);
                    }
                }
                else
                {
                    throw new ArgumentNullException("AppMenuPermissionViewModel", "Request data is null.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(AppMenuRolePermissionViewModel appMenuRolePermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appMenuRolePermissionViewModel != null)
                {
                    AppMenuRolePermission appMenuPermission = appMenuRolePermissionViewModel.ConvertViewModelToModel<AppMenuRolePermissionViewModel, AppMenuRolePermission>();
                    _appMenuPermissionRepository.Insert(appMenuPermission);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppMenuPermissionViewModel", "Request data is null.");
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

        public int Update(AppMenuRolePermissionViewModel appMenuRolePermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appMenuRolePermissionViewModel != null)
                {
                    AppMenuRolePermission appMenuPermission = appMenuRolePermissionViewModel.ConvertViewModelToModel<AppMenuRolePermissionViewModel, AppMenuRolePermission>();
                    _appMenuPermissionRepository.Update(appMenuPermission);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppMenuPermissionViewModel", "Request data is null.");
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

        public int Delete(AppMenuRolePermissionViewModel appMenuRolePermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appMenuRolePermissionViewModel != null)
                {
                    var viewModel = GetById(appMenuRolePermissionViewModel.AppMenuRolePermissionId);
                    AppMenuRolePermission appMenuPermission = viewModel.ConvertViewModelToModel<AppMenuRolePermissionViewModel, AppMenuRolePermission>();
                    _appMenuPermissionRepository.Delete(appMenuPermission);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppMenuPermissionViewModel", "Request data is null.");
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
                    AppMenuRolePermission appMenuPermission = appMenuRolePermissionViewModel.ConvertViewModelToModel<AppMenuRolePermissionViewModel, AppMenuRolePermission>();
                    _appMenuPermissionRepository.Delete(appMenuPermission);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppMenuPermissionViewModel", "Request data is null.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<AppMenuRolePermissionViewModel> appMenuRolePermissionViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appMenuRolePermissionViewModel in appMenuRolePermissionViewModels)
                {
                    AppMenuRolePermissionViewModel viewModel = GetById(appMenuRolePermissionViewModel.AppMenuRolePermissionId);
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

    #region Interface : AppMenuRolePermission

    public interface IAppMenuRolePermissionRepository : IGeneric<AppMenuRolePermissionViewModel>
    {
        int Delete(List<AppMenuRolePermissionViewModel> appMenuRolePermissionViewModels);
    }

    #endregion
}
