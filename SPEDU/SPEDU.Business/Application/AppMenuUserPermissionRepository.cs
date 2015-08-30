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
    #region Interface Implement : AppMenuUserPermission

    public class AppMenuUserPermissionRepository : IAppMenuUserPermissionRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<AppMenuUserPermission> _appMenuPermissionRepository;
        private readonly RepositoryBase<AppMenuUserPermission> _appMenuPermissionRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public AppMenuUserPermissionRepository(Repository<AppMenuUserPermission> appMenuPermissionRepository, IUnitOfWork iUnitOfWork)
        {
            this._appMenuPermissionRepository = appMenuPermissionRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<AppMenuUserPermissionViewModel> GetAll()
        {
            var appMenuUserPermissionViewModels = new List<AppMenuUserPermissionViewModel>();
            try
            {

                List<AppMenuUserPermission> appMenuPermissions = _appMenuPermissionRepository.GetAll();

                foreach (AppMenuUserPermission appMenuPermission in appMenuPermissions)
                {
                    var appMenuUserPermissionViewModel = appMenuPermission.ConvertModelToViewModel<AppMenuUserPermission, AppMenuUserPermissionViewModel>();
                    appMenuUserPermissionViewModels.Add(appMenuUserPermissionViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appMenuUserPermissionViewModels.AsQueryable();
        }

        public AppMenuUserPermissionViewModel GetById(long id)
        {
            var appMenuUserPermissionViewModel = new AppMenuUserPermissionViewModel();

            try
            {
                AppMenuUserPermission appMenuPermission = _appMenuPermissionRepository.GetById(id);
                appMenuUserPermissionViewModel = appMenuPermission.ConvertModelToViewModel<AppMenuUserPermission, AppMenuUserPermissionViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appMenuUserPermissionViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(AppMenuUserPermissionViewModel appMenuUserPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appMenuUserPermissionViewModel != null)
                {
                    //add
                    if (appMenuUserPermissionViewModel.AppMenuUserPermissionId == default(int))
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
                    throw new ArgumentNullException("AppMenuPermissionViewModel", "Request data is null.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(AppMenuUserPermissionViewModel appMenuUserPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appMenuUserPermissionViewModel != null)
                {
                    AppMenuUserPermission appMenuPermission = appMenuUserPermissionViewModel.ConvertViewModelToModel<AppMenuUserPermissionViewModel, AppMenuUserPermission>();
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

        public int Update(AppMenuUserPermissionViewModel appMenuUserPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appMenuUserPermissionViewModel != null)
                {
                    AppMenuUserPermission appMenuPermission = appMenuUserPermissionViewModel.ConvertViewModelToModel<AppMenuUserPermissionViewModel, AppMenuUserPermission>();
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

        public int Delete(AppMenuUserPermissionViewModel appMenuUserPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appMenuUserPermissionViewModel != null)
                {
                    var viewModel = GetById(appMenuUserPermissionViewModel.AppMenuUserPermissionId);
                    AppMenuUserPermission appMenuPermission = viewModel.ConvertViewModelToModel<AppMenuUserPermissionViewModel, AppMenuUserPermission>();
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
                var appMenuUserPermissionViewModel = GetById(id);
                if (appMenuUserPermissionViewModel != null)
                {
                    AppMenuUserPermission appMenuPermission = appMenuUserPermissionViewModel.ConvertViewModelToModel<AppMenuUserPermissionViewModel, AppMenuUserPermission>();
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

        public int Delete(List<AppMenuUserPermissionViewModel> appMenuUserPermissionViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appMenuUserPermissionViewModel in appMenuUserPermissionViewModels)
                {
                    AppMenuUserPermissionViewModel viewModel = GetById(appMenuUserPermissionViewModel.AppMenuUserPermissionId);
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

    #region Interface : AppMenuUserPermission

    public interface IAppMenuUserPermissionRepository : IGeneric<AppMenuUserPermissionViewModel>
    {
        int Delete(List<AppMenuUserPermissionViewModel> appMenuUserPermissionViewModels);
    }

    #endregion
}
