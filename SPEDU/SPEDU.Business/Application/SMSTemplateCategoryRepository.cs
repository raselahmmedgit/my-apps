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
    #region Interface Implement : SMSTemplateCategory

    public class SMSTemplateCategoryRepository : ISMSTemplateCategoryRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<SMSTemplateCategory> _appSMSTemplateCategoryRepository;
        private readonly RepositoryBase<SMSTemplateCategory> _appSMSTemplateCategoryRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public SMSTemplateCategoryRepository(Repository<SMSTemplateCategory> appSMSTemplateCategoryRepository, IUnitOfWork iUnitOfWork)
        {
            this._appSMSTemplateCategoryRepository = appSMSTemplateCategoryRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<SMSTemplateCategoryViewModel> GetAll()
        {
            var appSMSTemplateCategoryViewModels = new List<SMSTemplateCategoryViewModel>();
            try
            {

                List<SMSTemplateCategory> appSMSTemplateCategorys = _appSMSTemplateCategoryRepository.GetAll();

                foreach (SMSTemplateCategory appSMSTemplateCategory in appSMSTemplateCategorys)
                {
                    var appSMSTemplateCategoryViewModel = appSMSTemplateCategory.ConvertModelToViewModel<SMSTemplateCategory, SMSTemplateCategoryViewModel>();
                    appSMSTemplateCategoryViewModels.Add(appSMSTemplateCategoryViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appSMSTemplateCategoryViewModels.AsQueryable();
        }

        public SMSTemplateCategoryViewModel GetById(long id)
        {
            var appSMSTemplateCategoryViewModel = new SMSTemplateCategoryViewModel();

            try
            {
                SMSTemplateCategory appSMSTemplateCategory = _appSMSTemplateCategoryRepository.GetById(id);
                appSMSTemplateCategoryViewModel = appSMSTemplateCategory.ConvertModelToViewModel<SMSTemplateCategory, SMSTemplateCategoryViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appSMSTemplateCategoryViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(SMSTemplateCategoryViewModel appSMSTemplateCategoryViewModel)
        {
            int isSave = 0;
            try
            {
                if (appSMSTemplateCategoryViewModel != null)
                {
                    //add
                    if (appSMSTemplateCategoryViewModel.SMSTemplateCategoryId == default(int))
                    {
                        Create(appSMSTemplateCategoryViewModel);
                    }
                    else //edit
                    {
                        Update(appSMSTemplateCategoryViewModel);
                    }
                }
                else
                {
                    throw new ArgumentNullException("SMSTemplateCategoryViewModel", MessageResourceHelper.NullError);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(SMSTemplateCategoryViewModel appSMSTemplateCategoryViewModel)
        {
            int isSave = 0;
            try
            {
                if (appSMSTemplateCategoryViewModel != null)
                {
                    SMSTemplateCategory appSMSTemplateCategory = appSMSTemplateCategoryViewModel.ConvertViewModelToModel<SMSTemplateCategoryViewModel, SMSTemplateCategory>();
                    _appSMSTemplateCategoryRepository.Insert(appSMSTemplateCategory);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("SMSTemplateCategoryViewModel", MessageResourceHelper.NullError);
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

        public int Update(SMSTemplateCategoryViewModel appSMSTemplateCategoryViewModel)
        {
            int isSave = 0;
            try
            {
                if (appSMSTemplateCategoryViewModel != null)
                {
                    SMSTemplateCategory appSMSTemplateCategory = appSMSTemplateCategoryViewModel.ConvertViewModelToModel<SMSTemplateCategoryViewModel, SMSTemplateCategory>();
                    _appSMSTemplateCategoryRepository.Update(appSMSTemplateCategory);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("SMSTemplateCategoryViewModel", MessageResourceHelper.NullError);
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

        public int Delete(SMSTemplateCategoryViewModel appSMSTemplateCategoryViewModel)
        {
            int isSave = 0;
            try
            {
                if (appSMSTemplateCategoryViewModel != null)
                {
                    var viewModel = GetById(appSMSTemplateCategoryViewModel.SMSTemplateCategoryId);
                    SMSTemplateCategory appSMSTemplateCategory = viewModel.ConvertViewModelToModel<SMSTemplateCategoryViewModel, SMSTemplateCategory>();
                    _appSMSTemplateCategoryRepository.Delete(appSMSTemplateCategory);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("SMSTemplateCategoryViewModel", MessageResourceHelper.NullError);
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
                    SMSTemplateCategory appSMSTemplateCategory = appSMSTemplateCategoryViewModel.ConvertViewModelToModel<SMSTemplateCategoryViewModel, SMSTemplateCategory>();
                    _appSMSTemplateCategoryRepository.Delete(appSMSTemplateCategory);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("SMSTemplateCategoryViewModel", MessageResourceHelper.NullError);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<SMSTemplateCategoryViewModel> appSMSTemplateCategoryViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appSMSTemplateCategoryViewModel in appSMSTemplateCategoryViewModels)
                {
                    SMSTemplateCategoryViewModel viewModel = GetById(appSMSTemplateCategoryViewModel.SMSTemplateCategoryId);
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

    #region Interface : SMSTemplateCategory

    public interface ISMSTemplateCategoryRepository : IGeneric<SMSTemplateCategoryViewModel>
    {
        int Delete(List<SMSTemplateCategoryViewModel> appSMSTemplateCategoryViewModels);
    }

    #endregion
}
