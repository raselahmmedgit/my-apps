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
    #region Interface Implement : AppInformation

    public class AppInformationRepository : IAppInformationRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<AppInformation> _appInformationRepository;
        private readonly RepositoryBase<AppInformation> _appInformationRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public AppInformationRepository(Repository<AppInformation> appInformationRepository, IUnitOfWork iUnitOfWork)
        {
            this._appInformationRepository = appInformationRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<AppInformationViewModel> GetAll()
        {
            var appInformationViewModels = new List<AppInformationViewModel>();
            try
            {

                List<AppInformation> appInformations = _appInformationRepository.GetAll();

                foreach (AppInformation appInformation in appInformations)
                {
                    var appInformationViewModel = appInformation.ConvertModelToViewModel<AppInformation, AppInformationViewModel>();
                    appInformationViewModels.Add(appInformationViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appInformationViewModels.AsQueryable();
        }

        public AppInformationViewModel GetById(long id)
        {
            var appInformationViewModel = new AppInformationViewModel();

            try
            {
                AppInformation appInformation = _appInformationRepository.GetById(id);
                appInformationViewModel = appInformation.ConvertModelToViewModel<AppInformation, AppInformationViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appInformationViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(AppInformationViewModel appInformationViewModel)
        {
            int isSave = 0;
            try
            {
                if (appInformationViewModel != null)
                {
                    //add
                    if (appInformationViewModel.AppInformationId == 0 && appInformationViewModel.ActionName == "Add")
                    {
                        Create(appInformationViewModel);
                    }
                    else if (appInformationViewModel.ActionName == "Edit") //edit
                    {
                        Update(appInformationViewModel);
                    }
                }
                else
                {
                    throw new ArgumentNullException("AppInformationViewModel", "Request data is null.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(AppInformationViewModel appInformationViewModel)
        {
            int isSave = 0;
            try
            {
                if (appInformationViewModel != null)
                {
                    AppInformation appInformation = appInformationViewModel.ConvertViewModelToModel<AppInformationViewModel, AppInformation>();
                    _appInformationRepository.Insert(appInformation);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppInformationViewModel", "Request data is null.");
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

        public int Update(AppInformationViewModel appInformationViewModel)
        {
            int isSave = 0;
            try
            {
                if (appInformationViewModel != null)
                {
                    AppInformation appInformation = appInformationViewModel.ConvertViewModelToModel<AppInformationViewModel, AppInformation>();
                    _appInformationRepository.Update(appInformation);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppInformationViewModel", "Request data is null.");
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

        public int Delete(AppInformationViewModel appInformationViewModel)
        {
            int isSave = 0;
            try
            {
                if (appInformationViewModel != null)
                {
                    var viewModel = GetById(appInformationViewModel.AppInformationId);
                    AppInformation appInformation = viewModel.ConvertViewModelToModel<AppInformationViewModel, AppInformation>();
                    _appInformationRepository.Delete(appInformation);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppInformationViewModel", "Request data is null.");
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
                var appInformationViewModel = GetById(id);
                if (appInformationViewModel != null)
                {
                    AppInformation appInformation = appInformationViewModel.ConvertViewModelToModel<AppInformationViewModel, AppInformation>();
                    _appInformationRepository.Delete(appInformation);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppInformationViewModel", "Request data is null.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<AppInformationViewModel> appInformationViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appInformationViewModel in appInformationViewModels)
                {
                    AppInformationViewModel viewModel = GetById(appInformationViewModel.AppInformationId);
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

    #region Interface : AppInformation

    public interface IAppInformationRepository : IGeneric<AppInformationViewModel>
    {
        int Delete(List<AppInformationViewModel> appInformationViewModels);
    }

    #endregion
}
