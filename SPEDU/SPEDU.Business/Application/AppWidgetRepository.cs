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
    #region Interface Implement : AppWidget

    public class AppWidgetRepository : IAppWidgetRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<AppWidget> _appWidgetRepository;
        private readonly RepositoryBase<AppWidget> _appWidgetRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public AppWidgetRepository(Repository<AppWidget> appWidgetRepository, IUnitOfWork iUnitOfWork)
        {
            this._appWidgetRepository = appWidgetRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<AppWidgetViewModel> GetAll()
        {
            var appWidgetViewModels = new List<AppWidgetViewModel>();
            try
            {

                List<AppWidget> appWidgets = _appWidgetRepository.GetAll();

                foreach (AppWidget appWidget in appWidgets)
                {
                    var appWidgetViewModel = appWidget.ConvertModelToViewModel<AppWidget, AppWidgetViewModel>();
                    appWidgetViewModels.Add(appWidgetViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appWidgetViewModels.AsQueryable();
        }

        public AppWidgetViewModel GetById(long id)
        {
            var appWidgetViewModel = new AppWidgetViewModel();

            try
            {
                AppWidget appWidget = _appWidgetRepository.GetById(id);
                appWidgetViewModel = appWidget.ConvertModelToViewModel<AppWidget, AppWidgetViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appWidgetViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(AppWidgetViewModel appWidgetViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetViewModel != null)
                {
                    //add
                    if (appWidgetViewModel.AppWidgetId == 0 && appWidgetViewModel.ActionName == "Add")
                    {
                        Create(appWidgetViewModel);
                    }
                    else if (appWidgetViewModel.ActionName == "Edit") //edit
                    {
                        Update(appWidgetViewModel);
                    }
                }
                else
                {
                    throw new ArgumentNullException("AppWidgetViewModel", "Request data is null.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(AppWidgetViewModel appWidgetViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetViewModel != null)
                {
                    AppWidget appWidget = appWidgetViewModel.ConvertViewModelToModel<AppWidgetViewModel, AppWidget>();
                    _appWidgetRepository.Insert(appWidget);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppWidgetViewModel", "Request data is null.");
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

        public int Update(AppWidgetViewModel appWidgetViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetViewModel != null)
                {
                    AppWidget appWidget = appWidgetViewModel.ConvertViewModelToModel<AppWidgetViewModel, AppWidget>();
                    _appWidgetRepository.Update(appWidget);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppWidgetViewModel", "Request data is null.");
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

        public int Delete(AppWidgetViewModel appWidgetViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetViewModel != null)
                {
                    var viewModel = GetById(appWidgetViewModel.AppWidgetId);
                    AppWidget appWidget = viewModel.ConvertViewModelToModel<AppWidgetViewModel, AppWidget>();
                    _appWidgetRepository.Delete(appWidget);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppWidgetViewModel", "Request data is null.");
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
                var appWidgetViewModel = GetById(id);
                if (appWidgetViewModel != null)
                {
                    AppWidget appWidget = appWidgetViewModel.ConvertViewModelToModel<AppWidgetViewModel, AppWidget>();
                    _appWidgetRepository.Delete(appWidget);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("AppWidgetViewModel", "Request data is null.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<AppWidgetViewModel> appWidgetViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appWidgetViewModel in appWidgetViewModels)
                {
                    AppWidgetViewModel viewModel = GetById(appWidgetViewModel.AppWidgetId);
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

    #region Interface : AppWidget

    public interface IAppWidgetRepository : IGeneric<AppWidgetViewModel>
    {
        int Delete(List<AppWidgetViewModel> appWidgetViewModels);
    }

    #endregion
}
