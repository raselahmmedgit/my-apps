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
    #region Interface Implement : AppEmailTemplateCategory

    public class AppEmailTemplateCategoryRepository : IAppEmailTemplateCategoryRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<AppEmailTemplateCategory> _appEmailTemplateCategoryRepository;
        private readonly RepositoryBase<AppEmailTemplateCategory> _appEmailTemplateCategoryRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public AppEmailTemplateCategoryRepository(Repository<AppEmailTemplateCategory> appEmailTemplateCategoryRepository, IUnitOfWork iUnitOfWork)
        {
            this._appEmailTemplateCategoryRepository = appEmailTemplateCategoryRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<AppEmailTemplateCategoryViewModel> GetAll()
        {
            var appEmailTemplateCategoryViewModels = new List<AppEmailTemplateCategoryViewModel>();
            try
            {

                List<AppEmailTemplateCategory> appEmailTemplateCategorys = _appEmailTemplateCategoryRepository.GetAll();

                foreach (AppEmailTemplateCategory appEmailTemplateCategory in appEmailTemplateCategorys)
                {
                    var appEmailTemplateCategoryViewModel = appEmailTemplateCategory.ConvertModelToViewModel<AppEmailTemplateCategory, AppEmailTemplateCategoryViewModel>();
                    appEmailTemplateCategoryViewModels.Add(appEmailTemplateCategoryViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appEmailTemplateCategoryViewModels.AsQueryable();
        }

        public AppEmailTemplateCategoryViewModel GetById(long id)
        {
            var appEmailTemplateCategoryViewModel = new AppEmailTemplateCategoryViewModel();

            try
            {
                AppEmailTemplateCategory appEmailTemplateCategory = _appEmailTemplateCategoryRepository.GetById(id);
                appEmailTemplateCategoryViewModel = appEmailTemplateCategory.ConvertModelToViewModel<AppEmailTemplateCategory, AppEmailTemplateCategoryViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appEmailTemplateCategoryViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(AppEmailTemplateCategoryViewModel appEmailTemplateCategoryViewModel)
        {
            int isSave = 0;
            try
            {
                if (appEmailTemplateCategoryViewModel != null)
                {
                    //add
                    if (appEmailTemplateCategoryViewModel.AppEmailTemplateCategoryId == default(int))
                    {
                        Create(appEmailTemplateCategoryViewModel);
                    }
                    else //edit
                    {
                        Update(appEmailTemplateCategoryViewModel);
                    }
                }
                else
                {
                    throw new ArgumentNullException("AppEmailTemplateCategoryViewModel", "Request data is null.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(AppEmailTemplateCategoryViewModel appEmailTemplateCategoryViewModel)
        {
            int isSave = 0;
            try
            {
                if (appEmailTemplateCategoryViewModel != null)
                {
                    AppEmailTemplateCategory appEmailTemplateCategory = appEmailTemplateCategoryViewModel.ConvertViewModelToModel<AppEmailTemplateCategoryViewModel, AppEmailTemplateCategory>();
                    _appEmailTemplateCategoryRepository.Insert(appEmailTemplateCategory);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppEmailTemplateCategoryViewModel", "Request data is null.");
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

        public int Update(AppEmailTemplateCategoryViewModel appEmailTemplateCategoryViewModel)
        {
            int isSave = 0;
            try
            {
                if (appEmailTemplateCategoryViewModel != null)
                {
                    AppEmailTemplateCategory appEmailTemplateCategory = appEmailTemplateCategoryViewModel.ConvertViewModelToModel<AppEmailTemplateCategoryViewModel, AppEmailTemplateCategory>();
                    _appEmailTemplateCategoryRepository.Update(appEmailTemplateCategory);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppEmailTemplateCategoryViewModel", "Request data is null.");
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

        public int Delete(AppEmailTemplateCategoryViewModel appEmailTemplateCategoryViewModel)
        {
            int isSave = 0;
            try
            {
                if (appEmailTemplateCategoryViewModel != null)
                {
                    var viewModel = GetById(appEmailTemplateCategoryViewModel.AppEmailTemplateCategoryId);
                    AppEmailTemplateCategory appEmailTemplateCategory = viewModel.ConvertViewModelToModel<AppEmailTemplateCategoryViewModel, AppEmailTemplateCategory>();
                    _appEmailTemplateCategoryRepository.Delete(appEmailTemplateCategory);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppEmailTemplateCategoryViewModel", "Request data is null.");
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
                var appEmailTemplateCategoryViewModel = GetById(id);
                if (appEmailTemplateCategoryViewModel != null)
                {
                    AppEmailTemplateCategory appEmailTemplateCategory = appEmailTemplateCategoryViewModel.ConvertViewModelToModel<AppEmailTemplateCategoryViewModel, AppEmailTemplateCategory>();
                    _appEmailTemplateCategoryRepository.Delete(appEmailTemplateCategory);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppEmailTemplateCategoryViewModel", "Request data is null.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<AppEmailTemplateCategoryViewModel> appEmailTemplateCategoryViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appEmailTemplateCategoryViewModel in appEmailTemplateCategoryViewModels)
                {
                    AppEmailTemplateCategoryViewModel viewModel = GetById(appEmailTemplateCategoryViewModel.AppEmailTemplateCategoryId);
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

    #region Interface : AppEmailTemplateCategory

    public interface IAppEmailTemplateCategoryRepository : IGeneric<AppEmailTemplateCategoryViewModel>
    {
        int Delete(List<AppEmailTemplateCategoryViewModel> appEmailTemplateCategoryViewModels);
    }

    #endregion
}
