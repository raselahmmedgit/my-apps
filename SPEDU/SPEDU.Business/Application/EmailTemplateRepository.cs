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
    #region Interface Implement : EmailTemplate

    public class EmailTemplateRepository : IEmailTemplateRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<EmailTemplate> _appEmailTemplateRepository;
        private readonly RepositoryBase<EmailTemplate> _appEmailTemplateRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public EmailTemplateRepository(Repository<EmailTemplate> appEmailTemplateRepository, IUnitOfWork iUnitOfWork)
        {
            this._appEmailTemplateRepository = appEmailTemplateRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<EmailTemplateViewModel> GetAll()
        {
            var appEmailTemplateViewModels = new List<EmailTemplateViewModel>();
            try
            {

                List<EmailTemplate> appEmailTemplates = _appEmailTemplateRepository.GetAll();

                foreach (EmailTemplate appEmailTemplate in appEmailTemplates)
                {
                    var appEmailTemplateViewModel = appEmailTemplate.ConvertModelToViewModel<EmailTemplate, EmailTemplateViewModel>();
                    appEmailTemplateViewModels.Add(appEmailTemplateViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appEmailTemplateViewModels.AsQueryable();
        }

        public EmailTemplateViewModel GetById(long id)
        {
            var appEmailTemplateViewModel = new EmailTemplateViewModel();

            try
            {
                EmailTemplate appEmailTemplate = _appEmailTemplateRepository.GetById(id);
                appEmailTemplateViewModel = appEmailTemplate.ConvertModelToViewModel<EmailTemplate, EmailTemplateViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appEmailTemplateViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(EmailTemplateViewModel appEmailTemplateViewModel)
        {
            int isSave = 0;
            try
            {
                if (appEmailTemplateViewModel != null)
                {
                    //add
                    if (appEmailTemplateViewModel.EmailTemplateId == default(int))
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
                    throw new ArgumentNullException("EmailTemplateViewModel", "Request data is null.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(EmailTemplateViewModel appEmailTemplateViewModel)
        {
            int isSave = 0;
            try
            {
                if (appEmailTemplateViewModel != null)
                {
                    EmailTemplate appEmailTemplate = appEmailTemplateViewModel.ConvertViewModelToModel<EmailTemplateViewModel, EmailTemplate>();
                    _appEmailTemplateRepository.Insert(appEmailTemplate);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("EmailTemplateViewModel", "Request data is null.");
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

        public int Update(EmailTemplateViewModel appEmailTemplateViewModel)
        {
            int isSave = 0;
            try
            {
                if (appEmailTemplateViewModel != null)
                {
                    EmailTemplate appEmailTemplate = appEmailTemplateViewModel.ConvertViewModelToModel<EmailTemplateViewModel, EmailTemplate>();
                    _appEmailTemplateRepository.Update(appEmailTemplate);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("EmailTemplateViewModel", "Request data is null.");
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

        public int Delete(EmailTemplateViewModel appEmailTemplateViewModel)
        {
            int isSave = 0;
            try
            {
                if (appEmailTemplateViewModel != null)
                {
                    var viewModel = GetById(appEmailTemplateViewModel.EmailTemplateId);
                    EmailTemplate appEmailTemplate = viewModel.ConvertViewModelToModel<EmailTemplateViewModel, EmailTemplate>();
                    _appEmailTemplateRepository.Delete(appEmailTemplate);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("EmailTemplateViewModel", "Request data is null.");
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
                    EmailTemplate appEmailTemplate = appEmailTemplateViewModel.ConvertViewModelToModel<EmailTemplateViewModel, EmailTemplate>();
                    _appEmailTemplateRepository.Delete(appEmailTemplate);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("EmailTemplateViewModel", "Request data is null.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<EmailTemplateViewModel> appEmailTemplateViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appEmailTemplateViewModel in appEmailTemplateViewModels)
                {
                    EmailTemplateViewModel viewModel = GetById(appEmailTemplateViewModel.EmailTemplateId);
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

    #region Interface : EmailTemplate

    public interface IEmailTemplateRepository : IGeneric<EmailTemplateViewModel>
    {
        int Delete(List<EmailTemplateViewModel> appEmailTemplateViewModels);
    }

    #endregion
}
