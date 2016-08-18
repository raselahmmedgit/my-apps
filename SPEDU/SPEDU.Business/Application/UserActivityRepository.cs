using System;
using System.Collections.Generic;
using System.Linq;
using SPEDU.Common.Helper;
using SPEDU.Domain.Extensions;
using SPEDU.Domain.Models.Application;
using SPEDU.Data.Infrastructure;
using SPEDU.Data.Repositories;
using SPEDU.DomainViewModel.Application;

namespace SPEDU.Business.Application
{
    #region Interface Implement : UserActivity

    public class UserActivityRepository : IUserActivityRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<UserActivity> _appUserActivityRepository;
        private readonly RepositoryBase<UserActivity> _appUserActivityRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public UserActivityRepository(Repository<UserActivity> appUserActivityRepository, IUnitOfWork iUnitOfWork)
        {
            this._appUserActivityRepository = appUserActivityRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<UserActivityViewModel> GetAll()
        {
            var appUserActivityViewModels = new List<UserActivityViewModel>();
            try
            {

                List<UserActivity> appUserActivitys = _appUserActivityRepository.GetAll();

                foreach (UserActivity appUserActivity in appUserActivitys)
                {
                    var appUserActivityViewModel = appUserActivity.ConvertModelToViewModel<UserActivity, UserActivityViewModel>();
                    appUserActivityViewModels.Add(appUserActivityViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appUserActivityViewModels.AsQueryable();
        }

        public UserActivityViewModel GetById(long id)
        {
            var appUserActivityViewModel = new UserActivityViewModel();

            try
            {
                UserActivity appUserActivity = _appUserActivityRepository.GetById(id);
                appUserActivityViewModel = appUserActivity.ConvertModelToViewModel<UserActivity, UserActivityViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appUserActivityViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(UserActivityViewModel appUserActivityViewModel)
        {
            int isSave = 0;
            try
            {
                if (appUserActivityViewModel != null)
                {
                    //add
                    if (appUserActivityViewModel.UserActivityId == default(int))
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
                    throw new ArgumentNullException("UserActivityViewModel", MessageResourceHelper.NullError);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(UserActivityViewModel appUserActivityViewModel)
        {
            int isSave = 0;
            try
            {
                if (appUserActivityViewModel != null)
                {
                    UserActivity appUserActivity = appUserActivityViewModel.ConvertViewModelToModel<UserActivityViewModel, UserActivity>();
                    _appUserActivityRepository.Insert(appUserActivity);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("UserActivityViewModel", MessageResourceHelper.NullError);
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

        public int Update(UserActivityViewModel appUserActivityViewModel)
        {
            int isSave = 0;
            try
            {
                if (appUserActivityViewModel != null)
                {
                    UserActivity appUserActivity = appUserActivityViewModel.ConvertViewModelToModel<UserActivityViewModel, UserActivity>();
                    _appUserActivityRepository.Update(appUserActivity);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("UserActivityViewModel", MessageResourceHelper.NullError);
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

        public int Delete(UserActivityViewModel appUserActivityViewModel)
        {
            int isSave = 0;
            try
            {
                if (appUserActivityViewModel != null)
                {
                    var viewModel = GetById(appUserActivityViewModel.UserActivityId);
                    UserActivity appUserActivity = viewModel.ConvertViewModelToModel<UserActivityViewModel, UserActivity>();
                    _appUserActivityRepository.Delete(appUserActivity);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("UserActivityViewModel", MessageResourceHelper.NullError);
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
                    UserActivity appUserActivity = appUserActivityViewModel.ConvertViewModelToModel<UserActivityViewModel, UserActivity>();
                    _appUserActivityRepository.Delete(appUserActivity);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("UserActivityViewModel", MessageResourceHelper.NullError);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<UserActivityViewModel> appUserActivityViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appUserActivityViewModel in appUserActivityViewModels)
                {
                    UserActivityViewModel viewModel = GetById(appUserActivityViewModel.UserActivityId);
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

    #region Interface : UserActivity

    public interface IUserActivityRepository : IGeneric<UserActivityViewModel>
    {
        int Delete(List<UserActivityViewModel> appUserActivityViewModels);
    }

    #endregion
}
