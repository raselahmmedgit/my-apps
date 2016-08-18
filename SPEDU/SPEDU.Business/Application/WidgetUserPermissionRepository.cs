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
    #region Interface Implement : WidgetUserPermission

    public class WidgetUserPermissionRepository : IWidgetUserPermissionRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<WidgetUserPermission> _appWidgetPermissionRepository;
        private readonly RepositoryBase<WidgetUserPermission> _appWidgetPermissionRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public WidgetUserPermissionRepository(Repository<WidgetUserPermission> appWidgetPermissionRepository, IUnitOfWork iUnitOfWork)
        {
            this._appWidgetPermissionRepository = appWidgetPermissionRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<WidgetUserPermissionViewModel> GetAll()
        {
            var appWidgetUserPermissionViewModels = new List<WidgetUserPermissionViewModel>();
            try
            {

                List<WidgetUserPermission> appWidgetPermissions = _appWidgetPermissionRepository.GetAll();

                foreach (WidgetUserPermission appWidgetPermission in appWidgetPermissions)
                {
                    var appWidgetUserPermissionViewModel = appWidgetPermission.ConvertModelToViewModel<WidgetUserPermission, WidgetUserPermissionViewModel>();
                    appWidgetUserPermissionViewModels.Add(appWidgetUserPermissionViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appWidgetUserPermissionViewModels.AsQueryable();
        }

        public WidgetUserPermissionViewModel GetById(long id)
        {
            var appWidgetUserPermissionViewModel = new WidgetUserPermissionViewModel();

            try
            {
                WidgetUserPermission appWidgetPermission = _appWidgetPermissionRepository.GetById(id);
                appWidgetUserPermissionViewModel = appWidgetPermission.ConvertModelToViewModel<WidgetUserPermission, WidgetUserPermissionViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appWidgetUserPermissionViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(WidgetUserPermissionViewModel appWidgetUserPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetUserPermissionViewModel != null)
                {
                    //add
                    if (appWidgetUserPermissionViewModel.WidgetPermissionId == default(int))
                    {
                        Create(appWidgetUserPermissionViewModel);
                    }
                    else //edit
                    {
                        Update(appWidgetUserPermissionViewModel);
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
        public int Create(WidgetUserPermissionViewModel appWidgetUserPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetUserPermissionViewModel != null)
                {
                    WidgetUserPermission appWidgetPermission = appWidgetUserPermissionViewModel.ConvertViewModelToModel<WidgetUserPermissionViewModel, WidgetUserPermission>();
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

        public int Update(WidgetUserPermissionViewModel appWidgetUserPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetUserPermissionViewModel != null)
                {
                    WidgetUserPermission appWidgetPermission = appWidgetUserPermissionViewModel.ConvertViewModelToModel<WidgetUserPermissionViewModel, WidgetUserPermission>();
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

        public int Delete(WidgetUserPermissionViewModel appWidgetUserPermissionViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetUserPermissionViewModel != null)
                {
                    var viewModel = GetById(appWidgetUserPermissionViewModel.WidgetPermissionId);
                    WidgetUserPermission appWidgetPermission = viewModel.ConvertViewModelToModel<WidgetUserPermissionViewModel, WidgetUserPermission>();
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
                var appWidgetUserPermissionViewModel = GetById(id);
                if (appWidgetUserPermissionViewModel != null)
                {
                    WidgetUserPermission appWidgetPermission = appWidgetUserPermissionViewModel.ConvertViewModelToModel<WidgetUserPermissionViewModel, WidgetUserPermission>();
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

        public int Delete(List<WidgetUserPermissionViewModel> appWidgetUserPermissionViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appWidgetUserPermissionViewModel in appWidgetUserPermissionViewModels)
                {
                    WidgetUserPermissionViewModel viewModel = GetById(appWidgetUserPermissionViewModel.WidgetPermissionId);
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

    #region Interface : WidgetUserPermission

    public interface IWidgetUserPermissionRepository : IGeneric<WidgetUserPermissionViewModel>
    {
        int Delete(List<WidgetUserPermissionViewModel> appWidgetUserPermissionViewModels);
    }

    #endregion
}
