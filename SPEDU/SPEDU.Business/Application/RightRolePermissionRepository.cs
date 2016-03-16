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
    #region Interface Implement : RightRolePermission

    public class RightRolePermissionRepository : IRightRolePermissionRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<RightRolePermission> _appRightPermissionRepository;
        private readonly RepositoryBase<RightRolePermission> _appRightPermissionRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public RightRolePermissionRepository(Repository<RightRolePermission> appRightPermissionRepository, IUnitOfWork iUnitOfWork)
        {
            this._appRightPermissionRepository = appRightPermissionRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<RightRolePermissionViewModel> GetAll()
        {
            var appRightRolePermissionViewModels = new List<RightRolePermissionViewModel>();
            try
            {

                List<RightRolePermission> appRightPermissions = _appRightPermissionRepository.GetAll();

                foreach (RightRolePermission appRightPermission in appRightPermissions)
                {
                    var appRightRolePermissionViewModel = appRightPermission.ConvertModelToViewModel<RightRolePermission, RightRolePermissionViewModel>();
                    appRightRolePermissionViewModels.Add(appRightRolePermissionViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appRightRolePermissionViewModels.AsQueryable();
        }

        public RightRolePermissionViewModel GetById(long id)
        {
            var appRightRolePermissionViewModel = new RightRolePermissionViewModel();

            try
            {
                RightRolePermission appRightPermission = _appRightPermissionRepository.GetById(id);
                appRightRolePermissionViewModel = appRightPermission.ConvertModelToViewModel<RightRolePermission, RightRolePermissionViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appRightRolePermissionViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(RightRolePermissionViewModel appRightRolePermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appRightRolePermissionViewModel != null)
                {
                    //add
                    if (appRightRolePermissionViewModel.RightRolePermissionId == default(int))
                    {
                        Create(appRightRolePermissionViewModel);
                    }
                    else //edit
                    {
                        Update(appRightRolePermissionViewModel);
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
        public int Create(RightRolePermissionViewModel appRightRolePermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appRightRolePermissionViewModel != null)
                {
                    RightRolePermission appRightPermission = appRightRolePermissionViewModel.ConvertViewModelToModel<RightRolePermissionViewModel, RightRolePermission>();
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

        public int Update(RightRolePermissionViewModel appRightRolePermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appRightRolePermissionViewModel != null)
                {
                    RightRolePermission appRightPermission = appRightRolePermissionViewModel.ConvertViewModelToModel<RightRolePermissionViewModel, RightRolePermission>();
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

        public int Delete(RightRolePermissionViewModel appRightRolePermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appRightRolePermissionViewModel != null)
                {
                    var viewModel = GetById(appRightRolePermissionViewModel.RightRolePermissionId);
                    RightRolePermission appRightPermission = viewModel.ConvertViewModelToModel<RightRolePermissionViewModel, RightRolePermission>();
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
                var appRightRolePermissionViewModel = GetById(id);
                if (appRightRolePermissionViewModel != null)
                {
                    RightRolePermission appRightPermission = appRightRolePermissionViewModel.ConvertViewModelToModel<RightRolePermissionViewModel, RightRolePermission>();
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

        public int Delete(List<RightRolePermissionViewModel> appRightRolePermissionViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appRightRolePermissionViewModel in appRightRolePermissionViewModels)
                {
                    RightRolePermissionViewModel viewModel = GetById(appRightRolePermissionViewModel.RightRolePermissionId);
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

    #region Interface : RightRolePermission

    public interface IRightRolePermissionRepository : IGeneric<RightRolePermissionViewModel>
    {
        int Delete(List<RightRolePermissionViewModel> appRightRolePermissionViewModels);
    }

    #endregion
}
