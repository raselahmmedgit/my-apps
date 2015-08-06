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
    #region Interface Implement : AppWidgetRolePermission

    public class AppWidgetRolePermissionRepository : IAppWidgetRolePermissionRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<AppWidgetRolePermission> _appWidgetPermissionRepository;
        private readonly RepositoryBase<AppWidgetRolePermission> _appWidgetPermissionRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public AppWidgetRolePermissionRepository(Repository<AppWidgetRolePermission> appWidgetPermissionRepository, IUnitOfWork iUnitOfWork)
        {
            this._appWidgetPermissionRepository = appWidgetPermissionRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<AppWidgetRolePermissionViewModel> GetAll()
        {
            var appWidgetRolePermissionViewModels = new List<AppWidgetRolePermissionViewModel>();
            try
            {

                List<AppWidgetRolePermission> appWidgetPermissions = _appWidgetPermissionRepository.GetAll();

                foreach (AppWidgetRolePermission appWidgetPermission in appWidgetPermissions)
                {
                    var appWidgetRolePermissionViewModel = appWidgetPermission.ConvertModelToViewModel<AppWidgetRolePermission, AppWidgetRolePermissionViewModel>();
                    appWidgetRolePermissionViewModels.Add(appWidgetRolePermissionViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appWidgetRolePermissionViewModels.AsQueryable();
        }

        public AppWidgetRolePermissionViewModel GetById(long id)
        {
            var appWidgetRolePermissionViewModel = new AppWidgetRolePermissionViewModel();

            try
            {
                AppWidgetRolePermission appWidgetPermission = _appWidgetPermissionRepository.GetById(id);
                appWidgetRolePermissionViewModel = appWidgetPermission.ConvertModelToViewModel<AppWidgetRolePermission, AppWidgetRolePermissionViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appWidgetRolePermissionViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(AppWidgetRolePermissionViewModel appWidgetRolePermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetRolePermissionViewModel != null)
                {
                    //add
                    if (appWidgetRolePermissionViewModel.AppWidgetPermissionId == 0 && appWidgetRolePermissionViewModel.ActionName == "Add")
                    {
                        Create(appWidgetRolePermissionViewModel);
                    }
                    else if (appWidgetRolePermissionViewModel.ActionName == "Edit") //edit
                    {
                        Update(appWidgetRolePermissionViewModel);
                    }
                }
                else
                {
                    throw new ArgumentNullException("AppWidgetPermissionViewModel", "Request data is null.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(AppWidgetRolePermissionViewModel appWidgetRolePermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetRolePermissionViewModel != null)
                {
                    AppWidgetRolePermission appWidgetPermission = appWidgetRolePermissionViewModel.ConvertViewModelToModel<AppWidgetRolePermissionViewModel, AppWidgetRolePermission>();
                    _appWidgetPermissionRepository.Insert(appWidgetPermission);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppWidgetPermissionViewModel", "Request data is null.");
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

        public int Update(AppWidgetRolePermissionViewModel appWidgetRolePermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetRolePermissionViewModel != null)
                {
                    AppWidgetRolePermission appWidgetPermission = appWidgetRolePermissionViewModel.ConvertViewModelToModel<AppWidgetRolePermissionViewModel, AppWidgetRolePermission>();
                    _appWidgetPermissionRepository.Update(appWidgetPermission);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppWidgetPermissionViewModel", "Request data is null.");
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

        public int Delete(AppWidgetRolePermissionViewModel appWidgetRolePermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetRolePermissionViewModel != null)
                {
                    var viewModel = GetById(appWidgetRolePermissionViewModel.AppWidgetPermissionId);
                    AppWidgetRolePermission appWidgetPermission = viewModel.ConvertViewModelToModel<AppWidgetRolePermissionViewModel, AppWidgetRolePermission>();
                    _appWidgetPermissionRepository.Delete(appWidgetPermission);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppWidgetPermissionViewModel", "Request data is null.");
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
                var appWidgetRolePermissionViewModel = GetById(id);
                if (appWidgetRolePermissionViewModel != null)
                {
                    AppWidgetRolePermission appWidgetPermission = appWidgetRolePermissionViewModel.ConvertViewModelToModel<AppWidgetRolePermissionViewModel, AppWidgetRolePermission>();
                    _appWidgetPermissionRepository.Delete(appWidgetPermission);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppWidgetPermissionViewModel", "Request data is null.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<AppWidgetRolePermissionViewModel> appWidgetRolePermissionViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appWidgetRolePermissionViewModel in appWidgetRolePermissionViewModels)
                {
                    AppWidgetRolePermissionViewModel viewModel = GetById(appWidgetRolePermissionViewModel.AppWidgetPermissionId);
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

    #region Interface : AppWidgetRolePermission

    public interface IAppWidgetRolePermissionRepository : IGeneric<AppWidgetRolePermissionViewModel>
    {
        int Delete(List<AppWidgetRolePermissionViewModel> appWidgetRolePermissionViewModels);
    }

    #endregion
}
