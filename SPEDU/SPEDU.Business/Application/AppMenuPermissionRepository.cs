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
    #region Interface Implement : AppMenuPermission

    public class AppMenuPermissionRepository : IAppMenuPermissionRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<AppMenuPermission> _appMenuPermissionRepository;
        private readonly RepositoryBase<AppMenuPermission> _appMenuPermissionRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public AppMenuPermissionRepository(Repository<AppMenuPermission> appMenuPermissionRepository, IUnitOfWork iUnitOfWork)
        {
            this._appMenuPermissionRepository = appMenuPermissionRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<AppMenuPermissionViewModel> GetAll()
        {
            var appMenuPermissionViewModels = new List<AppMenuPermissionViewModel>();
            try
            {

                List<AppMenuPermission> appMenuPermissions = _appMenuPermissionRepository.GetAll();

                foreach (AppMenuPermission appMenuPermission in appMenuPermissions)
                {
                    var appMenuPermissionViewModel = appMenuPermission.ConvertModelToViewModel<AppMenuPermission, AppMenuPermissionViewModel>();
                    appMenuPermissionViewModels.Add(appMenuPermissionViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appMenuPermissionViewModels.AsQueryable();
        }

        public AppMenuPermissionViewModel GetById(long id)
        {
            var appMenuPermissionViewModel = new AppMenuPermissionViewModel();

            try
            {
                AppMenuPermission appMenuPermission = _appMenuPermissionRepository.GetById(id);
                appMenuPermissionViewModel = appMenuPermission.ConvertModelToViewModel<AppMenuPermission, AppMenuPermissionViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appMenuPermissionViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(AppMenuPermissionViewModel appMenuPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appMenuPermissionViewModel != null)
                {
                    //add
                    if (appMenuPermissionViewModel.AppMenuPermissionId == 0 && appMenuPermissionViewModel.ActionName == "Add")
                    {
                        Create(appMenuPermissionViewModel);
                    }
                    else if (appMenuPermissionViewModel.ActionName == "Edit") //edit
                    {
                        Update(appMenuPermissionViewModel);
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
        public int Create(AppMenuPermissionViewModel appMenuPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appMenuPermissionViewModel != null)
                {
                    AppMenuPermission appMenuPermission = appMenuPermissionViewModel.ConvertViewModelToModel<AppMenuPermissionViewModel, AppMenuPermission>();
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

        public int Update(AppMenuPermissionViewModel appMenuPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appMenuPermissionViewModel != null)
                {
                    AppMenuPermission appMenuPermission = appMenuPermissionViewModel.ConvertViewModelToModel<AppMenuPermissionViewModel, AppMenuPermission>();
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

        public int Delete(AppMenuPermissionViewModel appMenuPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appMenuPermissionViewModel != null)
                {
                    var viewModel = GetById(appMenuPermissionViewModel.AppMenuPermissionId);
                    AppMenuPermission appMenuPermission = viewModel.ConvertViewModelToModel<AppMenuPermissionViewModel, AppMenuPermission>();
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
                var appMenuPermissionViewModel = GetById(id);
                if (appMenuPermissionViewModel != null)
                {
                    AppMenuPermission appMenuPermission = appMenuPermissionViewModel.ConvertViewModelToModel<AppMenuPermissionViewModel, AppMenuPermission>();
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

        public int Delete(List<AppMenuPermissionViewModel> appMenuPermissionViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appMenuPermissionViewModel in appMenuPermissionViewModels)
                {
                    AppMenuPermissionViewModel viewModel = GetById(appMenuPermissionViewModel.AppMenuPermissionId);
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

    #region Interface : AppMenuPermission

    public interface IAppMenuPermissionRepository : IGeneric<AppMenuPermissionViewModel>
    {
        int Delete(List<AppMenuPermissionViewModel> appMenuPermissionViewModels);
    }

    #endregion
}
