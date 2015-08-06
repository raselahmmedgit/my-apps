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
    #region Interface Implement : AppApplicationInfo

    public class AppApplicationInfoRepository : IAppInformationRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<AppApplicationInfo> _appInformationRepository;
        private readonly RepositoryBase<AppApplicationInfo> _appInformationRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public AppApplicationInfoRepository(Repository<AppApplicationInfo> appInformationRepository, IUnitOfWork iUnitOfWork)
        {
            this._appInformationRepository = appInformationRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<AppApplicationInfoViewModel> GetAll()
        {
            var appInformationViewModels = new List<AppApplicationInfoViewModel>();
            try
            {

                List<AppApplicationInfo> appInformations = _appInformationRepository.GetAll();

                foreach (AppApplicationInfo appInformation in appInformations)
                {
                    var appInformationViewModel = appInformation.ConvertModelToViewModel<AppApplicationInfo, AppApplicationInfoViewModel>();
                    appInformationViewModels.Add(appInformationViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appInformationViewModels.AsQueryable();
        }

        public AppApplicationInfoViewModel GetById(long id)
        {
            var appInformationViewModel = new AppApplicationInfoViewModel();

            try
            {
                AppApplicationInfo appInformation = _appInformationRepository.GetById(id);
                appInformationViewModel = appInformation.ConvertModelToViewModel<AppApplicationInfo, AppApplicationInfoViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appInformationViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(AppApplicationInfoViewModel appInformationViewModel)
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
        public int Create(AppApplicationInfoViewModel appInformationViewModel)
        {
            int isSave = 0;
            try
            {
                if (appInformationViewModel != null)
                {
                    AppApplicationInfo appInformation = appInformationViewModel.ConvertViewModelToModel<AppApplicationInfoViewModel, AppApplicationInfo>();
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

        public int Update(AppApplicationInfoViewModel appInformationViewModel)
        {
            int isSave = 0;
            try
            {
                if (appInformationViewModel != null)
                {
                    AppApplicationInfo appInformation = appInformationViewModel.ConvertViewModelToModel<AppApplicationInfoViewModel, AppApplicationInfo>();
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

        public int Delete(AppApplicationInfoViewModel appInformationViewModel)
        {
            int isSave = 0;
            try
            {
                if (appInformationViewModel != null)
                {
                    var viewModel = GetById(appInformationViewModel.AppInformationId);
                    AppApplicationInfo appInformation = viewModel.ConvertViewModelToModel<AppApplicationInfoViewModel, AppApplicationInfo>();
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
                    AppApplicationInfo appInformation = appInformationViewModel.ConvertViewModelToModel<AppApplicationInfoViewModel, AppApplicationInfo>();
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

        public int Delete(List<AppApplicationInfoViewModel> appInformationViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appInformationViewModel in appInformationViewModels)
                {
                    AppApplicationInfoViewModel viewModel = GetById(appInformationViewModel.AppInformationId);
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

    #region Interface : AppApplicationInfo

    public interface IAppInformationRepository : IGeneric<AppApplicationInfoViewModel>
    {
        int Delete(List<AppApplicationInfoViewModel> appInformationViewModels);
    }

    #endregion
}
