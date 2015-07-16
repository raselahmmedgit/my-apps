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
    #region Interface Implement : AppUserMetadata

    public class AppUserMetadataRepository : IAppUserMetadataRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<AppUserMetadata> _appUserMetadataRepository;
        private readonly RepositoryBase<AppUserMetadata> _appUserMetadataRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public AppUserMetadataRepository(Repository<AppUserMetadata> appUserMetadataRepository, IUnitOfWork iUnitOfWork)
        {
            this._appUserMetadataRepository = appUserMetadataRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<AppUserMetadataViewModel> GetAll()
        {
            var appUserMetadataViewModels = new List<AppUserMetadataViewModel>();
            try
            {

                List<AppUserMetadata> appUserMetadatas = _appUserMetadataRepository.GetAll();

                foreach (AppUserMetadata appUserMetadata in appUserMetadatas)
                {
                    var appUserMetadataViewModel = appUserMetadata.ConvertModelToViewModel<AppUserMetadata, AppUserMetadataViewModel>();
                    appUserMetadataViewModels.Add(appUserMetadataViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appUserMetadataViewModels.AsQueryable();
        }

        public AppUserMetadataViewModel GetById(long id)
        {
            var appUserMetadataViewModel = new AppUserMetadataViewModel();

            try
            {
                AppUserMetadata appUserMetadata = _appUserMetadataRepository.GetById(id);
                appUserMetadataViewModel = appUserMetadata.ConvertModelToViewModel<AppUserMetadata, AppUserMetadataViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appUserMetadataViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(AppUserMetadataViewModel appUserMetadataViewModel)
        {
            int isSave = 0;
            try
            {
                if (appUserMetadataViewModel != null)
                {
                    //add
                    if (appUserMetadataViewModel.AppUserMetadataId == 0 && appUserMetadataViewModel.ActionName == "Add")
                    {
                        Create(appUserMetadataViewModel);
                    }
                    else if (appUserMetadataViewModel.ActionName == "Edit") //edit
                    {
                        Update(appUserMetadataViewModel);
                    }
                }
                else
                {
                    throw new ArgumentNullException("AppUserMetadataViewModel", "Request data is null.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(AppUserMetadataViewModel appUserMetadataViewModel)
        {
            int isSave = 0;
            try
            {
                if (appUserMetadataViewModel != null)
                {
                    AppUserMetadata appUserMetadata = appUserMetadataViewModel.ConvertViewModelToModel<AppUserMetadataViewModel, AppUserMetadata>();
                    _appUserMetadataRepository.Insert(appUserMetadata);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppUserMetadataViewModel", "Request data is null.");
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

        public int Update(AppUserMetadataViewModel appUserMetadataViewModel)
        {
            int isSave = 0;
            try
            {
                if (appUserMetadataViewModel != null)
                {
                    AppUserMetadata appUserMetadata = appUserMetadataViewModel.ConvertViewModelToModel<AppUserMetadataViewModel, AppUserMetadata>();
                    _appUserMetadataRepository.Update(appUserMetadata);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppUserMetadataViewModel", "Request data is null.");
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

        public int Delete(AppUserMetadataViewModel appUserMetadataViewModel)
        {
            int isSave = 0;
            try
            {
                if (appUserMetadataViewModel != null)
                {
                    var viewModel = GetById(appUserMetadataViewModel.AppUserMetadataId);
                    AppUserMetadata appUserMetadata = viewModel.ConvertViewModelToModel<AppUserMetadataViewModel, AppUserMetadata>();
                    _appUserMetadataRepository.Delete(appUserMetadata);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppUserMetadataViewModel", "Request data is null.");
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
                    AppUserMetadata appUserMetadata = appUserMetadataViewModel.ConvertViewModelToModel<AppUserMetadataViewModel, AppUserMetadata>();
                    _appUserMetadataRepository.Delete(appUserMetadata);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppUserMetadataViewModel", "Request data is null.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<AppUserMetadataViewModel> appUserMetadataViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appUserMetadataViewModel in appUserMetadataViewModels)
                {
                    AppUserMetadataViewModel viewModel = GetById(appUserMetadataViewModel.AppUserMetadataId);
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

    #region Interface : AppUserMetadata

    public interface IAppUserMetadataRepository : IGeneric<AppUserMetadataViewModel>
    {
        int Delete(List<AppUserMetadataViewModel> appUserMetadataViewModels);
    }

    #endregion
}
