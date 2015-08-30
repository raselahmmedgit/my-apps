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
    #region Interface Implement : AppApplicationInfo

    public class AppApplicationInfoRepository : IAppApplicationInfoRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<AppApplicationInfo> _appApplicationInfoRepository;
        private readonly RepositoryBase<AppApplicationInfo> _appApplicationInfoRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public AppApplicationInfoRepository(Repository<AppApplicationInfo> appInformationRepository, IUnitOfWork iUnitOfWork)
        {
            this._appApplicationInfoRepository = appInformationRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method By Cache

        public IQueryable<AppApplicationInfoViewModel> GetAllFromCache()
        {
            var appApplicationInfoViewModels = new List<AppApplicationInfoViewModel>();
            try
            {

                List<AppApplicationInfo> appApplicationInfoList = new List<AppApplicationInfo>();

                string cacheKey = AppConstant.CacheKey.AllAppApplicationInfo;
                if (!CacheManager.ICache.IsSet(cacheKey))
                {
                    appApplicationInfoList = _appApplicationInfoRepository.GetAll();
                    CacheManager.ICache.Set(cacheKey, appApplicationInfoList);
                }
                else
                {
                    appApplicationInfoList = CacheManager.ICache.Get(cacheKey) as List<AppApplicationInfo>;
                }

                if (appApplicationInfoList != null)
                {
                    foreach (AppApplicationInfo appApplicationInfo in appApplicationInfoList)
                    {
                        var appMenuViewModel = appApplicationInfo.ConvertModelToViewModel<AppApplicationInfo, AppApplicationInfoViewModel>();
                        appApplicationInfoViewModels.Add(appMenuViewModel);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appApplicationInfoViewModels.AsQueryable();
        }

        public AppApplicationInfoViewModel GetByIdFromCache(long id)
        {
            var appApplicationInfoViewModel = new AppApplicationInfoViewModel();

            try
            {
                AppApplicationInfo appInformation = GetAllFromCache().FirstOrDefault(item => item.AppInformationId == id);
                appApplicationInfoViewModel = appInformation.ConvertModelToViewModel<AppApplicationInfo, AppApplicationInfoViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appApplicationInfoViewModel;
        }

        #endregion

        #region Get Method

        public IQueryable<AppApplicationInfoViewModel> GetAll()
        {
            var appInformationViewModels = new List<AppApplicationInfoViewModel>();
            try
            {

                List<AppApplicationInfo> appApplicationInfos = _appApplicationInfoRepository.GetAll();

                foreach (AppApplicationInfo appInformation in appApplicationInfos)
                {
                    var appApplicationInfoViewModel = appInformation.ConvertModelToViewModel<AppApplicationInfo, AppApplicationInfoViewModel>();
                    appInformationViewModels.Add(appApplicationInfoViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appInformationViewModels.AsQueryable();
        }

        public AppApplicationInfoViewModel GetById(long id)
        {
            var appApplicationInfoViewModel = new AppApplicationInfoViewModel();

            try
            {
                AppApplicationInfo appInformation = _appApplicationInfoRepository.GetById(id);
                appApplicationInfoViewModel = appInformation.ConvertModelToViewModel<AppApplicationInfo, AppApplicationInfoViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appApplicationInfoViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(AppApplicationInfoViewModel appApplicationInfoViewModel)
        {
            int isSave = 0;
            try
            {
                if (appApplicationInfoViewModel != null)
                {
                    //add
                    if (appApplicationInfoViewModel.AppInformationId == default(int))
                    {
                        Create(appApplicationInfoViewModel);
                    }
                    else //edit
                    {
                        Update(appApplicationInfoViewModel);
                    }
                }
                else
                {
                    throw new ArgumentNullException("AppInformationViewModel", "Request data is null.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(AppApplicationInfoViewModel appApplicationInfoViewModel)
        {
            int isSave = 0;
            try
            {
                if (appApplicationInfoViewModel != null)
                {
                    AppApplicationInfo appInformation = appApplicationInfoViewModel.ConvertViewModelToModel<AppApplicationInfoViewModel, AppApplicationInfo>();
                    _appApplicationInfoRepository.Insert(appInformation);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppInformationViewModel", "Request data is null.");
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

        public int Update(AppApplicationInfoViewModel appApplicationInfoViewModel)
        {
            int isSave = 0;
            try
            {
                if (appApplicationInfoViewModel != null)
                {
                    AppApplicationInfo appInformation = appApplicationInfoViewModel.ConvertViewModelToModel<AppApplicationInfoViewModel, AppApplicationInfo>();
                    _appApplicationInfoRepository.Update(appInformation);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppInformationViewModel", "Request data is null.");
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

        public int Delete(AppApplicationInfoViewModel appApplicationInfoViewModel)
        {
            int isSave = 0;
            try
            {
                if (appApplicationInfoViewModel != null)
                {
                    var viewModel = GetById(appApplicationInfoViewModel.AppInformationId);
                    AppApplicationInfo appInformation = viewModel.ConvertViewModelToModel<AppApplicationInfoViewModel, AppApplicationInfo>();
                    _appApplicationInfoRepository.Delete(appInformation);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppInformationViewModel", "Request data is null.");
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
                var appApplicationInfoViewModel = GetById(id);
                if (appApplicationInfoViewModel != null)
                {
                    AppApplicationInfo appInformation = appApplicationInfoViewModel.ConvertViewModelToModel<AppApplicationInfoViewModel, AppApplicationInfo>();
                    _appApplicationInfoRepository.Delete(appInformation);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppInformationViewModel", "Request data is null.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<AppApplicationInfoViewModel> appInformationViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appApplicationInfoViewModel in appInformationViewModels)
                {
                    AppApplicationInfoViewModel viewModel = GetById(appApplicationInfoViewModel.AppInformationId);
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

    #region Interface : AppApplicationInfo

    public interface IAppApplicationInfoRepository : IGeneric<AppApplicationInfoViewModel>
    {
        IQueryable<AppApplicationInfoViewModel> GetAllFromCache();
        AppApplicationInfoViewModel GetByIdFromCache(long id);
        int Delete(List<AppApplicationInfoViewModel> appInformationViewModels);
    }

    #endregion
}
