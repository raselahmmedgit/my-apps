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
    #region Interface Implement : UserMetadata

    public class UserMetadataRepository : IUserMetadataRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<UserMetadata> _appUserMetadataRepository;
        private readonly RepositoryBase<UserMetadata> _appUserMetadataRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public UserMetadataRepository(Repository<UserMetadata> appUserMetadataRepository, IUnitOfWork iUnitOfWork)
        {
            this._appUserMetadataRepository = appUserMetadataRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<UserMetadataViewModel> GetAll()
        {
            var appUserMetadataViewModels = new List<UserMetadataViewModel>();
            try
            {

                List<UserMetadata> appUserMetadatas = _appUserMetadataRepository.GetAll();

                foreach (UserMetadata appUserMetadata in appUserMetadatas)
                {
                    var appUserMetadataViewModel = appUserMetadata.ConvertModelToViewModel<UserMetadata, UserMetadataViewModel>();
                    appUserMetadataViewModels.Add(appUserMetadataViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appUserMetadataViewModels.AsQueryable();
        }

        public UserMetadataViewModel GetById(long id)
        {
            var appUserMetadataViewModel = new UserMetadataViewModel();

            try
            {
                UserMetadata appUserMetadata = _appUserMetadataRepository.GetById(id);
                appUserMetadataViewModel = appUserMetadata.ConvertModelToViewModel<UserMetadata, UserMetadataViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appUserMetadataViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(UserMetadataViewModel appUserMetadataViewModel)
        {
            int isSave = 0;
            try
            {
                if (appUserMetadataViewModel != null)
                {
                    //add
                    if (appUserMetadataViewModel.UserMetadataId == default(int))
                    {
                        Create(appUserMetadataViewModel);
                    }
                    else //edit
                    {
                        Update(appUserMetadataViewModel);
                    }
                }
                else
                {
                    throw new ArgumentNullException("UserMetadataViewModel", MessageResourceHelper.NullError);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(UserMetadataViewModel appUserMetadataViewModel)
        {
            int isSave = 0;
            try
            {
                if (appUserMetadataViewModel != null)
                {
                    UserMetadata appUserMetadata = appUserMetadataViewModel.ConvertViewModelToModel<UserMetadataViewModel, UserMetadata>();
                    _appUserMetadataRepository.Insert(appUserMetadata);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("UserMetadataViewModel", MessageResourceHelper.NullError);
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

        public int Update(UserMetadataViewModel appUserMetadataViewModel)
        {
            int isSave = 0;
            try
            {
                if (appUserMetadataViewModel != null)
                {
                    UserMetadata appUserMetadata = appUserMetadataViewModel.ConvertViewModelToModel<UserMetadataViewModel, UserMetadata>();
                    _appUserMetadataRepository.Update(appUserMetadata);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("UserMetadataViewModel", MessageResourceHelper.NullError);
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

        public int Delete(UserMetadataViewModel appUserMetadataViewModel)
        {
            int isSave = 0;
            try
            {
                if (appUserMetadataViewModel != null)
                {
                    var viewModel = GetById(appUserMetadataViewModel.UserMetadataId);
                    UserMetadata appUserMetadata = viewModel.ConvertViewModelToModel<UserMetadataViewModel, UserMetadata>();
                    _appUserMetadataRepository.Delete(appUserMetadata);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("UserMetadataViewModel", MessageResourceHelper.NullError);
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
                var appUserMetadataViewModel = GetById(id);
                if (appUserMetadataViewModel != null)
                {
                    UserMetadata appUserMetadata = appUserMetadataViewModel.ConvertViewModelToModel<UserMetadataViewModel, UserMetadata>();
                    _appUserMetadataRepository.Delete(appUserMetadata);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("UserMetadataViewModel", MessageResourceHelper.NullError);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<UserMetadataViewModel> appUserMetadataViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appUserMetadataViewModel in appUserMetadataViewModels)
                {
                    UserMetadataViewModel viewModel = GetById(appUserMetadataViewModel.UserMetadataId);
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

    #region Interface : UserMetadata

    public interface IUserMetadataRepository : IGeneric<UserMetadataViewModel>
    {
        int Delete(List<UserMetadataViewModel> appUserMetadataViewModels);
    }

    #endregion
}
