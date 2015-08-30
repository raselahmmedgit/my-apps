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
    #region Interface Implement : AppRightRolePermission

    public class AppRightRolePermissionRepository : IAppRightRolePermissionRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<AppRightRolePermission> _appRightPermissionRepository;
        private readonly RepositoryBase<AppRightRolePermission> _appRightPermissionRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public AppRightRolePermissionRepository(Repository<AppRightRolePermission> appRightPermissionRepository, IUnitOfWork iUnitOfWork)
        {
            this._appRightPermissionRepository = appRightPermissionRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<AppRightRolePermissionViewModel> GetAll()
        {
            var appRightRolePermissionViewModels = new List<AppRightRolePermissionViewModel>();
            try
            {

                List<AppRightRolePermission> appRightPermissions = _appRightPermissionRepository.GetAll();

                foreach (AppRightRolePermission appRightPermission in appRightPermissions)
                {
                    var appRightRolePermissionViewModel = appRightPermission.ConvertModelToViewModel<AppRightRolePermission, AppRightRolePermissionViewModel>();
                    appRightRolePermissionViewModels.Add(appRightRolePermissionViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appRightRolePermissionViewModels.AsQueryable();
        }

        public AppRightRolePermissionViewModel GetById(long id)
        {
            var appRightRolePermissionViewModel = new AppRightRolePermissionViewModel();

            try
            {
                AppRightRolePermission appRightPermission = _appRightPermissionRepository.GetById(id);
                appRightRolePermissionViewModel = appRightPermission.ConvertModelToViewModel<AppRightRolePermission, AppRightRolePermissionViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appRightRolePermissionViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(AppRightRolePermissionViewModel appRightRolePermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appRightRolePermissionViewModel != null)
                {
                    //add
                    if (appRightRolePermissionViewModel.AppRightRolePermissionId == default(int))
                    {
                        Create(appRightRolePermissionViewModel);
                    }
                    else //edit
                    {
                        Update(appRightRolePermissionViewModel);
                    }
                }
                else
                {
                    throw new ArgumentNullException("AppRightPermissionViewModel", "Request data is null.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(AppRightRolePermissionViewModel appRightRolePermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appRightRolePermissionViewModel != null)
                {
                    AppRightRolePermission appRightPermission = appRightRolePermissionViewModel.ConvertViewModelToModel<AppRightRolePermissionViewModel, AppRightRolePermission>();
                    _appRightPermissionRepository.Insert(appRightPermission);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppRightPermissionViewModel", "Request data is null.");
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

        public int Update(AppRightRolePermissionViewModel appRightRolePermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appRightRolePermissionViewModel != null)
                {
                    AppRightRolePermission appRightPermission = appRightRolePermissionViewModel.ConvertViewModelToModel<AppRightRolePermissionViewModel, AppRightRolePermission>();
                    _appRightPermissionRepository.Update(appRightPermission);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppRightPermissionViewModel", "Request data is null.");
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

        public int Delete(AppRightRolePermissionViewModel appRightRolePermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appRightRolePermissionViewModel != null)
                {
                    var viewModel = GetById(appRightRolePermissionViewModel.AppRightRolePermissionId);
                    AppRightRolePermission appRightPermission = viewModel.ConvertViewModelToModel<AppRightRolePermissionViewModel, AppRightRolePermission>();
                    _appRightPermissionRepository.Delete(appRightPermission);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppRightPermissionViewModel", "Request data is null.");
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
                var appRightRolePermissionViewModel = GetById(id);
                if (appRightRolePermissionViewModel != null)
                {
                    AppRightRolePermission appRightPermission = appRightRolePermissionViewModel.ConvertViewModelToModel<AppRightRolePermissionViewModel, AppRightRolePermission>();
                    _appRightPermissionRepository.Delete(appRightPermission);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppRightPermissionViewModel", "Request data is null.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<AppRightRolePermissionViewModel> appRightRolePermissionViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appRightRolePermissionViewModel in appRightRolePermissionViewModels)
                {
                    AppRightRolePermissionViewModel viewModel = GetById(appRightRolePermissionViewModel.AppRightRolePermissionId);
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

    #region Interface : AppRightRolePermission

    public interface IAppRightRolePermissionRepository : IGeneric<AppRightRolePermissionViewModel>
    {
        int Delete(List<AppRightRolePermissionViewModel> appRightRolePermissionViewModels);
    }

    #endregion
}
