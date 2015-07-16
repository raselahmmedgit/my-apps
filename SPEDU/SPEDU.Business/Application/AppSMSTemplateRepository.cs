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
    #region Interface Implement : AppSMSTemplate

    public class AppSMSTemplateRepository : IAppSMSTemplateRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<AppSMSTemplate> _appSMSTemplateRepository;
        private readonly RepositoryBase<AppSMSTemplate> _appSMSTemplateRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public AppSMSTemplateRepository(Repository<AppSMSTemplate> appSMSTemplateRepository, IUnitOfWork iUnitOfWork)
        {
            this._appSMSTemplateRepository = appSMSTemplateRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<AppSMSTemplateViewModel> GetAll()
        {
            var appSMSTemplateViewModels = new List<AppSMSTemplateViewModel>();
            try
            {

                List<AppSMSTemplate> appSMSTemplates = _appSMSTemplateRepository.GetAll();

                foreach (AppSMSTemplate appSMSTemplate in appSMSTemplates)
                {
                    var appSMSTemplateViewModel = appSMSTemplate.ConvertModelToViewModel<AppSMSTemplate, AppSMSTemplateViewModel>();
                    appSMSTemplateViewModels.Add(appSMSTemplateViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appSMSTemplateViewModels.AsQueryable();
        }

        public AppSMSTemplateViewModel GetById(long id)
        {
            var appSMSTemplateViewModel = new AppSMSTemplateViewModel();

            try
            {
                AppSMSTemplate appSMSTemplate = _appSMSTemplateRepository.GetById(id);
                appSMSTemplateViewModel = appSMSTemplate.ConvertModelToViewModel<AppSMSTemplate, AppSMSTemplateViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appSMSTemplateViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(AppSMSTemplateViewModel appSMSTemplateViewModel)
        {
            int isSave = 0;
            try
            {
                if (appSMSTemplateViewModel != null)
                {
                    //add
                    if (appSMSTemplateViewModel.AppSMSTemplateId == 0 && appSMSTemplateViewModel.ActionName == "Add")
                    {
                        Create(appSMSTemplateViewModel);
                    }
                    else if (appSMSTemplateViewModel.ActionName == "Edit") //edit
                    {
                        Update(appSMSTemplateViewModel);
                    }
                }
                else
                {
                    throw new ArgumentNullException("AppSMSTemplateViewModel", "Request data is null.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(AppSMSTemplateViewModel appSMSTemplateViewModel)
        {
            int isSave = 0;
            try
            {
                if (appSMSTemplateViewModel != null)
                {
                    AppSMSTemplate appSMSTemplate = appSMSTemplateViewModel.ConvertViewModelToModel<AppSMSTemplateViewModel, AppSMSTemplate>();
                    _appSMSTemplateRepository.Insert(appSMSTemplate);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppSMSTemplateViewModel", "Request data is null.");
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

        public int Update(AppSMSTemplateViewModel appSMSTemplateViewModel)
        {
            int isSave = 0;
            try
            {
                if (appSMSTemplateViewModel != null)
                {
                    AppSMSTemplate appSMSTemplate = appSMSTemplateViewModel.ConvertViewModelToModel<AppSMSTemplateViewModel, AppSMSTemplate>();
                    _appSMSTemplateRepository.Update(appSMSTemplate);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppSMSTemplateViewModel", "Request data is null.");
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

        public int Delete(AppSMSTemplateViewModel appSMSTemplateViewModel)
        {
            int isSave = 0;
            try
            {
                if (appSMSTemplateViewModel != null)
                {
                    var viewModel = GetById(appSMSTemplateViewModel.AppSMSTemplateId);
                    AppSMSTemplate appSMSTemplate = viewModel.ConvertViewModelToModel<AppSMSTemplateViewModel, AppSMSTemplate>();
                    _appSMSTemplateRepository.Delete(appSMSTemplate);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppSMSTemplateViewModel", "Request data is null.");
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
                var appSMSTemplateViewModel = GetById(id);
                if (appSMSTemplateViewModel != null)
                {
                    AppSMSTemplate appSMSTemplate = appSMSTemplateViewModel.ConvertViewModelToModel<AppSMSTemplateViewModel, AppSMSTemplate>();
                    _appSMSTemplateRepository.Delete(appSMSTemplate);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppSMSTemplateViewModel", "Request data is null.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<AppSMSTemplateViewModel> appSMSTemplateViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appSMSTemplateViewModel in appSMSTemplateViewModels)
                {
                    AppSMSTemplateViewModel viewModel = GetById(appSMSTemplateViewModel.AppSMSTemplateId);
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

    #region Interface : AppSMSTemplate

    public interface IAppSMSTemplateRepository : IGeneric<AppSMSTemplateViewModel>
    {
        int Delete(List<AppSMSTemplateViewModel> appSMSTemplateViewModels);
    }

    #endregion
}
