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
    #region Interface Implement : ApplicationInfo

    public class ApplicationInfoRepository : IApplicationInfoRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<ApplicationInfo> _ApplicationInfoRepository;
        private readonly RepositoryBase<ApplicationInfo> _ApplicationInfoRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public ApplicationInfoRepository(Repository<ApplicationInfo> appInformationRepository, IUnitOfWork iUnitOfWork)
        {
            this._ApplicationInfoRepository = appInformationRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method By Cache

        public IQueryable<ApplicationInfoViewModel> GetAllFromCache()
        {
            var ApplicationInfoViewModels = new List<ApplicationInfoViewModel>();
            try
            {

                List<ApplicationInfo> ApplicationInfoList = new List<ApplicationInfo>();

                string cacheKey = AppConstant.CacheKey.AllApplicationInfo;
                if (!CacheManager.ICache.IsSet(cacheKey))
                {
                    ApplicationInfoList = _ApplicationInfoRepository.GetAll();
                    CacheManager.ICache.Set(cacheKey, ApplicationInfoList);
                }
                else
                {
                    ApplicationInfoList = CacheManager.ICache.Get(cacheKey) as List<ApplicationInfo>;
                }

                if (ApplicationInfoList != null)
                {
                    foreach (ApplicationInfo ApplicationInfo in ApplicationInfoList)
                    {
                        var appMenuViewModel = ApplicationInfo.ConvertModelToViewModel<ApplicationInfo, ApplicationInfoViewModel>();
                        ApplicationInfoViewModels.Add(appMenuViewModel);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ApplicationInfoViewModels.AsQueryable();
        }

        public ApplicationInfoViewModel GetByIdFromCache(long id)
        {
            var ApplicationInfoViewModel = new ApplicationInfoViewModel();

            try
            {
                ApplicationInfo appInformation = GetAllFromCache().FirstOrDefault(item => item.ApplicationInfoId == id);
                ApplicationInfoViewModel = appInformation.ConvertModelToViewModel<ApplicationInfo, ApplicationInfoViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ApplicationInfoViewModel;
        }

        #endregion

        #region Get Method

        public IQueryable<ApplicationInfoViewModel> GetAll()
        {
            var appInformationViewModels = new List<ApplicationInfoViewModel>();
            try
            {

                List<ApplicationInfo> ApplicationInfos = _ApplicationInfoRepository.GetAll();

                foreach (ApplicationInfo appInformation in ApplicationInfos)
                {
                    var ApplicationInfoViewModel = appInformation.ConvertModelToViewModel<ApplicationInfo, ApplicationInfoViewModel>();
                    appInformationViewModels.Add(ApplicationInfoViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appInformationViewModels.AsQueryable();
        }

        public ApplicationInfoViewModel GetById(long id)
        {
            var ApplicationInfoViewModel = new ApplicationInfoViewModel();

            try
            {
                ApplicationInfo appInformation = _ApplicationInfoRepository.GetById(id);
                ApplicationInfoViewModel = appInformation.ConvertModelToViewModel<ApplicationInfo, ApplicationInfoViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ApplicationInfoViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(ApplicationInfoViewModel ApplicationInfoViewModel)
        {
            int isSave = 0;
            try
            {
                if (ApplicationInfoViewModel != null)
                {
                    //add
                    if (ApplicationInfoViewModel.ApplicationInfoId == default(int))
                    {
                        Create(ApplicationInfoViewModel);
                    }
                    else //edit
                    {
                        Update(ApplicationInfoViewModel);
                    }
                }
                else
                {
                    throw new ArgumentNullException("AppInformationViewModel", MessageResourceHelper.NullError);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(ApplicationInfoViewModel ApplicationInfoViewModel)
        {
            int isSave = 0;
            try
            {
                if (ApplicationInfoViewModel != null)
                {
                    ApplicationInfo appInformation = ApplicationInfoViewModel.ConvertViewModelToModel<ApplicationInfoViewModel, ApplicationInfo>();
                    _ApplicationInfoRepository.Insert(appInformation);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppInformationViewModel", MessageResourceHelper.NullError);
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

        public int Update(ApplicationInfoViewModel ApplicationInfoViewModel)
        {
            int isSave = 0;
            try
            {
                if (ApplicationInfoViewModel != null)
                {
                    ApplicationInfo appInformation = ApplicationInfoViewModel.ConvertViewModelToModel<ApplicationInfoViewModel, ApplicationInfo>();
                    _ApplicationInfoRepository.Update(appInformation);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppInformationViewModel", MessageResourceHelper.NullError);
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

        public int Delete(ApplicationInfoViewModel ApplicationInfoViewModel)
        {
            int isSave = 0;
            try
            {
                if (ApplicationInfoViewModel != null)
                {
                    var viewModel = GetById(ApplicationInfoViewModel.ApplicationInfoId);
                    ApplicationInfo appInformation = viewModel.ConvertViewModelToModel<ApplicationInfoViewModel, ApplicationInfo>();
                    _ApplicationInfoRepository.Delete(appInformation);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppInformationViewModel", MessageResourceHelper.NullError);
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
                var ApplicationInfoViewModel = GetById(id);
                if (ApplicationInfoViewModel != null)
                {
                    ApplicationInfo appInformation = ApplicationInfoViewModel.ConvertViewModelToModel<ApplicationInfoViewModel, ApplicationInfo>();
                    _ApplicationInfoRepository.Delete(appInformation);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppInformationViewModel", MessageResourceHelper.NullError);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<ApplicationInfoViewModel> appInformationViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var ApplicationInfoViewModel in appInformationViewModels)
                {
                    ApplicationInfoViewModel viewModel = GetById(ApplicationInfoViewModel.ApplicationInfoId);
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

    #region Interface : ApplicationInfo

    public interface IApplicationInfoRepository : IGeneric<ApplicationInfoViewModel>
    {
        IQueryable<ApplicationInfoViewModel> GetAllFromCache();
        ApplicationInfoViewModel GetByIdFromCache(long id);
        int Delete(List<ApplicationInfoViewModel> appInformationViewModels);
    }

    #endregion
}
