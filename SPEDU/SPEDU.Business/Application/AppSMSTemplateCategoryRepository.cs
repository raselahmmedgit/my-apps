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
    #region Interface Implement : AppSMSTemplateCategory

    public class AppSMSTemplateCategoryRepository : IAppSMSTemplateCategoryRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<AppSMSTemplateCategory> _appSMSTemplateCategoryRepository;
        private readonly RepositoryBase<AppSMSTemplateCategory> _appSMSTemplateCategoryRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public AppSMSTemplateCategoryRepository(Repository<AppSMSTemplateCategory> appSMSTemplateCategoryRepository, IUnitOfWork iUnitOfWork)
        {
            this._appSMSTemplateCategoryRepository = appSMSTemplateCategoryRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<AppSMSTemplateCategoryViewModel> GetAll()
        {
            var appSMSTemplateCategoryViewModels = new List<AppSMSTemplateCategoryViewModel>();
            try
            {

                List<AppSMSTemplateCategory> appSMSTemplateCategorys = _appSMSTemplateCategoryRepository.GetAll();

                foreach (AppSMSTemplateCategory appSMSTemplateCategory in appSMSTemplateCategorys)
                {
                    var appSMSTemplateCategoryViewModel = appSMSTemplateCategory.ConvertModelToViewModel<AppSMSTemplateCategory, AppSMSTemplateCategoryViewModel>();
                    appSMSTemplateCategoryViewModels.Add(appSMSTemplateCategoryViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appSMSTemplateCategoryViewModels.AsQueryable();
        }

        public AppSMSTemplateCategoryViewModel GetById(long id)
        {
            var appSMSTemplateCategoryViewModel = new AppSMSTemplateCategoryViewModel();

            try
            {
                AppSMSTemplateCategory appSMSTemplateCategory = _appSMSTemplateCategoryRepository.GetById(id);
                appSMSTemplateCategoryViewModel = appSMSTemplateCategory.ConvertModelToViewModel<AppSMSTemplateCategory, AppSMSTemplateCategoryViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appSMSTemplateCategoryViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(AppSMSTemplateCategoryViewModel appSMSTemplateCategoryViewModel)
        {
            int isSave = 0;
            try
            {
                if (appSMSTemplateCategoryViewModel != null)
                {
                    //add
                    if (appSMSTemplateCategoryViewModel.AppSMSTemplateCategoryId == 0 && appSMSTemplateCategoryViewModel.ActionName == "Add")
                    {
                        Create(appSMSTemplateCategoryViewModel);
                    }
                    else if (appSMSTemplateCategoryViewModel.ActionName == "Edit") //edit
                    {
                        Update(appSMSTemplateCategoryViewModel);
                    }
                }
                else
                {
                    throw new ArgumentNullException("AppSMSTemplateCategoryViewModel", "Request data is null.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(AppSMSTemplateCategoryViewModel appSMSTemplateCategoryViewModel)
        {
            int isSave = 0;
            try
            {
                if (appSMSTemplateCategoryViewModel != null)
                {
                    AppSMSTemplateCategory appSMSTemplateCategory = appSMSTemplateCategoryViewModel.ConvertViewModelToModel<AppSMSTemplateCategoryViewModel, AppSMSTemplateCategory>();
                    _appSMSTemplateCategoryRepository.Insert(appSMSTemplateCategory);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppSMSTemplateCategoryViewModel", "Request data is null.");
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

        public int Update(AppSMSTemplateCategoryViewModel appSMSTemplateCategoryViewModel)
        {
            int isSave = 0;
            try
            {
                if (appSMSTemplateCategoryViewModel != null)
                {
                    AppSMSTemplateCategory appSMSTemplateCategory = appSMSTemplateCategoryViewModel.ConvertViewModelToModel<AppSMSTemplateCategoryViewModel, AppSMSTemplateCategory>();
                    _appSMSTemplateCategoryRepository.Update(appSMSTemplateCategory);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppSMSTemplateCategoryViewModel", "Request data is null.");
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

        public int Delete(AppSMSTemplateCategoryViewModel appSMSTemplateCategoryViewModel)
        {
            int isSave = 0;
            try
            {
                if (appSMSTemplateCategoryViewModel != null)
                {
                    var viewModel = GetById(appSMSTemplateCategoryViewModel.AppSMSTemplateCategoryId);
                    AppSMSTemplateCategory appSMSTemplateCategory = viewModel.ConvertViewModelToModel<AppSMSTemplateCategoryViewModel, AppSMSTemplateCategory>();
                    _appSMSTemplateCategoryRepository.Delete(appSMSTemplateCategory);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppSMSTemplateCategoryViewModel", "Request data is null.");
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
                var appSMSTemplateCategoryViewModel = GetById(id);
                if (appSMSTemplateCategoryViewModel != null)
                {
                    AppSMSTemplateCategory appSMSTemplateCategory = appSMSTemplateCategoryViewModel.ConvertViewModelToModel<AppSMSTemplateCategoryViewModel, AppSMSTemplateCategory>();
                    _appSMSTemplateCategoryRepository.Delete(appSMSTemplateCategory);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppSMSTemplateCategoryViewModel", "Request data is null.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<AppSMSTemplateCategoryViewModel> appSMSTemplateCategoryViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appSMSTemplateCategoryViewModel in appSMSTemplateCategoryViewModels)
                {
                    AppSMSTemplateCategoryViewModel viewModel = GetById(appSMSTemplateCategoryViewModel.AppSMSTemplateCategoryId);
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

    #region Interface : AppSMSTemplateCategory

    public interface IAppSMSTemplateCategoryRepository : IGeneric<AppSMSTemplateCategoryViewModel>
    {
        int Delete(List<AppSMSTemplateCategoryViewModel> appSMSTemplateCategoryViewModels);
    }

    #endregion
}
