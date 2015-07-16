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
    #region Interface Implement : AppRightPermission

    public class AppRightPermissionRepository : IAppRightPermissionRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<AppRightPermission> _appRightPermissionRepository;
        private readonly RepositoryBase<AppRightPermission> _appRightPermissionRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public AppRightPermissionRepository(Repository<AppRightPermission> appRightPermissionRepository, IUnitOfWork iUnitOfWork)
        {
            this._appRightPermissionRepository = appRightPermissionRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<AppRightPermissionViewModel> GetAll()
        {
            var appRightPermissionViewModels = new List<AppRightPermissionViewModel>();
            try
            {

                List<AppRightPermission> appRightPermissions = _appRightPermissionRepository.GetAll();

                foreach (AppRightPermission appRightPermission in appRightPermissions)
                {
                    var appRightPermissionViewModel = appRightPermission.ConvertModelToViewModel<AppRightPermission, AppRightPermissionViewModel>();
                    appRightPermissionViewModels.Add(appRightPermissionViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appRightPermissionViewModels.AsQueryable();
        }

        public AppRightPermissionViewModel GetById(long id)
        {
            var appRightPermissionViewModel = new AppRightPermissionViewModel();

            try
            {
                AppRightPermission appRightPermission = _appRightPermissionRepository.GetById(id);
                appRightPermissionViewModel = appRightPermission.ConvertModelToViewModel<AppRightPermission, AppRightPermissionViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appRightPermissionViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(AppRightPermissionViewModel appRightPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appRightPermissionViewModel != null)
                {
                    //add
                    if (appRightPermissionViewModel.AppRightPermissionId == 0 && appRightPermissionViewModel.ActionName == "Add")
                    {
                        Create(appRightPermissionViewModel);
                    }
                    else if (appRightPermissionViewModel.ActionName == "Edit") //edit
                    {
                        Update(appRightPermissionViewModel);
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
        public int Create(AppRightPermissionViewModel appRightPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appRightPermissionViewModel != null)
                {
                    AppRightPermission appRightPermission = appRightPermissionViewModel.ConvertViewModelToModel<AppRightPermissionViewModel, AppRightPermission>();
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

        public int Update(AppRightPermissionViewModel appRightPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appRightPermissionViewModel != null)
                {
                    AppRightPermission appRightPermission = appRightPermissionViewModel.ConvertViewModelToModel<AppRightPermissionViewModel, AppRightPermission>();
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

        public int Delete(AppRightPermissionViewModel appRightPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appRightPermissionViewModel != null)
                {
                    var viewModel = GetById(appRightPermissionViewModel.AppRightPermissionId);
                    AppRightPermission appRightPermission = viewModel.ConvertViewModelToModel<AppRightPermissionViewModel, AppRightPermission>();
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
                var appRightPermissionViewModel = GetById(id);
                if (appRightPermissionViewModel != null)
                {
                    AppRightPermission appRightPermission = appRightPermissionViewModel.ConvertViewModelToModel<AppRightPermissionViewModel, AppRightPermission>();
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

        public int Delete(List<AppRightPermissionViewModel> appRightPermissionViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appRightPermissionViewModel in appRightPermissionViewModels)
                {
                    AppRightPermissionViewModel viewModel = GetById(appRightPermissionViewModel.AppRightPermissionId);
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

    #region Interface : AppRightPermission

    public interface IAppRightPermissionRepository : IGeneric<AppRightPermissionViewModel>
    {
        int Delete(List<AppRightPermissionViewModel> appRightPermissionViewModels);
    }

    #endregion
}
