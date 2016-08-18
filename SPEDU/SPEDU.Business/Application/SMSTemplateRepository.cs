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
    #region Interface Implement : SMSTemplate

    public class SMSTemplateRepository : ISMSTemplateRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<SMSTemplate> _appSMSTemplateRepository;
        private readonly RepositoryBase<SMSTemplate> _appSMSTemplateRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public SMSTemplateRepository(Repository<SMSTemplate> appSMSTemplateRepository, IUnitOfWork iUnitOfWork)
        {
            this._appSMSTemplateRepository = appSMSTemplateRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<SMSTemplateViewModel> GetAll()
        {
            var appSMSTemplateViewModels = new List<SMSTemplateViewModel>();
            try
            {

                List<SMSTemplate> appSMSTemplates = _appSMSTemplateRepository.GetAll();

                foreach (SMSTemplate appSMSTemplate in appSMSTemplates)
                {
                    var appSMSTemplateViewModel = appSMSTemplate.ConvertModelToViewModel<SMSTemplate, SMSTemplateViewModel>();
                    appSMSTemplateViewModels.Add(appSMSTemplateViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appSMSTemplateViewModels.AsQueryable();
        }

        public SMSTemplateViewModel GetById(long id)
        {
            var appSMSTemplateViewModel = new SMSTemplateViewModel();

            try
            {
                SMSTemplate appSMSTemplate = _appSMSTemplateRepository.GetById(id);
                appSMSTemplateViewModel = appSMSTemplate.ConvertModelToViewModel<SMSTemplate, SMSTemplateViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appSMSTemplateViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(SMSTemplateViewModel appSMSTemplateViewModel)
        {
            int isSave = 0;
            try
            {
                if (appSMSTemplateViewModel != null)
                {
                    //add
                    if (appSMSTemplateViewModel.SMSTemplateId == default(int))
                    {
                        Create(appSMSTemplateViewModel);
                    }
                    else //edit
                    {
                        Update(appSMSTemplateViewModel);
                    }
                }
                else
                {
                    throw new ArgumentNullException("SMSTemplateViewModel", MessageResourceHelper.NullError);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(SMSTemplateViewModel appSMSTemplateViewModel)
        {
            int isSave = 0;
            try
            {
                if (appSMSTemplateViewModel != null)
                {
                    SMSTemplate appSMSTemplate = appSMSTemplateViewModel.ConvertViewModelToModel<SMSTemplateViewModel, SMSTemplate>();
                    _appSMSTemplateRepository.Insert(appSMSTemplate);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("SMSTemplateViewModel", MessageResourceHelper.NullError);
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

        public int Update(SMSTemplateViewModel appSMSTemplateViewModel)
        {
            int isSave = 0;
            try
            {
                if (appSMSTemplateViewModel != null)
                {
                    SMSTemplate appSMSTemplate = appSMSTemplateViewModel.ConvertViewModelToModel<SMSTemplateViewModel, SMSTemplate>();
                    _appSMSTemplateRepository.Update(appSMSTemplate);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("SMSTemplateViewModel", MessageResourceHelper.NullError);
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

        public int Delete(SMSTemplateViewModel appSMSTemplateViewModel)
        {
            int isSave = 0;
            try
            {
                if (appSMSTemplateViewModel != null)
                {
                    var viewModel = GetById(appSMSTemplateViewModel.SMSTemplateId);
                    SMSTemplate appSMSTemplate = viewModel.ConvertViewModelToModel<SMSTemplateViewModel, SMSTemplate>();
                    _appSMSTemplateRepository.Delete(appSMSTemplate);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("SMSTemplateViewModel", MessageResourceHelper.NullError);
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
                    SMSTemplate appSMSTemplate = appSMSTemplateViewModel.ConvertViewModelToModel<SMSTemplateViewModel, SMSTemplate>();
                    _appSMSTemplateRepository.Delete(appSMSTemplate);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("SMSTemplateViewModel", MessageResourceHelper.NullError);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<SMSTemplateViewModel> appSMSTemplateViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appSMSTemplateViewModel in appSMSTemplateViewModels)
                {
                    SMSTemplateViewModel viewModel = GetById(appSMSTemplateViewModel.SMSTemplateId);
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

    #region Interface : SMSTemplate

    public interface ISMSTemplateRepository : IGeneric<SMSTemplateViewModel>
    {
        int Delete(List<SMSTemplateViewModel> appSMSTemplateViewModels);
    }

    #endregion
}
