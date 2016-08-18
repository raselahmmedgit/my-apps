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
    #region Interface Implement : EmailTemplateCategory

    public class EmailTemplateCategoryRepository : IEmailTemplateCategoryRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<EmailTemplateCategory> _appEmailTemplateCategoryRepository;
        private readonly RepositoryBase<EmailTemplateCategory> _appEmailTemplateCategoryRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public EmailTemplateCategoryRepository(Repository<EmailTemplateCategory> appEmailTemplateCategoryRepository, IUnitOfWork iUnitOfWork)
        {
            this._appEmailTemplateCategoryRepository = appEmailTemplateCategoryRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<EmailTemplateCategoryViewModel> GetAll()
        {
            var appEmailTemplateCategoryViewModels = new List<EmailTemplateCategoryViewModel>();
            try
            {

                List<EmailTemplateCategory> appEmailTemplateCategorys = _appEmailTemplateCategoryRepository.GetAll();

                foreach (EmailTemplateCategory appEmailTemplateCategory in appEmailTemplateCategorys)
                {
                    var appEmailTemplateCategoryViewModel = appEmailTemplateCategory.ConvertModelToViewModel<EmailTemplateCategory, EmailTemplateCategoryViewModel>();
                    appEmailTemplateCategoryViewModels.Add(appEmailTemplateCategoryViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appEmailTemplateCategoryViewModels.AsQueryable();
        }

        public EmailTemplateCategoryViewModel GetById(long id)
        {
            var appEmailTemplateCategoryViewModel = new EmailTemplateCategoryViewModel();

            try
            {
                EmailTemplateCategory appEmailTemplateCategory = _appEmailTemplateCategoryRepository.GetById(id);
                appEmailTemplateCategoryViewModel = appEmailTemplateCategory.ConvertModelToViewModel<EmailTemplateCategory, EmailTemplateCategoryViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appEmailTemplateCategoryViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(EmailTemplateCategoryViewModel appEmailTemplateCategoryViewModel)
        {
            int isSave = 0;
            try
            {
                if (appEmailTemplateCategoryViewModel != null)
                {
                    //add
                    if (appEmailTemplateCategoryViewModel.EmailTemplateCategoryId == default(int))
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
                    throw new ArgumentNullException("EmailTemplateCategoryViewModel", MessageResourceHelper.NullError);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(EmailTemplateCategoryViewModel appEmailTemplateCategoryViewModel)
        {
            int isSave = 0;
            try
            {
                if (appEmailTemplateCategoryViewModel != null)
                {
                    EmailTemplateCategory appEmailTemplateCategory = appEmailTemplateCategoryViewModel.ConvertViewModelToModel<EmailTemplateCategoryViewModel, EmailTemplateCategory>();
                    _appEmailTemplateCategoryRepository.Insert(appEmailTemplateCategory);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("EmailTemplateCategoryViewModel", MessageResourceHelper.NullError);
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

        public int Update(EmailTemplateCategoryViewModel appEmailTemplateCategoryViewModel)
        {
            int isSave = 0;
            try
            {
                if (appEmailTemplateCategoryViewModel != null)
                {
                    EmailTemplateCategory appEmailTemplateCategory = appEmailTemplateCategoryViewModel.ConvertViewModelToModel<EmailTemplateCategoryViewModel, EmailTemplateCategory>();
                    _appEmailTemplateCategoryRepository.Update(appEmailTemplateCategory);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("EmailTemplateCategoryViewModel", MessageResourceHelper.NullError);
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

        public int Delete(EmailTemplateCategoryViewModel appEmailTemplateCategoryViewModel)
        {
            int isSave = 0;
            try
            {
                if (appEmailTemplateCategoryViewModel != null)
                {
                    var viewModel = GetById(appEmailTemplateCategoryViewModel.EmailTemplateCategoryId);
                    EmailTemplateCategory appEmailTemplateCategory = viewModel.ConvertViewModelToModel<EmailTemplateCategoryViewModel, EmailTemplateCategory>();
                    _appEmailTemplateCategoryRepository.Delete(appEmailTemplateCategory);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("EmailTemplateCategoryViewModel", MessageResourceHelper.NullError);
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
                    EmailTemplateCategory appEmailTemplateCategory = appEmailTemplateCategoryViewModel.ConvertViewModelToModel<EmailTemplateCategoryViewModel, EmailTemplateCategory>();
                    _appEmailTemplateCategoryRepository.Delete(appEmailTemplateCategory);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("EmailTemplateCategoryViewModel", MessageResourceHelper.NullError);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<EmailTemplateCategoryViewModel> appEmailTemplateCategoryViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appEmailTemplateCategoryViewModel in appEmailTemplateCategoryViewModels)
                {
                    EmailTemplateCategoryViewModel viewModel = GetById(appEmailTemplateCategoryViewModel.EmailTemplateCategoryId);
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

    #region Interface : EmailTemplateCategory

    public interface IEmailTemplateCategoryRepository : IGeneric<EmailTemplateCategoryViewModel>
    {
        int Delete(List<EmailTemplateCategoryViewModel> appEmailTemplateCategoryViewModels);
    }

    #endregion
}
