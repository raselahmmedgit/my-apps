using System;
using System.Collections.Generic;
using System.Linq;
using SPEDU.Domain.Extensions;
using SPEDU.Domain.Models.Application;
using SPEDU.Data.Infrastructure;
using SPEDU.Data.Repositories;
using SPEDU.DomainViewModel.Application;
using SPEDU.Common.Utility;
using SPEDU.Common.Manager;

namespace SPEDU.Business.Application
{
    #region Interface Implement : AppMenu

    public class AppMenuRepository : IAppMenuRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<AppMenu> _appMenuRepository;
        private readonly RepositoryBase<AppMenu> _appMenuRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public AppMenuRepository(Repository<AppMenu> appMenuRepository, IUnitOfWork iUnitOfWork)
        {
            this._appMenuRepository = appMenuRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method By Cache

        public IQueryable<AppMenuViewModel> GetAllByCache()
        {
            var appMenuViewModels = new List<AppMenuViewModel>();
            try
            {

                List<AppMenu> appMenuList = new List<AppMenu>();

                string cacheKey = AppConstant.CacheKey.AllAppMenu;
                if (!CacheManager.ICache.IsSet(cacheKey))
                {
                    appMenuList = _appMenuRepository.GetAll();
                    CacheManager.ICache.Set(cacheKey, appMenuList);
                }
                else
                {
                    appMenuList = CacheManager.ICache.Get(cacheKey) as List<AppMenu>;
                }

                if (appMenuList != null)
                {
                    foreach (AppMenu appMenu in appMenuList)
                    {
                        var appMenuViewModel = appMenu.ConvertModelToViewModel<AppMenu, AppMenuViewModel>();
                        appMenuViewModels.Add(appMenuViewModel);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appMenuViewModels.AsQueryable();
        }

        #endregion

        #region Get Method

        public IQueryable<AppMenuViewModel> GetAll()
        {
            var appMenuViewModels = new List<AppMenuViewModel>();
            try
            {

                List<AppMenu> appMenus = _appMenuRepository.GetAll();

                foreach (AppMenu appMenu in appMenus)
                {
                    var appMenuViewModel = appMenu.ConvertModelToViewModel<AppMenu, AppMenuViewModel>();
                    appMenuViewModels.Add(appMenuViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appMenuViewModels.AsQueryable();
        }

        public AppMenuViewModel GetById(long id)
        {
            var appMenuViewModel = new AppMenuViewModel();

            try
            {
                AppMenu appMenu = _appMenuRepository.GetById(id);
                appMenuViewModel = appMenu.ConvertModelToViewModel<AppMenu, AppMenuViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appMenuViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(AppMenuViewModel appMenuViewModel)
        {
            int isSave = 0;
            try
            {
                if (appMenuViewModel != null)
                {
                    //add
                    if (appMenuViewModel.AppMenuId == 0 && appMenuViewModel.ActionName == "Add")
                    {
                        Create(appMenuViewModel);
                    }
                    else if (appMenuViewModel.ActionName == "Edit") //edit
                    {
                        Update(appMenuViewModel);
                    }
                }
                else
                {
                    throw new ArgumentNullException("AppMenuViewModel", "Request data is null.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(AppMenuViewModel appMenuViewModel)
        {
            int isSave = 0;
            try
            {
                if (appMenuViewModel != null)
                {
                    AppMenu appMenu = appMenuViewModel.ConvertViewModelToModel<AppMenuViewModel, AppMenu>();
                    _appMenuRepository.Insert(appMenu);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppMenuViewModel", "Request data is null.");
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

        public int Update(AppMenuViewModel appMenuViewModel)
        {
            int isSave = 0;
            try
            {
                if (appMenuViewModel != null)
                {
                    AppMenu appMenu = appMenuViewModel.ConvertViewModelToModel<AppMenuViewModel, AppMenu>();
                    _appMenuRepository.Update(appMenu);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppMenuViewModel", "Request data is null.");
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

        public int Delete(AppMenuViewModel appMenuViewModel)
        {
            int isSave = 0;
            try
            {
                if (appMenuViewModel != null)
                {
                    var viewModel = GetById(appMenuViewModel.AppMenuId);
                    AppMenu appMenu = viewModel.ConvertViewModelToModel<AppMenuViewModel, AppMenu>();
                    _appMenuRepository.Delete(appMenu);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppMenuViewModel", "Request data is null.");
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
                var appMenuViewModel = GetById(id);
                if (appMenuViewModel != null)
                {
                    AppMenu appMenu = appMenuViewModel.ConvertViewModelToModel<AppMenuViewModel, AppMenu>();
                    _appMenuRepository.Delete(appMenu);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppMenuViewModel", "Request data is null.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<AppMenuViewModel> appMenuViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appMenuViewModel in appMenuViewModels)
                {
                    AppMenuViewModel viewModel = GetById(appMenuViewModel.AppMenuId);
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

    #region Interface : AppMenu

    public interface IAppMenuRepository : IGeneric<AppMenuViewModel>
    {
        IQueryable<AppMenuViewModel> GetAllByCache();

        int Delete(List<AppMenuViewModel> appMenuViewModels);
    }

    #endregion
}
