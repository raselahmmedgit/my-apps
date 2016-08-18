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
    #region Interface Implement : DefaultSetting

    public class DefaultSettingRepository : IDefaultSettingRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<DefaultSetting> _appDefaultSettingRepository;
        private readonly RepositoryBase<DefaultSetting> _appDefaultSettingRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public DefaultSettingRepository(Repository<DefaultSetting> appDefaultSettingRepository, IUnitOfWork iUnitOfWork)
        {
            this._appDefaultSettingRepository = appDefaultSettingRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<DefaultSettingViewModel> GetAll()
        {
            var appDefaultSettingViewModels = new List<DefaultSettingViewModel>();
            try
            {

                List<DefaultSetting> appDefaultSettings = _appDefaultSettingRepository.GetAll();

                foreach (DefaultSetting appDefaultSetting in appDefaultSettings)
                {
                    var appDefaultSettingViewModel = appDefaultSetting.ConvertModelToViewModel<DefaultSetting, DefaultSettingViewModel>();
                    appDefaultSettingViewModels.Add(appDefaultSettingViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appDefaultSettingViewModels.AsQueryable();
        }

        public DefaultSettingViewModel GetById(long id)
        {
            var appDefaultSettingViewModel = new DefaultSettingViewModel();

            try
            {
                DefaultSetting appDefaultSetting = _appDefaultSettingRepository.GetById(id);
                appDefaultSettingViewModel = appDefaultSetting.ConvertModelToViewModel<DefaultSetting, DefaultSettingViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appDefaultSettingViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(DefaultSettingViewModel appDefaultSettingViewModel)
        {
            int isSave = 0;
            try
            {
                if (appDefaultSettingViewModel != null)
                {
                    //add
                    if (appDefaultSettingViewModel.DefaultSettingId == default(int))
                    {
                        Create(appDefaultSettingViewModel);
                    }
                    else //edit
                    {
                        Update(appDefaultSettingViewModel);
                    }
                }
                else
                {
                    throw new ArgumentNullException("DefaultSettingViewModel", MessageResourceHelper.NullError);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(DefaultSettingViewModel appDefaultSettingViewModel)
        {
            int isSave = 0;
            try
            {
                if (appDefaultSettingViewModel != null)
                {
                    DefaultSetting appDefaultSetting = appDefaultSettingViewModel.ConvertViewModelToModel<DefaultSettingViewModel, DefaultSetting>();
                    _appDefaultSettingRepository.Insert(appDefaultSetting);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("DefaultSettingViewModel", MessageResourceHelper.NullError);
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

        public int Update(DefaultSettingViewModel appDefaultSettingViewModel)
        {
            int isSave = 0;
            try
            {
                var updateDefaultSettingViewModel = GetById(appDefaultSettingViewModel.DefaultSettingId);

                if (updateDefaultSettingViewModel != null)
                {
                    DefaultSetting appDefaultSetting = appDefaultSettingViewModel.ConvertViewModelToModel<DefaultSettingViewModel, DefaultSetting>();
                    _appDefaultSettingRepository.Update(appDefaultSetting);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("DefaultSettingViewModel", MessageResourceHelper.NullError);
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

        public int Delete(DefaultSettingViewModel appDefaultSettingViewModel)
        {
            int isSave = 0;
            try
            {
                var deleteDefaultSettingViewModel = GetById(appDefaultSettingViewModel.DefaultSettingId);

                if (deleteDefaultSettingViewModel != null)
                {
                    var viewModel = GetById(appDefaultSettingViewModel.DefaultSettingId);
                    DefaultSetting appDefaultSetting = viewModel.ConvertViewModelToModel<DefaultSettingViewModel, DefaultSetting>();
                    _appDefaultSettingRepository.Delete(appDefaultSetting);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("DefaultSettingViewModel", MessageResourceHelper.NullError);
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
                var appDefaultSettingViewModel = GetById(id);
                if (appDefaultSettingViewModel != null)
                {
                    DefaultSetting appDefaultSetting = appDefaultSettingViewModel.ConvertViewModelToModel<DefaultSettingViewModel, DefaultSetting>();
                    _appDefaultSettingRepository.Delete(appDefaultSetting);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("DefaultSettingViewModel", MessageResourceHelper.NullError);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<DefaultSettingViewModel> appDefaultSettingViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appDefaultSettingViewModel in appDefaultSettingViewModels)
                {
                    DefaultSettingViewModel viewModel = GetById(appDefaultSettingViewModel.DefaultSettingId);
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

    #region Interface : DefaultSetting

    public interface IDefaultSettingRepository : IGeneric<DefaultSettingViewModel>
    {
        int Delete(List<DefaultSettingViewModel> appDefaultSettingViewModels);
    }

    #endregion
}
