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
    #region Interface Implement : AppRightUserPermission

    public class AppRightUserPermissionRepository : IAppRightUserPermissionRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<AppRightUserPermission> _appRightPermissionRepository;
        private readonly RepositoryBase<AppRightUserPermission> _appRightPermissionRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public AppRightUserPermissionRepository(Repository<AppRightUserPermission> appRightPermissionRepository, IUnitOfWork iUnitOfWork)
        {
            this._appRightPermissionRepository = appRightPermissionRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<AppRightUserPermissionViewModel> GetAll()
        {
            var appRightUserPermissionViewModels = new List<AppRightUserPermissionViewModel>();
            try
            {

                List<AppRightUserPermission> appRightPermissions = _appRightPermissionRepository.GetAll();

                foreach (AppRightUserPermission appRightPermission in appRightPermissions)
                {
                    var appRightUserPermissionViewModel = appRightPermission.ConvertModelToViewModel<AppRightUserPermission, AppRightUserPermissionViewModel>();
                    appRightUserPermissionViewModels.Add(appRightUserPermissionViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appRightUserPermissionViewModels.AsQueryable();
        }

        public AppRightUserPermissionViewModel GetById(long id)
        {
            var appRightUserPermissionViewModel = new AppRightUserPermissionViewModel();

            try
            {
                AppRightUserPermission appRightPermission = _appRightPermissionRepository.GetById(id);
                appRightUserPermissionViewModel = appRightPermission.ConvertModelToViewModel<AppRightUserPermission, AppRightUserPermissionViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appRightUserPermissionViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(AppRightUserPermissionViewModel appRightUserPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appRightUserPermissionViewModel != null)
                {
                    //add
                    if (appRightUserPermissionViewModel.AppRightUserPermissionId == default(int))
                    {
                        Create(appRightUserPermissionViewModel);
                    }
                    else //edit
                    {
                        Update(appRightUserPermissionViewModel);
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
        public int Create(AppRightUserPermissionViewModel appRightUserPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appRightUserPermissionViewModel != null)
                {
                    AppRightUserPermission appRightPermission = appRightUserPermissionViewModel.ConvertViewModelToModel<AppRightUserPermissionViewModel, AppRightUserPermission>();
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

        public int Update(AppRightUserPermissionViewModel appRightUserPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appRightUserPermissionViewModel != null)
                {
                    AppRightUserPermission appRightPermission = appRightUserPermissionViewModel.ConvertViewModelToModel<AppRightUserPermissionViewModel, AppRightUserPermission>();
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

        public int Delete(AppRightUserPermissionViewModel appRightUserPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appRightUserPermissionViewModel != null)
                {
                    var viewModel = GetById(appRightUserPermissionViewModel.AppRightUserPermissionId);
                    AppRightUserPermission appRightPermission = viewModel.ConvertViewModelToModel<AppRightUserPermissionViewModel, AppRightUserPermission>();
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
                var appRightUserPermissionViewModel = GetById(id);
                if (appRightUserPermissionViewModel != null)
                {
                    AppRightUserPermission appRightPermission = appRightUserPermissionViewModel.ConvertViewModelToModel<AppRightUserPermissionViewModel, AppRightUserPermission>();
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

        public int Delete(List<AppRightUserPermissionViewModel> appRightUserPermissionViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appRightUserPermissionViewModel in appRightUserPermissionViewModels)
                {
                    AppRightUserPermissionViewModel viewModel = GetById(appRightUserPermissionViewModel.AppRightUserPermissionId);
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

    #region Interface : AppRightUserPermission

    public interface IAppRightUserPermissionRepository : IGeneric<AppRightUserPermissionViewModel>
    {
        int Delete(List<AppRightUserPermissionViewModel> appRightUserPermissionViewModels);
    }

    #endregion
}
