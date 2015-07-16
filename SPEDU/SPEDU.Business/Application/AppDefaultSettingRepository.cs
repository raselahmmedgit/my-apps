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
    #region Interface Implement : AppDefaultSetting

    public class AppDefaultSettingRepository : IAppDefaultSettingRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<AppDefaultSetting> _appDefaultSettingRepository;
        private readonly RepositoryBase<AppDefaultSetting> _appDefaultSettingRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public AppDefaultSettingRepository(Repository<AppDefaultSetting> appDefaultSettingRepository, IUnitOfWork iUnitOfWork)
        {
            this._appDefaultSettingRepository = appDefaultSettingRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<AppDefaultSettingViewModel> GetAll()
        {
            var appDefaultSettingViewModels = new List<AppDefaultSettingViewModel>();
            try
            {

                List<AppDefaultSetting> appDefaultSettings = _appDefaultSettingRepository.GetAll();

                foreach (AppDefaultSetting appDefaultSetting in appDefaultSettings)
                {
                    var appDefaultSettingViewModel = appDefaultSetting.ConvertModelToViewModel<AppDefaultSetting, AppDefaultSettingViewModel>();
                    appDefaultSettingViewModels.Add(appDefaultSettingViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appDefaultSettingViewModels.AsQueryable();
        }

        public AppDefaultSettingViewModel GetById(long id)
        {
            var appDefaultSettingViewModel = new AppDefaultSettingViewModel();

            try
            {
                AppDefaultSetting appDefaultSetting = _appDefaultSettingRepository.GetById(id);
                appDefaultSettingViewModel = appDefaultSetting.ConvertModelToViewModel<AppDefaultSetting, AppDefaultSettingViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appDefaultSettingViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(AppDefaultSettingViewModel appDefaultSettingViewModel)
        {
            int isSave = 0;
            try
            {
                if (appDefaultSettingViewModel != null)
                {
                    //add
                    if (appDefaultSettingViewModel.AppDefaultSettingId == 0 && appDefaultSettingViewModel.ActionName == "Add")
                    {
                        Create(appDefaultSettingViewModel);
                    }
                    else if (appDefaultSettingViewModel.ActionName == "Edit") //edit
                    {
                        Update(appDefaultSettingViewModel);
                    }
                }
                else
                {
                    throw new ArgumentNullException("AppDefaultSettingViewModel", "Request data is null.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(AppDefaultSettingViewModel appDefaultSettingViewModel)
        {
            int isSave = 0;
            try
            {
                if (appDefaultSettingViewModel != null)
                {
                    AppDefaultSetting appDefaultSetting = appDefaultSettingViewModel.ConvertViewModelToModel<AppDefaultSettingViewModel, AppDefaultSetting>();
                    _appDefaultSettingRepository.Insert(appDefaultSetting);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppDefaultSettingViewModel", "Request data is null.");
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

        public int Update(AppDefaultSettingViewModel appDefaultSettingViewModel)
        {
            int isSave = 0;
            try
            {
                var updateAppDefaultSettingViewModel = GetById(appDefaultSettingViewModel.AppDefaultSettingId);

                if (updateAppDefaultSettingViewModel != null)
                {
                    AppDefaultSetting appDefaultSetting = appDefaultSettingViewModel.ConvertViewModelToModel<AppDefaultSettingViewModel, AppDefaultSetting>();
                    _appDefaultSettingRepository.Update(appDefaultSetting);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppDefaultSettingViewModel", "Request data is null.");
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

        public int Delete(AppDefaultSettingViewModel appDefaultSettingViewModel)
        {
            int isSave = 0;
            try
            {
                var deleteAppDefaultSettingViewModel = GetById(appDefaultSettingViewModel.AppDefaultSettingId);

                if (deleteAppDefaultSettingViewModel != null)
                {
                    var viewModel = GetById(appDefaultSettingViewModel.AppDefaultSettingId);
                    AppDefaultSetting appDefaultSetting = viewModel.ConvertViewModelToModel<AppDefaultSettingViewModel, AppDefaultSetting>();
                    _appDefaultSettingRepository.Delete(appDefaultSetting);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppDefaultSettingViewModel", "Request data is null.");
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
                var appDefaultSettingViewModel = GetById(id);
                if (appDefaultSettingViewModel != null)
                {
                    AppDefaultSetting appDefaultSetting = appDefaultSettingViewModel.ConvertViewModelToModel<AppDefaultSettingViewModel, AppDefaultSetting>();
                    _appDefaultSettingRepository.Delete(appDefaultSetting);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppDefaultSettingViewModel", "Request data is null.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<AppDefaultSettingViewModel> appDefaultSettingViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appDefaultSettingViewModel in appDefaultSettingViewModels)
                {
                    AppDefaultSettingViewModel viewModel = GetById(appDefaultSettingViewModel.AppDefaultSettingId);
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

    #region Interface : AppDefaultSetting

    public interface IAppDefaultSettingRepository : IGeneric<AppDefaultSettingViewModel>
    {
        int Delete(List<AppDefaultSettingViewModel> appDefaultSettingViewModels);
    }

    #endregion
}
