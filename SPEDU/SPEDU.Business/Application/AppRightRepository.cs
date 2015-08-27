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
    #region Interface Implement :  AppRight

    public class AppRightRepository : IAppRightRepository
    {
        #region Global Variable Declaration

        //private readonly Repository< AppRight> _appRightRepository;
        private readonly RepositoryBase< AppRight> _appRightRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public  AppRightRepository(Repository< AppRight> appRightRepository, IUnitOfWork iUnitOfWork)
        {
            this._appRightRepository = appRightRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable< AppRightViewModel> GetAll()
        {
            var appRightViewModels = new List< AppRightViewModel>();
            try
            {

                List< AppRight> appRights = _appRightRepository.GetAll();

                foreach ( AppRight appRight in appRights)
                {
                    var appRightViewModel = appRight.ConvertModelToViewModel< AppRight,  AppRightViewModel>();
                    appRightViewModels.Add(appRightViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appRightViewModels.AsQueryable();
        }

        public  AppRightViewModel GetById(long id)
        {
            var appRightViewModel = new  AppRightViewModel();

            try
            {
                 AppRight appRight = _appRightRepository.GetById(id);
                appRightViewModel = appRight.ConvertModelToViewModel< AppRight,  AppRightViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appRightViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(AppRightViewModel appRightViewModel)
        {
            int isSave = 0;
            try
            {
                if (appRightViewModel != null)
                {
                    //add
                    if (appRightViewModel.AppRightId == default(int))
                    {
                        Create(appRightViewModel);
                    }
                    else //edit
                    {
                        Update(appRightViewModel);
                    }
                }
                else
                {
                    throw new ArgumentNullException("AppRightViewModel", "Request data is null.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create( AppRightViewModel appRightViewModel)
        {
            int isSave = 0;
            try
            {
                if (appRightViewModel != null)
                {
                     AppRight appRight = appRightViewModel.ConvertViewModelToModel< AppRightViewModel,  AppRight>();
                    _appRightRepository.Insert(appRight);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException(" AppRightViewModel", "Request data is null.");
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

        public int Update( AppRightViewModel appRightViewModel)
        {
            int isSave = 0;
            try
            {
                if (appRightViewModel != null)
                {
                     AppRight appRight = appRightViewModel.ConvertViewModelToModel< AppRightViewModel,  AppRight>();
                    _appRightRepository.Update(appRight);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException(" AppRightViewModel", "Request data is null.");
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

        public int Delete( AppRightViewModel appRightViewModel)
        {
            int isSave = 0;
            try
            {
                if (appRightViewModel != null)
                {
                    var viewModel = GetById(appRightViewModel. AppRightId);
                     AppRight appRight = viewModel.ConvertViewModelToModel< AppRightViewModel,  AppRight>();
                    _appRightRepository.Delete(appRight);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException(" AppRightViewModel", "Request data is null.");
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
                var appRightViewModel = GetById(id);
                if (appRightViewModel != null)
                {
                     AppRight appRight = appRightViewModel.ConvertViewModelToModel< AppRightViewModel,  AppRight>();
                    _appRightRepository.Delete(appRight);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException(" AppRightViewModel", "Request data is null.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List< AppRightViewModel> appRightViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appRightViewModel in appRightViewModels)
                {
                     AppRightViewModel viewModel = GetById(appRightViewModel. AppRightId);
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

    #region Interface :  AppRight

    public interface IAppRightRepository : IGeneric< AppRightViewModel>
    {
        int Delete(List< AppRightViewModel> appRightViewModels);
    }

    #endregion
}
