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
    #region Interface Implement : AppUserProfile

    public class AppUserProfileRepository : IAppUserProfileRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<AppUserProfile> _userProfileRepository;
        private readonly RepositoryBase<AppUserProfile> _userProfileRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public AppUserProfileRepository(Repository<AppUserProfile> userProfileRepository, IUnitOfWork iUnitOfWork)
        {
            this._userProfileRepository = userProfileRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<AppUserProfileViewModel> GetAll()
        {
            var userProfileViewModels = new List<AppUserProfileViewModel>();
            try
            {

                List<AppUserProfile> appUserProfiles = _userProfileRepository.GetAll();

                foreach (AppUserProfile appUserProfile in appUserProfiles)
                {
                    var userProfileViewModel = appUserProfile.ConvertModelToViewModel<AppUserProfile, AppUserProfileViewModel>();
                    userProfileViewModels.Add(userProfileViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userProfileViewModels.AsQueryable();
        }

        public AppUserProfileViewModel GetById(long id)
        {
            var userProfileViewModel = new AppUserProfileViewModel();

            try
            {
                AppUserProfile appUserProfile = _userProfileRepository.GetById(id);
                userProfileViewModel = appUserProfile.ConvertModelToViewModel<AppUserProfile, AppUserProfileViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return userProfileViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(AppUserProfileViewModel userProfileViewModel)
        {
            int isSave = 0;
            try
            {
                if (userProfileViewModel != null)
                {
                    //add
                    if (userProfileViewModel.UserProfileId == default(int))
                    {
                        Create(userProfileViewModel);
                    }
                    else //edit
                    {
                        Update(userProfileViewModel);
                    }
                }
                else
                {
                    throw new ArgumentNullException("UserProfileViewModel", "Request data is null.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(AppUserProfileViewModel userProfileViewModel)
        {
            int isSave = 0;
            try
            {
                if (userProfileViewModel != null)
                {
                    AppUserProfile appUserProfile = userProfileViewModel.ConvertViewModelToModel<AppUserProfileViewModel, AppUserProfile>();
                    _userProfileRepository.Insert(appUserProfile);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("UserProfileViewModel", "Request data is null.");
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

        public int Update(AppUserProfileViewModel userProfileViewModel)
        {
            int isSave = 0;
            try
            {
                if (userProfileViewModel != null)
                {
                    AppUserProfile appUserProfile = userProfileViewModel.ConvertViewModelToModel<AppUserProfileViewModel, AppUserProfile>();
                    _userProfileRepository.Update(appUserProfile);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("UserProfileViewModel", "Request data is null.");
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

        public int Delete(AppUserProfileViewModel userProfileViewModel)
        {
            int isSave = 0;
            try
            {
                if (userProfileViewModel != null)
                {
                    var viewModel = GetById(userProfileViewModel.UserProfileId);
                    AppUserProfile appUserProfile = viewModel.ConvertViewModelToModel<AppUserProfileViewModel, AppUserProfile>();
                    _userProfileRepository.Delete(appUserProfile);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("UserProfileViewModel", "Request data is null.");
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
                var userProfileViewModel = GetById(id);
                if (userProfileViewModel != null)
                {
                    AppUserProfile appUserProfile = userProfileViewModel.ConvertViewModelToModel<AppUserProfileViewModel, AppUserProfile>();
                    _userProfileRepository.Delete(appUserProfile);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("UserProfileViewModel", "Request data is null.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<AppUserProfileViewModel> userProfileViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var userProfileViewModel in userProfileViewModels)
                {
                    AppUserProfileViewModel viewModel = GetById(userProfileViewModel.UserProfileId);
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

    #region Interface : AppUserProfile

    public interface IAppUserProfileRepository : IGeneric<AppUserProfileViewModel>
    {
        int Delete(List<AppUserProfileViewModel> userProfileViewModels);
    }

    #endregion
}
