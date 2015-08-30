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
    #region Interface Implement : AppWidgetCategory

    public class AppWidgetCategoryRepository : IAppWidgetCategoryRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<AppWidgetCategory> _appWidgetCategoryRepository;
        private readonly RepositoryBase<AppWidgetCategory> _appWidgetCategoryRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public AppWidgetCategoryRepository(Repository<AppWidgetCategory> appWidgetCategoryRepository, IUnitOfWork iUnitOfWork)
        {
            this._appWidgetCategoryRepository = appWidgetCategoryRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<AppWidgetCategoryViewModel> GetAll()
        {
            var appWidgetCategoryViewModels = new List<AppWidgetCategoryViewModel>();
            try
            {

                List<AppWidgetCategory> appWidgetCategorys = _appWidgetCategoryRepository.GetAll();

                foreach (AppWidgetCategory appWidgetCategory in appWidgetCategorys)
                {
                    var appWidgetCategoryViewModel = appWidgetCategory.ConvertModelToViewModel<AppWidgetCategory, AppWidgetCategoryViewModel>();
                    appWidgetCategoryViewModels.Add(appWidgetCategoryViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appWidgetCategoryViewModels.AsQueryable();
        }

        public AppWidgetCategoryViewModel GetById(long id)
        {
            var appWidgetCategoryViewModel = new AppWidgetCategoryViewModel();

            try
            {
                AppWidgetCategory appWidgetCategory = _appWidgetCategoryRepository.GetById(id);
                appWidgetCategoryViewModel = appWidgetCategory.ConvertModelToViewModel<AppWidgetCategory, AppWidgetCategoryViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appWidgetCategoryViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(AppWidgetCategoryViewModel appWidgetCategoryViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetCategoryViewModel != null)
                {
                    //add
                    if (appWidgetCategoryViewModel.AppWidgetCategoryId == default(int))
                    {
                        Create(appWidgetCategoryViewModel);
                    }
                    else //edit
                    {
                        Update(appWidgetCategoryViewModel);
                    }
                }
                else
                {
                    throw new ArgumentNullException("AppWidgetCategoryViewModel", "Request data is null.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(AppWidgetCategoryViewModel appWidgetCategoryViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetCategoryViewModel != null)
                {
                    AppWidgetCategory appWidgetCategory = appWidgetCategoryViewModel.ConvertViewModelToModel<AppWidgetCategoryViewModel, AppWidgetCategory>();
                    _appWidgetCategoryRepository.Insert(appWidgetCategory);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppWidgetCategoryViewModel", "Request data is null.");
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

        public int Update(AppWidgetCategoryViewModel appWidgetCategoryViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetCategoryViewModel != null)
                {
                    AppWidgetCategory appWidgetCategory = appWidgetCategoryViewModel.ConvertViewModelToModel<AppWidgetCategoryViewModel, AppWidgetCategory>();
                    _appWidgetCategoryRepository.Update(appWidgetCategory);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppWidgetCategoryViewModel", "Request data is null.");
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

        public int Delete(AppWidgetCategoryViewModel appWidgetCategoryViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetCategoryViewModel != null)
                {
                    var viewModel = GetById(appWidgetCategoryViewModel.AppWidgetCategoryId);
                    AppWidgetCategory appWidgetCategory = viewModel.ConvertViewModelToModel<AppWidgetCategoryViewModel, AppWidgetCategory>();
                    _appWidgetCategoryRepository.Delete(appWidgetCategory);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppWidgetCategoryViewModel", "Request data is null.");
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
                var appWidgetCategoryViewModel = GetById(id);
                if (appWidgetCategoryViewModel != null)
                {
                    AppWidgetCategory appWidgetCategory = appWidgetCategoryViewModel.ConvertViewModelToModel<AppWidgetCategoryViewModel, AppWidgetCategory>();
                    _appWidgetCategoryRepository.Delete(appWidgetCategory);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppWidgetCategoryViewModel", "Request data is null.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<AppWidgetCategoryViewModel> appWidgetCategoryViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appWidgetCategoryViewModel in appWidgetCategoryViewModels)
                {
                    AppWidgetCategoryViewModel viewModel = GetById(appWidgetCategoryViewModel.AppWidgetCategoryId);
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

    #region Interface : AppWidgetCategory

    public interface IAppWidgetCategoryRepository : IGeneric<AppWidgetCategoryViewModel>
    {
        int Delete(List<AppWidgetCategoryViewModel> appWidgetCategoryViewModels);
    }

    #endregion
}
