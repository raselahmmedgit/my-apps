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
    #region Interface Implement : User

    public class UserRepository : IUserRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<User> _userRepository;
        private readonly RepositoryBase<User> _userRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public UserRepository(Repository<User> userRepository, IUnitOfWork iUnitOfWork)
        {
            this._userRepository = userRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<UserViewModel> GetAll()
        {
            var userViewModels = new List<UserViewModel>();
            try
            {

                List<User> users = _userRepository.GetAll();

                foreach (User user in users)
                {
                    var userViewModel = user.ConvertModelToViewModel<User, UserViewModel>();
                    userViewModels.Add(userViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userViewModels.AsQueryable();
        }

        public UserViewModel GetById(long id)
        {
            var userViewModel = new UserViewModel();

            try
            {
                User user = _userRepository.GetById(id);
                userViewModel = user.ConvertModelToViewModel<User, UserViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return userViewModel;
        }

        public UserViewModel GetByEmail(string email)
        {
            var userViewModel = new UserViewModel();

            try
            {
                User user = _userRepository.GetAll().SingleOrDefault(item => item.Email == email);
                userViewModel = user.ConvertModelToViewModel<User, UserViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return userViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(UserViewModel userViewModel)
        {
            int isSave = 0;
            try
            {
                if (userViewModel != null)
                {
                    //add
                    if (userViewModel.UserId == default(int))
                    {
                        Create(userViewModel);
                    }
                    else //edit
                    {
                        Update(userViewModel);
                    }
                }
                else
                {
                    throw new ArgumentNullException("UserViewModel", MessageResourceHelper.NullError);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(UserViewModel userViewModel)
        {
            int isSave = 0;
            try
            {
                if (userViewModel != null)
                {
                    User user = userViewModel.ConvertViewModelToModel<UserViewModel, User>();
                    _userRepository.Insert(user);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("UserViewModel", MessageResourceHelper.NullError);
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

        public int Update(UserViewModel userViewModel)
        {
            int isSave = 0;
            try
            {
                if (userViewModel != null)
                {
                    User user = userViewModel.ConvertViewModelToModel<UserViewModel, User>();
                    _userRepository.Update(user);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("UserViewModel", MessageResourceHelper.NullError);
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

        public int Delete(UserViewModel userViewModel)
        {
            int isSave = 0;
            try
            {
                if (userViewModel != null)
                {
                    var viewModel = GetById(userViewModel.UserId);
                    User user = viewModel.ConvertViewModelToModel<UserViewModel, User>();
                    _userRepository.Delete(user);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("UserViewModel", MessageResourceHelper.NullError);
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
                var userViewModel = GetById(id);
                if (userViewModel != null)
                {
                    User user = userViewModel.ConvertViewModelToModel<UserViewModel, User>();
                    _userRepository.Delete(user);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("UserViewModel", MessageResourceHelper.NullError);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<UserViewModel> userViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var userViewModel in userViewModels)
                {
                    UserViewModel viewModel = GetById(userViewModel.UserId);
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

    #region Interface : User

    public interface IUserRepository : IGeneric<UserViewModel>
    {
        UserViewModel GetByEmail(string email);

        int Delete(List<UserViewModel> userViewModels);
    }

    #endregion
}
