using System;
using System.Collections.Generic;
using System.Linq;
using SPEDU.Common.Helper;
using SPEDU.Domain.Extensions;
using SPEDU.Domain.Models.Application;
using SPEDU.Data.Infrastructure;
using SPEDU.Data.Repositories;
using SPEDU.DomainViewModel.Application;
using SPEDU.Common.Utility;
using SPEDU.Common.Manager;

namespace SPEDU.Business.Application
{
    #region Interface Implement : Menu

    public class MenuRepository : IMenuRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<Menu> _appMenuRepository;
        private readonly RepositoryBase<Menu> _appMenuRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public MenuRepository(Repository<Menu> appMenuRepository, IUnitOfWork iUnitOfWork)
        {
            this._appMenuRepository = appMenuRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method By Cache

        public IQueryable<MenuViewModel> GetAllByCache()
        {
            var appMenuViewModels = new List<MenuViewModel>();
            try
            {

                List<Menu> appMenuList = new List<Menu>();

                string cacheKey = AppConstant.CacheKey.AllMenu;
                if (!CacheManager.ICache.IsSet(cacheKey))
                {
                    appMenuList = _appMenuRepository.GetAll();
                    CacheManager.ICache.Set(cacheKey, appMenuList);
                }
                else
                {
                    appMenuList = CacheManager.ICache.Get(cacheKey) as List<Menu>;
                }

                if (appMenuList != null)
                {
                    foreach (Menu appMenu in appMenuList)
                    {
                        var appMenuViewModel = appMenu.ConvertModelToViewModel<Menu, MenuViewModel>();
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

        public IQueryable<MenuViewModel> GetAll()
        {
            var appMenuViewModels = new List<MenuViewModel>();
            try
            {

                List<Menu> appMenus = _appMenuRepository.GetAll();

                foreach (Menu appMenu in appMenus)
                {
                    var appMenuViewModel = appMenu.ConvertModelToViewModel<Menu, MenuViewModel>();
                    appMenuViewModels.Add(appMenuViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appMenuViewModels.AsQueryable();
        }

        public MenuViewModel GetById(long id)
        {
            var appMenuViewModel = new MenuViewModel();

            try
            {
                Menu appMenu = _appMenuRepository.GetById(id);
                appMenuViewModel = appMenu.ConvertModelToViewModel<Menu, MenuViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appMenuViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(MenuViewModel appMenuViewModel)
        {
            int isSave = 0;
            try
            {
                if (appMenuViewModel != null)
                {
                    //add
                    if (appMenuViewModel.MenuId == 0 && appMenuViewModel.ActionName == "Add")
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
                    throw new ArgumentNullException("MenuViewModel", MessageResourceHelper.NullError);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(MenuViewModel appMenuViewModel)
        {
            int isSave = 0;
            try
            {
                if (appMenuViewModel != null)
                {
                    Menu appMenu = appMenuViewModel.ConvertViewModelToModel<MenuViewModel, Menu>();
                    _appMenuRepository.Insert(appMenu);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("MenuViewModel", MessageResourceHelper.NullError);
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

        public int Update(MenuViewModel appMenuViewModel)
        {
            int isSave = 0;
            try
            {
                if (appMenuViewModel != null)
                {
                    Menu appMenu = appMenuViewModel.ConvertViewModelToModel<MenuViewModel, Menu>();
                    _appMenuRepository.Update(appMenu);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("MenuViewModel", MessageResourceHelper.NullError);
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

        public int Delete(MenuViewModel appMenuViewModel)
        {
            int isSave = 0;
            try
            {
                if (appMenuViewModel != null)
                {
                    var viewModel = GetById(appMenuViewModel.MenuId);
                    Menu appMenu = viewModel.ConvertViewModelToModel<MenuViewModel, Menu>();
                    _appMenuRepository.Delete(appMenu);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("MenuViewModel", MessageResourceHelper.NullError);
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
                    Menu appMenu = appMenuViewModel.ConvertViewModelToModel<MenuViewModel, Menu>();
                    _appMenuRepository.Delete(appMenu);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("MenuViewModel", MessageResourceHelper.NullError);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<MenuViewModel> appMenuViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appMenuViewModel in appMenuViewModels)
                {
                    MenuViewModel viewModel = GetById(appMenuViewModel.MenuId);
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

    #region Interface : Menu

    public interface IMenuRepository : IGeneric<MenuViewModel>
    {
        IQueryable<MenuViewModel> GetAllByCache();

        int Delete(List<MenuViewModel> appMenuViewModels);
    }

    #endregion
}
