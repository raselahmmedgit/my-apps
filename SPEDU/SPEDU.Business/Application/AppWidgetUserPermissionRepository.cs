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
    #region Interface Implement : AppWidgetUserPermission

    public class AppWidgetUserPermissionRepository : IAppWidgetUserPermissionRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<AppWidgetUserPermission> _appWidgetPermissionRepository;
        private readonly RepositoryBase<AppWidgetUserPermission> _appWidgetPermissionRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public AppWidgetUserPermissionRepository(Repository<AppWidgetUserPermission> appWidgetPermissionRepository, IUnitOfWork iUnitOfWork)
        {
            this._appWidgetPermissionRepository = appWidgetPermissionRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<AppWidgetUserPermissionViewModel> GetAll()
        {
            var appWidgetUserPermissionViewModels = new List<AppWidgetUserPermissionViewModel>();
            try
            {

                List<AppWidgetUserPermission> appWidgetPermissions = _appWidgetPermissionRepository.GetAll();

                foreach (AppWidgetUserPermission appWidgetPermission in appWidgetPermissions)
                {
                    var appWidgetUserPermissionViewModel = appWidgetPermission.ConvertModelToViewModel<AppWidgetUserPermission, AppWidgetUserPermissionViewModel>();
                    appWidgetUserPermissionViewModels.Add(appWidgetUserPermissionViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appWidgetUserPermissionViewModels.AsQueryable();
        }

        public AppWidgetUserPermissionViewModel GetById(long id)
        {
            var appWidgetUserPermissionViewModel = new AppWidgetUserPermissionViewModel();

            try
            {
                AppWidgetUserPermission appWidgetPermission = _appWidgetPermissionRepository.GetById(id);
                appWidgetUserPermissionViewModel = appWidgetPermission.ConvertModelToViewModel<AppWidgetUserPermission, AppWidgetUserPermissionViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appWidgetUserPermissionViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(AppWidgetUserPermissionViewModel appWidgetUserPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetUserPermissionViewModel != null)
                {
                    //add
                    if (appWidgetUserPermissionViewModel.AppWidgetPermissionId == 0 && appWidgetUserPermissionViewModel.ActionName == "Add")
                    {
                        Create(appWidgetUserPermissionViewModel);
                    }
                    else if (appWidgetUserPermissionViewModel.ActionName == "Edit") //edit
                    {
                        Update(appWidgetUserPermissionViewModel);
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
        public int Create(AppWidgetUserPermissionViewModel appWidgetUserPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetUserPermissionViewModel != null)
                {
                    AppWidgetUserPermission appWidgetPermission = appWidgetUserPermissionViewModel.ConvertViewModelToModel<AppWidgetUserPermissionViewModel, AppWidgetUserPermission>();
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

        public int Update(AppWidgetUserPermissionViewModel appWidgetUserPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetUserPermissionViewModel != null)
                {
                    AppWidgetUserPermission appWidgetPermission = appWidgetUserPermissionViewModel.ConvertViewModelToModel<AppWidgetUserPermissionViewModel, AppWidgetUserPermission>();
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

        public int Delete(AppWidgetUserPermissionViewModel appWidgetUserPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetUserPermissionViewModel != null)
                {
                    var viewModel = GetById(appWidgetUserPermissionViewModel.AppWidgetPermissionId);
                    AppWidgetUserPermission appWidgetPermission = viewModel.ConvertViewModelToModel<AppWidgetUserPermissionViewModel, AppWidgetUserPermission>();
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
                var appWidgetUserPermissionViewModel = GetById(id);
                if (appWidgetUserPermissionViewModel != null)
                {
                    AppWidgetUserPermission appWidgetPermission = appWidgetUserPermissionViewModel.ConvertViewModelToModel<AppWidgetUserPermissionViewModel, AppWidgetUserPermission>();
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

        public int Delete(List<AppWidgetUserPermissionViewModel> appWidgetUserPermissionViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appWidgetUserPermissionViewModel in appWidgetUserPermissionViewModels)
                {
                    AppWidgetUserPermissionViewModel viewModel = GetById(appWidgetUserPermissionViewModel.AppWidgetPermissionId);
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

    #region Interface : AppWidgetUserPermission

    public interface IAppWidgetUserPermissionRepository : IGeneric<AppWidgetUserPermissionViewModel>
    {
        int Delete(List<AppWidgetUserPermissionViewModel> appWidgetUserPermissionViewModels);
    }

    #endregion
}
