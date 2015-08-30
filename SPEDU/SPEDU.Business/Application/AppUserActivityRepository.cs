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
    #region Interface Implement : AppUserActivity

    public class AppUserActivityRepository : IAppUserActivityRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<AppUserActivity> _appUserActivityRepository;
        private readonly RepositoryBase<AppUserActivity> _appUserActivityRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public AppUserActivityRepository(Repository<AppUserActivity> appUserActivityRepository, IUnitOfWork iUnitOfWork)
        {
            this._appUserActivityRepository = appUserActivityRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<AppUserActivityViewModel> GetAll()
        {
            var appUserActivityViewModels = new List<AppUserActivityViewModel>();
            try
            {

                List<AppUserActivity> appUserActivitys = _appUserActivityRepository.GetAll();

                foreach (AppUserActivity appUserActivity in appUserActivitys)
                {
                    var appUserActivityViewModel = appUserActivity.ConvertModelToViewModel<AppUserActivity, AppUserActivityViewModel>();
                    appUserActivityViewModels.Add(appUserActivityViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appUserActivityViewModels.AsQueryable();
        }

        public AppUserActivityViewModel GetById(long id)
        {
            var appUserActivityViewModel = new AppUserActivityViewModel();

            try
            {
                AppUserActivity appUserActivity = _appUserActivityRepository.GetById(id);
                appUserActivityViewModel = appUserActivity.ConvertModelToViewModel<AppUserActivity, AppUserActivityViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appUserActivityViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(AppUserActivityViewModel appUserActivityViewModel)
        {
            int isSave = 0;
            try
            {
                if (appUserActivityViewModel != null)
                {
                    //add
                    if (appUserActivityViewModel.AppUserActivityId == default(int))
                    {
                        Create(appUserActivityViewModel);
                    }
                    else //edit
                    {
                        Update(appUserActivityViewModel);
                    }
                }
                else
                {
                    throw new ArgumentNullException("AppUserActivityViewModel", "Request data is null.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(AppUserActivityViewModel appUserActivityViewModel)
        {
            int isSave = 0;
            try
            {
                if (appUserActivityViewModel != null)
                {
                    AppUserActivity appUserActivity = appUserActivityViewModel.ConvertViewModelToModel<AppUserActivityViewModel, AppUserActivity>();
                    _appUserActivityRepository.Insert(appUserActivity);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppUserActivityViewModel", "Request data is null.");
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

        public int Update(AppUserActivityViewModel appUserActivityViewModel)
        {
            int isSave = 0;
            try
            {
                if (appUserActivityViewModel != null)
                {
                    AppUserActivity appUserActivity = appUserActivityViewModel.ConvertViewModelToModel<AppUserActivityViewModel, AppUserActivity>();
                    _appUserActivityRepository.Update(appUserActivity);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppUserActivityViewModel", "Request data is null.");
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

        public int Delete(AppUserActivityViewModel appUserActivityViewModel)
        {
            int isSave = 0;
            try
            {
                if (appUserActivityViewModel != null)
                {
                    var viewModel = GetById(appUserActivityViewModel.AppUserActivityId);
                    AppUserActivity appUserActivity = viewModel.ConvertViewModelToModel<AppUserActivityViewModel, AppUserActivity>();
                    _appUserActivityRepository.Delete(appUserActivity);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppUserActivityViewModel", "Request data is null.");
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
                var appUserActivityViewModel = GetById(id);
                if (appUserActivityViewModel != null)
                {
                    AppUserActivity appUserActivity = appUserActivityViewModel.ConvertViewModelToModel<AppUserActivityViewModel, AppUserActivity>();
                    _appUserActivityRepository.Delete(appUserActivity);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppUserActivityViewModel", "Request data is null.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<AppUserActivityViewModel> appUserActivityViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appUserActivityViewModel in appUserActivityViewModels)
                {
                    AppUserActivityViewModel viewModel = GetById(appUserActivityViewModel.AppUserActivityId);
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

    #region Interface : AppUserActivity

    public interface IAppUserActivityRepository : IGeneric<AppUserActivityViewModel>
    {
        int Delete(List<AppUserActivityViewModel> appUserActivityViewModels);
    }

    #endregion
}
