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
    #region Interface Implement : Widget

    public class WidgetRepository : IWidgetRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<Widget> _appWidgetRepository;
        private readonly RepositoryBase<Widget> _appWidgetRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public WidgetRepository(Repository<Widget> appWidgetRepository, IUnitOfWork iUnitOfWork)
        {
            this._appWidgetRepository = appWidgetRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<WidgetViewModel> GetAll()
        {
            var appWidgetViewModels = new List<WidgetViewModel>();
            try
            {

                List<Widget> appWidgets = _appWidgetRepository.GetAll();

                foreach (Widget appWidget in appWidgets)
                {
                    var appWidgetViewModel = appWidget.ConvertModelToViewModel<Widget, WidgetViewModel>();
                    appWidgetViewModels.Add(appWidgetViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appWidgetViewModels.AsQueryable();
        }

        public WidgetViewModel GetById(long id)
        {
            var appWidgetViewModel = new WidgetViewModel();

            try
            {
                Widget appWidget = _appWidgetRepository.GetById(id);
                appWidgetViewModel = appWidget.ConvertModelToViewModel<Widget, WidgetViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appWidgetViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(WidgetViewModel appWidgetViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetViewModel != null)
                {
                    //add
                    if (appWidgetViewModel.WidgetId == default(int))
                    {
                        Create(appWidgetViewModel);
                    }
                    else //edit
                    {
                        Update(appWidgetViewModel);
                    }
                }
                else
                {
                    throw new ArgumentNullException("WidgetViewModel", MessageResourceHelper.NullError);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(WidgetViewModel appWidgetViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetViewModel != null)
                {
                    Widget appWidget = appWidgetViewModel.ConvertViewModelToModel<WidgetViewModel, Widget>();
                    _appWidgetRepository.Insert(appWidget);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("WidgetViewModel", MessageResourceHelper.NullError);
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

        public int Update(WidgetViewModel appWidgetViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetViewModel != null)
                {
                    Widget appWidget = appWidgetViewModel.ConvertViewModelToModel<WidgetViewModel, Widget>();
                    _appWidgetRepository.Update(appWidget);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("WidgetViewModel", MessageResourceHelper.NullError);
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

        public int Delete(WidgetViewModel appWidgetViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetViewModel != null)
                {
                    var viewModel = GetById(appWidgetViewModel.WidgetId);
                    Widget appWidget = viewModel.ConvertViewModelToModel<WidgetViewModel, Widget>();
                    _appWidgetRepository.Delete(appWidget);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("WidgetViewModel", MessageResourceHelper.NullError);
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
                    Widget appWidget = appWidgetViewModel.ConvertViewModelToModel<WidgetViewModel, Widget>();
                    _appWidgetRepository.Delete(appWidget);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("WidgetViewModel", MessageResourceHelper.NullError);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<WidgetViewModel> appWidgetViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appWidgetViewModel in appWidgetViewModels)
                {
                    WidgetViewModel viewModel = GetById(appWidgetViewModel.WidgetId);
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

    #region Interface : Widget

    public interface IWidgetRepository : IGeneric<WidgetViewModel>
    {
        int Delete(List<WidgetViewModel> appWidgetViewModels);
    }

    #endregion
}
