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
    #region Interface Implement : UserRole

    public class UserRoleRepository : IUserRoleRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<UserRole> _userRoleRepository;
        private readonly RepositoryBase<UserRole> _userRoleRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public UserRoleRepository(Repository<UserRole> userRoleRepository, IUnitOfWork iUnitOfWork)
        {
            this._userRoleRepository = userRoleRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<UserRoleViewModel> GetAll()
        {
            var userRoleViewModels = new List<UserRoleViewModel>();
            try
            {

                List<UserRole> userRoles = _userRoleRepository.GetAll();

                foreach (UserRole userRole in userRoles)
                {
                    var userRoleViewModel = userRole.ConvertModelToViewModel<UserRole, UserRoleViewModel>();
                    userRoleViewModels.Add(userRoleViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userRoleViewModels.AsQueryable();
        }

        public UserRoleViewModel GetById(long id)
        {
            var userRoleViewModel = new UserRoleViewModel();

            try
            {
                UserRole userRole = _userRoleRepository.GetById(id);
                userRoleViewModel = userRole.ConvertModelToViewModel<UserRole, UserRoleViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return userRoleViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(UserRoleViewModel userRoleViewModel)
        {
            int isSave = 0;
            try
            {
                if (userRoleViewModel != null)
                {
                    //add
                    if (userRoleViewModel.UserRoleId == default(int))
                    {
                        Create(userRoleViewModel);
                    }
                    else //edit
                    {
                        Update(userRoleViewModel);
                    }
                }
                else
                {
                    throw new ArgumentNullException("UserRoleViewModel", MessageResourceHelper.NullError);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(UserRoleViewModel userRoleViewModel)
        {
            int isSave = 0;
            try
            {
                if (userRoleViewModel != null)
                {
                    UserRole userRole = userRoleViewModel.ConvertViewModelToModel<UserRoleViewModel, UserRole>();
                    _userRoleRepository.Insert(userRole);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("UserRoleViewModel", MessageResourceHelper.NullError);
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

        public int Update(UserRoleViewModel userRoleViewModel)
        {
            int isSave = 0;
            try
            {
                if (userRoleViewModel != null)
                {
                    UserRole userRole = userRoleViewModel.ConvertViewModelToModel<UserRoleViewModel, UserRole>();
                    _userRoleRepository.Update(userRole);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("UserRoleViewModel", MessageResourceHelper.NullError);
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

        public int Delete(UserRoleViewModel userRoleViewModel)
        {
            int isSave = 0;
            try
            {
                if (userRoleViewModel != null)
                {
                    var viewModel = GetById(userRoleViewModel.UserRoleId);
                    UserRole userRole = viewModel.ConvertViewModelToModel<UserRoleViewModel, UserRole>();
                    _userRoleRepository.Delete(userRole);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("UserRoleViewModel", MessageResourceHelper.NullError);
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
                var userRoleViewModel = GetById(id);
                if (userRoleViewModel != null)
                {
                    UserRole userRole = userRoleViewModel.ConvertViewModelToModel<UserRoleViewModel, UserRole>();
                    _userRoleRepository.Delete(userRole);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("UserRoleViewModel", MessageResourceHelper.NullError);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<UserRoleViewModel> userRoleViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var userRoleViewModel in userRoleViewModels)
                {
                    UserRoleViewModel viewModel = GetById(userRoleViewModel.UserRoleId);
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

    #region Interface : UserRole

    public interface IUserRoleRepository : IGeneric<UserRoleViewModel>
    {
        int Delete(List<UserRoleViewModel> userRoleViewModels);
    }

    #endregion
}
