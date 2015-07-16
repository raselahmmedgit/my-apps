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
    #region Interface Implement : AppWidgetPermission

    public class AppWidgetPermissionRepository : IAppWidgetPermissionRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<AppWidgetPermission> _appWidgetPermissionRepository;
        private readonly RepositoryBase<AppWidgetPermission> _appWidgetPermissionRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public AppWidgetPermissionRepository(Repository<AppWidgetPermission> appWidgetPermissionRepository, IUnitOfWork iUnitOfWork)
        {
            this._appWidgetPermissionRepository = appWidgetPermissionRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<AppWidgetPermissionViewModel> GetAll()
        {
            var appWidgetPermissionViewModels = new List<AppWidgetPermissionViewModel>();
            try
            {

                List<AppWidgetPermission> appWidgetPermissions = _appWidgetPermissionRepository.GetAll();

                foreach (AppWidgetPermission appWidgetPermission in appWidgetPermissions)
                {
                    var appWidgetPermissionViewModel = appWidgetPermission.ConvertModelToViewModel<AppWidgetPermission, AppWidgetPermissionViewModel>();
                    appWidgetPermissionViewModels.Add(appWidgetPermissionViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appWidgetPermissionViewModels.AsQueryable();
        }

        public AppWidgetPermissionViewModel GetById(long id)
        {
            var appWidgetPermissionViewModel = new AppWidgetPermissionViewModel();

            try
            {
                AppWidgetPermission appWidgetPermission = _appWidgetPermissionRepository.GetById(id);
                appWidgetPermissionViewModel = appWidgetPermission.ConvertModelToViewModel<AppWidgetPermission, AppWidgetPermissionViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appWidgetPermissionViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(AppWidgetPermissionViewModel appWidgetPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetPermissionViewModel != null)
                {
                    //add
                    if (appWidgetPermissionViewModel.AppWidgetPermissionId == 0 && appWidgetPermissionViewModel.ActionName == "Add")
                    {
                        Create(appWidgetPermissionViewModel);
                    }
                    else if (appWidgetPermissionViewModel.ActionName == "Edit") //edit
                    {
                        Update(appWidgetPermissionViewModel);
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
        public int Create(AppWidgetPermissionViewModel appWidgetPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetPermissionViewModel != null)
                {
                    AppWidgetPermission appWidgetPermission = appWidgetPermissionViewModel.ConvertViewModelToModel<AppWidgetPermissionViewModel, AppWidgetPermission>();
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

        public int Update(AppWidgetPermissionViewModel appWidgetPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetPermissionViewModel != null)
                {
                    AppWidgetPermission appWidgetPermission = appWidgetPermissionViewModel.ConvertViewModelToModel<AppWidgetPermissionViewModel, AppWidgetPermission>();
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

        public int Delete(AppWidgetPermissionViewModel appWidgetPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetPermissionViewModel != null)
                {
                    var viewModel = GetById(appWidgetPermissionViewModel.AppWidgetPermissionId);
                    AppWidgetPermission appWidgetPermission = viewModel.ConvertViewModelToModel<AppWidgetPermissionViewModel, AppWidgetPermission>();
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
                var appWidgetPermissionViewModel = GetById(id);
                if (appWidgetPermissionViewModel != null)
                {
                    AppWidgetPermission appWidgetPermission = appWidgetPermissionViewModel.ConvertViewModelToModel<AppWidgetPermissionViewModel, AppWidgetPermission>();
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

        public int Delete(List<AppWidgetPermissionViewModel> appWidgetPermissionViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appWidgetPermissionViewModel in appWidgetPermissionViewModels)
                {
                    AppWidgetPermissionViewModel viewModel = GetById(appWidgetPermissionViewModel.AppWidgetPermissionId);
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

    #region Interface : AppWidgetPermission

    public interface IAppWidgetPermissionRepository : IGeneric<AppWidgetPermissionViewModel>
    {
        int Delete(List<AppWidgetPermissionViewModel> appWidgetPermissionViewModels);
    }

    #endregion
}
