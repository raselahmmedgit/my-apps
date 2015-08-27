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
    #region Interface Implement : AppEmailTemplate

    public class AppEmailTemplateRepository : IAppEmailTemplateRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<AppEmailTemplate> _appEmailTemplateRepository;
        private readonly RepositoryBase<AppEmailTemplate> _appEmailTemplateRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public AppEmailTemplateRepository(Repository<AppEmailTemplate> appEmailTemplateRepository, IUnitOfWork iUnitOfWork)
        {
            this._appEmailTemplateRepository = appEmailTemplateRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<AppEmailTemplateViewModel> GetAll()
        {
            var appEmailTemplateViewModels = new List<AppEmailTemplateViewModel>();
            try
            {

                List<AppEmailTemplate> appEmailTemplates = _appEmailTemplateRepository.GetAll();

                foreach (AppEmailTemplate appEmailTemplate in appEmailTemplates)
                {
                    var appEmailTemplateViewModel = appEmailTemplate.ConvertModelToViewModel<AppEmailTemplate, AppEmailTemplateViewModel>();
                    appEmailTemplateViewModels.Add(appEmailTemplateViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appEmailTemplateViewModels.AsQueryable();
        }

        public AppEmailTemplateViewModel GetById(long id)
        {
            var appEmailTemplateViewModel = new AppEmailTemplateViewModel();

            try
            {
                AppEmailTemplate appEmailTemplate = _appEmailTemplateRepository.GetById(id);
                appEmailTemplateViewModel = appEmailTemplate.ConvertModelToViewModel<AppEmailTemplate, AppEmailTemplateViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appEmailTemplateViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(AppEmailTemplateViewModel appEmailTemplateViewModel)
        {
            int isSave = 0;
            try
            {
                if (appEmailTemplateViewModel != null)
                {
                    //add
                    if (appEmailTemplateViewModel.AppEmailTemplateId == default(int))
                    {
                        Create(appEmailTemplateViewModel);
                    }
                    else //edit
                    {
                        Update(appEmailTemplateViewModel);
                    }
                }
                else
                {
                    throw new ArgumentNullException("AppEmailTemplateViewModel", "Request data is null.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(AppEmailTemplateViewModel appEmailTemplateViewModel)
        {
            int isSave = 0;
            try
            {
                if (appEmailTemplateViewModel != null)
                {
                    AppEmailTemplate appEmailTemplate = appEmailTemplateViewModel.ConvertViewModelToModel<AppEmailTemplateViewModel, AppEmailTemplate>();
                    _appEmailTemplateRepository.Insert(appEmailTemplate);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppEmailTemplateViewModel", "Request data is null.");
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

        public int Update(AppEmailTemplateViewModel appEmailTemplateViewModel)
        {
            int isSave = 0;
            try
            {
                if (appEmailTemplateViewModel != null)
                {
                    AppEmailTemplate appEmailTemplate = appEmailTemplateViewModel.ConvertViewModelToModel<AppEmailTemplateViewModel, AppEmailTemplate>();
                    _appEmailTemplateRepository.Update(appEmailTemplate);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppEmailTemplateViewModel", "Request data is null.");
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

        public int Delete(AppEmailTemplateViewModel appEmailTemplateViewModel)
        {
            int isSave = 0;
            try
            {
                if (appEmailTemplateViewModel != null)
                {
                    var viewModel = GetById(appEmailTemplateViewModel.AppEmailTemplateId);
                    AppEmailTemplate appEmailTemplate = viewModel.ConvertViewModelToModel<AppEmailTemplateViewModel, AppEmailTemplate>();
                    _appEmailTemplateRepository.Delete(appEmailTemplate);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppEmailTemplateViewModel", "Request data is null.");
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
                var appEmailTemplateViewModel = GetById(id);
                if (appEmailTemplateViewModel != null)
                {
                    AppEmailTemplate appEmailTemplate = appEmailTemplateViewModel.ConvertViewModelToModel<AppEmailTemplateViewModel, AppEmailTemplate>();
                    _appEmailTemplateRepository.Delete(appEmailTemplate);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppEmailTemplateViewModel", "Request data is null.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<AppEmailTemplateViewModel> appEmailTemplateViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appEmailTemplateViewModel in appEmailTemplateViewModels)
                {
                    AppEmailTemplateViewModel viewModel = GetById(appEmailTemplateViewModel.AppEmailTemplateId);
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

    #region Interface : AppEmailTemplate

    public interface IAppEmailTemplateRepository : IGeneric<AppEmailTemplateViewModel>
    {
        int Delete(List<AppEmailTemplateViewModel> appEmailTemplateViewModels);
    }

    #endregion
}
