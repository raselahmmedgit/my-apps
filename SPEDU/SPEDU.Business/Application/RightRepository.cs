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
    #region Interface Implement :  Right

    public class RightRepository : IRightRepository
    {
        #region Global Variable Declaration

        //private readonly Repository< Right> _appRightRepository;
        private readonly RepositoryBase< Right> _appRightRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public  RightRepository(Repository< Right> appRightRepository, IUnitOfWork iUnitOfWork)
        {
            this._appRightRepository = appRightRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable< RightViewModel> GetAll()
        {
            var appRightViewModels = new List< RightViewModel>();
            try
            {

                List< Right> appRights = _appRightRepository.GetAll();

                foreach ( Right appRight in appRights)
                {
                    var appRightViewModel = appRight.ConvertModelToViewModel< Right,  RightViewModel>();
                    appRightViewModels.Add(appRightViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appRightViewModels.AsQueryable();
        }

        public  RightViewModel GetById(long id)
        {
            var appRightViewModel = new  RightViewModel();

            try
            {
                 Right appRight = _appRightRepository.GetById(id);
                appRightViewModel = appRight.ConvertModelToViewModel< Right,  RightViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appRightViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(RightViewModel appRightViewModel)
        {
            int isSave = 0;
            try
            {
                if (appRightViewModel != null)
                {
                    //add
                    if (appRightViewModel.RightId == default(int))
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
                    throw new ArgumentNullException("RightViewModel", MessageResourceHelper.NullError);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create( RightViewModel appRightViewModel)
        {
            int isSave = 0;
            try
            {
                if (appRightViewModel != null)
                {
                     Right appRight = appRightViewModel.ConvertViewModelToModel< RightViewModel,  Right>();
                    _appRightRepository.Insert(appRight);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException(" RightViewModel", MessageResourceHelper.NullError);
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

        public int Update( RightViewModel appRightViewModel)
        {
            int isSave = 0;
            try
            {
                if (appRightViewModel != null)
                {
                     Right appRight = appRightViewModel.ConvertViewModelToModel< RightViewModel,  Right>();
                    _appRightRepository.Update(appRight);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException(" RightViewModel", MessageResourceHelper.NullError);
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

        public int Delete( RightViewModel appRightViewModel)
        {
            int isSave = 0;
            try
            {
                if (appRightViewModel != null)
                {
                    var viewModel = GetById(appRightViewModel. RightId);
                     Right appRight = viewModel.ConvertViewModelToModel< RightViewModel,  Right>();
                    _appRightRepository.Delete(appRight);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException(" RightViewModel", MessageResourceHelper.NullError);
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
                     Right appRight = appRightViewModel.ConvertViewModelToModel< RightViewModel,  Right>();
                    _appRightRepository.Delete(appRight);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException(" RightViewModel", MessageResourceHelper.NullError);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List< RightViewModel> appRightViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appRightViewModel in appRightViewModels)
                {
                     RightViewModel viewModel = GetById(appRightViewModel. RightId);
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

    #region Interface :  Right

    public interface IRightRepository : IGeneric< RightViewModel>
    {
        int Delete(List< RightViewModel> appRightViewModels);
    }

    #endregion
}
