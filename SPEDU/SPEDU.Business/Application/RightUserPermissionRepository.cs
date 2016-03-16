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
    #region Interface Implement : RightUserPermission

    public class RightUserPermissionRepository : IRightUserPermissionRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<RightUserPermission> _appRightPermissionRepository;
        private readonly RepositoryBase<RightUserPermission> _appRightPermissionRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public RightUserPermissionRepository(Repository<RightUserPermission> appRightPermissionRepository, IUnitOfWork iUnitOfWork)
        {
            this._appRightPermissionRepository = appRightPermissionRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<RightUserPermissionViewModel> GetAll()
        {
            var appRightUserPermissionViewModels = new List<RightUserPermissionViewModel>();
            try
            {

                List<RightUserPermission> appRightPermissions = _appRightPermissionRepository.GetAll();

                foreach (RightUserPermission appRightPermission in appRightPermissions)
                {
                    var appRightUserPermissionViewModel = appRightPermission.ConvertModelToViewModel<RightUserPermission, RightUserPermissionViewModel>();
                    appRightUserPermissionViewModels.Add(appRightUserPermissionViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appRightUserPermissionViewModels.AsQueryable();
        }

        public RightUserPermissionViewModel GetById(long id)
        {
            var appRightUserPermissionViewModel = new RightUserPermissionViewModel();

            try
            {
                RightUserPermission appRightPermission = _appRightPermissionRepository.GetById(id);
                appRightUserPermissionViewModel = appRightPermission.ConvertModelToViewModel<RightUserPermission, RightUserPermissionViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appRightUserPermissionViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(RightUserPermissionViewModel appRightUserPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appRightUserPermissionViewModel != null)
                {
                    //add
                    if (appRightUserPermissionViewModel.RightUserPermissionId == default(int))
                    {
                        Create(appRightUserPermissionViewModel);
                    }
                    else //edit
                    {
                        Update(appRightUserPermissionViewModel);
                    }
                }
                else
                {
                    throw new ArgumentNullException("RightPermissionViewModel", "Request data is null.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(RightUserPermissionViewModel appRightUserPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appRightUserPermissionViewModel != null)
                {
                    RightUserPermission appRightPermission = appRightUserPermissionViewModel.ConvertViewModelToModel<RightUserPermissionViewModel, RightUserPermission>();
                    _appRightPermissionRepository.Insert(appRightPermission);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("RightPermissionViewModel", "Request data is null.");
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

        public int Update(RightUserPermissionViewModel appRightUserPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appRightUserPermissionViewModel != null)
                {
                    RightUserPermission appRightPermission = appRightUserPermissionViewModel.ConvertViewModelToModel<RightUserPermissionViewModel, RightUserPermission>();
                    _appRightPermissionRepository.Update(appRightPermission);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("RightPermissionViewModel", "Request data is null.");
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

        public int Delete(RightUserPermissionViewModel appRightUserPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appRightUserPermissionViewModel != null)
                {
                    var viewModel = GetById(appRightUserPermissionViewModel.RightUserPermissionId);
                    RightUserPermission appRightPermission = viewModel.ConvertViewModelToModel<RightUserPermissionViewModel, RightUserPermission>();
                    _appRightPermissionRepository.Delete(appRightPermission);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("RightPermissionViewModel", "Request data is null.");
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
                var appRightUserPermissionViewModel = GetById(id);
                if (appRightUserPermissionViewModel != null)
                {
                    RightUserPermission appRightPermission = appRightUserPermissionViewModel.ConvertViewModelToModel<RightUserPermissionViewModel, RightUserPermission>();
                    _appRightPermissionRepository.Delete(appRightPermission);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("RightPermissionViewModel", "Request data is null.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<RightUserPermissionViewModel> appRightUserPermissionViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appRightUserPermissionViewModel in appRightUserPermissionViewModels)
                {
                    RightUserPermissionViewModel viewModel = GetById(appRightUserPermissionViewModel.RightUserPermissionId);
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

    #region Interface : RightUserPermission

    public interface IRightUserPermissionRepository : IGeneric<RightUserPermissionViewModel>
    {
        int Delete(List<RightUserPermissionViewModel> appRightUserPermissionViewModels);
    }

    #endregion
}
