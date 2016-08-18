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
    #region Interface Implement : WidgetCategory

    public class WidgetCategoryRepository : IWidgetCategoryRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<WidgetCategory> _appWidgetCategoryRepository;
        private readonly RepositoryBase<WidgetCategory> _appWidgetCategoryRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public WidgetCategoryRepository(Repository<WidgetCategory> appWidgetCategoryRepository, IUnitOfWork iUnitOfWork)
        {
            this._appWidgetCategoryRepository = appWidgetCategoryRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<WidgetCategoryViewModel> GetAll()
        {
            var appWidgetCategoryViewModels = new List<WidgetCategoryViewModel>();
            try
            {

                List<WidgetCategory> appWidgetCategorys = _appWidgetCategoryRepository.GetAll();

                foreach (WidgetCategory appWidgetCategory in appWidgetCategorys)
                {
                    var appWidgetCategoryViewModel = appWidgetCategory.ConvertModelToViewModel<WidgetCategory, WidgetCategoryViewModel>();
                    appWidgetCategoryViewModels.Add(appWidgetCategoryViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appWidgetCategoryViewModels.AsQueryable();
        }

        public WidgetCategoryViewModel GetById(long id)
        {
            var appWidgetCategoryViewModel = new WidgetCategoryViewModel();

            try
            {
                WidgetCategory appWidgetCategory = _appWidgetCategoryRepository.GetById(id);
                appWidgetCategoryViewModel = appWidgetCategory.ConvertModelToViewModel<WidgetCategory, WidgetCategoryViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appWidgetCategoryViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(WidgetCategoryViewModel appWidgetCategoryViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetCategoryViewModel != null)
                {
                    //add
                    if (appWidgetCategoryViewModel.WidgetCategoryId == default(int))
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
                    throw new ArgumentNullException("WidgetCategoryViewModel", MessageResourceHelper.NullError);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(WidgetCategoryViewModel appWidgetCategoryViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetCategoryViewModel != null)
                {
                    WidgetCategory appWidgetCategory = appWidgetCategoryViewModel.ConvertViewModelToModel<WidgetCategoryViewModel, WidgetCategory>();
                    _appWidgetCategoryRepository.Insert(appWidgetCategory);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("WidgetCategoryViewModel", MessageResourceHelper.NullError);
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

        public int Update(WidgetCategoryViewModel appWidgetCategoryViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetCategoryViewModel != null)
                {
                    WidgetCategory appWidgetCategory = appWidgetCategoryViewModel.ConvertViewModelToModel<WidgetCategoryViewModel, WidgetCategory>();
                    _appWidgetCategoryRepository.Update(appWidgetCategory);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("WidgetCategoryViewModel", MessageResourceHelper.NullError);
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

        public int Delete(WidgetCategoryViewModel appWidgetCategoryViewModel)
        {
            int isSave = 0;
            try
            {
                if (appWidgetCategoryViewModel != null)
                {
                    var viewModel = GetById(appWidgetCategoryViewModel.WidgetCategoryId);
                    WidgetCategory appWidgetCategory = viewModel.ConvertViewModelToModel<WidgetCategoryViewModel, WidgetCategory>();
                    _appWidgetCategoryRepository.Delete(appWidgetCategory);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("WidgetCategoryViewModel", MessageResourceHelper.NullError);
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
                    WidgetCategory appWidgetCategory = appWidgetCategoryViewModel.ConvertViewModelToModel<WidgetCategoryViewModel, WidgetCategory>();
                    _appWidgetCategoryRepository.Delete(appWidgetCategory);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("WidgetCategoryViewModel", MessageResourceHelper.NullError);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<WidgetCategoryViewModel> appWidgetCategoryViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appWidgetCategoryViewModel in appWidgetCategoryViewModels)
                {
                    WidgetCategoryViewModel viewModel = GetById(appWidgetCategoryViewModel.WidgetCategoryId);
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

    #region Interface : WidgetCategory

    public interface IWidgetCategoryRepository : IGeneric<WidgetCategoryViewModel>
    {
        int Delete(List<WidgetCategoryViewModel> appWidgetCategoryViewModels);
    }

    #endregion
}
