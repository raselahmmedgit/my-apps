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
    #region Interface Implement : UserProfile

    public class UserProfileRepository : IUserProfileRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<UserProfile> _userProfileRepository;
        private readonly RepositoryBase<UserProfile> _userProfileRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public UserProfileRepository(Repository<UserProfile> userProfileRepository, IUnitOfWork iUnitOfWork)
        {
            this._userProfileRepository = userProfileRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<UserProfileViewModel> GetAll()
        {
            var userProfileViewModels = new List<UserProfileViewModel>();
            try
            {

                List<UserProfile> appUserProfiles = _userProfileRepository.GetAll();

                foreach (UserProfile appUserProfile in appUserProfiles)
                {
                    var userProfileViewModel = appUserProfile.ConvertModelToViewModel<UserProfile, UserProfileViewModel>();
                    userProfileViewModels.Add(userProfileViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userProfileViewModels.AsQueryable();
        }

        public UserProfileViewModel GetById(long id)
        {
            var userProfileViewModel = new UserProfileViewModel();

            try
            {
                UserProfile appUserProfile = _userProfileRepository.GetById(id);
                userProfileViewModel = appUserProfile.ConvertModelToViewModel<UserProfile, UserProfileViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return userProfileViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(UserProfileViewModel userProfileViewModel)
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
        public int Create(UserProfileViewModel userProfileViewModel)
        {
            int isSave = 0;
            try
            {
                if (userProfileViewModel != null)
                {
                    UserProfile appUserProfile = userProfileViewModel.ConvertViewModelToModel<UserProfileViewModel, UserProfile>();
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

        public int Update(UserProfileViewModel userProfileViewModel)
        {
            int isSave = 0;
            try
            {
                if (userProfileViewModel != null)
                {
                    UserProfile appUserProfile = userProfileViewModel.ConvertViewModelToModel<UserProfileViewModel, UserProfile>();
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

        public int Delete(UserProfileViewModel userProfileViewModel)
        {
            int isSave = 0;
            try
            {
                if (userProfileViewModel != null)
                {
                    var viewModel = GetById(userProfileViewModel.UserProfileId);
                    UserProfile appUserProfile = viewModel.ConvertViewModelToModel<UserProfileViewModel, UserProfile>();
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
                    UserProfile appUserProfile = userProfileViewModel.ConvertViewModelToModel<UserProfileViewModel, UserProfile>();
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

        public int Delete(List<UserProfileViewModel> userProfileViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var userProfileViewModel in userProfileViewModels)
                {
                    UserProfileViewModel viewModel = GetById(userProfileViewModel.UserProfileId);
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

    #region Interface : UserProfile

    public interface IUserProfileRepository : IGeneric<UserProfileViewModel>
    {
        int Delete(List<UserProfileViewModel> userProfileViewModels);
    }

    #endregion
}
