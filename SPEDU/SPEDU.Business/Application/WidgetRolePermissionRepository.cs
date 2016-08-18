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
    #region Interface Implement : WidgetRolePermission

    public class WidgetRolePermissionRepository : IWidgetRolePermissionRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<WidgetRolePermission> _appWidgetPermissionRepository;
        private readonly RepositoryBase<WidgetRolePermission> _appWidgetPermissionRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public WidgetRolePermissionRepository(Repository<WidgetRolePermission> appWidgetPermissionRepository, IUnitOfWork iUnitOfWork)
        {
            this._appWidgetPermissionRepository = appWidgetPermissionRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<WidgetRolePermissionViewModel> GetAll()
        {
            var appWidgetRolePermissionViewModels = new List<WidgetRolePermissionViewModel>();
            try
            {

                List<WidgetRolePermission> appWidgetPermissions = _appWidgetPermissionRepository.GetAll();

                foreach (WidgetRolePermission appWidgetPermission in appWidgetPermissions)
                {
                    var appWidgetRolePermissionViewModel = appWidgetPermission.ConvertModelToViewModel<WidgetRolePermission, WidgetRolePermissionViewModel>();
                    appWidgetRolePermissionViewModels.Add(appWidgetRolePermissionViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appWidgetRolePermissionViewModels.AsQueryable();
        }

        public WidgetRolePermissionViewModel GetById(long id)
        {
            var appWidgetRolePermissionViewModel = new WidgetRolePermissionViewModel();

            try
            {
                WidgetRolePermission appWidgetPermission = _appWidgetPermissionRepository.GetById(id);
                appWidgetRolePermissionViewModel = appWidgetPermission.ConvertModelToViewModel<WidgetRolePermission, WidgetRolePermissionViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appWidgetRolePermissionViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(WidgetRolePermissionViewModel appWidgetRolePermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetRolePermissionViewModel != null)
                {
                    //add
                    if (appWidgetRolePermissionViewModel.WidgetPermissionId == default(int))
                    {
                        Create(appWidgetRolePermissionViewModel);
                    }
                    else //edit
                    {
                        Update(appWidgetRolePermissionViewModel);
                    }
                }
                else
                {
                    throw new ArgumentNullException("WidgetPermissionViewModel", MessageResourceHelper.NullError);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(WidgetRolePermissionViewModel appWidgetRolePermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetRolePermissionViewModel != null)
                {
                    WidgetRolePermission appWidgetPermission = appWidgetRolePermissionViewModel.ConvertViewModelToModel<WidgetRolePermissionViewModel, WidgetRolePermission>();
                    _appWidgetPermissionRepository.Insert(appWidgetPermission);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("WidgetPermissionViewModel", MessageResourceHelper.NullError);
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

        public int Update(WidgetRolePermissionViewModel appWidgetRolePermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetRolePermissionViewModel != null)
                {
                    WidgetRolePermission appWidgetPermission = appWidgetRolePermissionViewModel.ConvertViewModelToModel<WidgetRolePermissionViewModel, WidgetRolePermission>();
                    _appWidgetPermissionRepository.Update(appWidgetPermission);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("WidgetPermissionViewModel", MessageResourceHelper.NullError);
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

        public int Delete(WidgetRolePermissionViewModel appWidgetRolePermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetRolePermissionViewModel != null)
                {
                    var viewModel = GetById(appWidgetRolePermissionViewModel.WidgetPermissionId);
                    WidgetRolePermission appWidgetPermission = viewModel.ConvertViewModelToModel<WidgetRolePermissionViewModel, WidgetRolePermission>();
                    _appWidgetPermissionRepository.Delete(appWidgetPermission);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("WidgetPermissionViewModel", MessageResourceHelper.NullError);
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
                var appWidgetRolePermissionViewModel = GetById(id);
                if (appWidgetRolePermissionViewModel != null)
                {
                    WidgetRolePermission appWidgetPermission = appWidgetRolePermissionViewModel.ConvertViewModelToModel<WidgetRolePermissionViewModel, WidgetRolePermission>();
                    _appWidgetPermissionRepository.Delete(appWidgetPermission);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("WidgetPermissionViewModel", MessageResourceHelper.NullError);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<WidgetRolePermissionViewModel> appWidgetRolePermissionViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appWidgetRolePermissionViewModel in appWidgetRolePermissionViewModels)
                {
                    WidgetRolePermissionViewModel viewModel = GetById(appWidgetRolePermissionViewModel.WidgetPermissionId);
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

    #region Interface : WidgetRolePermission

    public interface IWidgetRolePermissionRepository : IGeneric<WidgetRolePermissionViewModel>
    {
        int Delete(List<WidgetRolePermissionViewModel> appWidgetRolePermissionViewModels);
    }

    #endregion
}
