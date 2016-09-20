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
    #region Interface Implement : DocumentInfo

    public class DocumentInfoRepository : IDocumentInfoRepository
    {
        #region Global Variable Declaration

        //private readonly Repository<DocumentInfo> _appDocumentInfoRepository;
        private readonly RepositoryBase<DocumentInfo> _appDocumentInfoRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        #endregion

        #region Constructor

        public DocumentInfoRepository(Repository<DocumentInfo> appDocumentInfoRepository, IUnitOfWork iUnitOfWork)
        {
            this._appDocumentInfoRepository = appDocumentInfoRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Get Method

        public IQueryable<DocumentInfoViewModel> GetAll()
        {
            var appDocumentInfoViewModels = new List<DocumentInfoViewModel>();
            try
            {

                List<DocumentInfo> appDocumentInfos = _appDocumentInfoRepository.GetAll();

                foreach (DocumentInfo appDocumentInfo in appDocumentInfos)
                {
                    var appDocumentInfoViewModel = appDocumentInfo.ConvertModelToViewModel<DocumentInfo, DocumentInfoViewModel>();
                    appDocumentInfoViewModels.Add(appDocumentInfoViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appDocumentInfoViewModels.AsQueryable();
        }

        public DocumentInfoViewModel GetById(long id)
        {
            var appDocumentInfoViewModel = new DocumentInfoViewModel();

            try
            {
                DocumentInfo appDocumentInfo = _appDocumentInfoRepository.GetById(id);
                appDocumentInfoViewModel = appDocumentInfo.ConvertModelToViewModel<DocumentInfo, DocumentInfoViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appDocumentInfoViewModel;
        }

        #endregion

        #region Create Method

        public int CreateOrUpdate(DocumentInfoViewModel appDocumentInfoViewModel)
        {
            int isSave = 0;
            try
            {
                if (appDocumentInfoViewModel != null)
                {
                    //add
                    if (appDocumentInfoViewModel.DocumentInfoId == default(int))
                    {
                        Create(appDocumentInfoViewModel);
                    }
                    else //edit
                    {
                        Update(appDocumentInfoViewModel);
                    }
                }
                else
                {
                    throw new ArgumentNullException("DocumentInfoViewModel", MessageResourceHelper.NullError);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSave;
        }
        public int Create(DocumentInfoViewModel appDocumentInfoViewModel)
        {
            int isSave = 0;
            try
            {
                if (appDocumentInfoViewModel != null)
                {
                    DocumentInfo appDocumentInfo = appDocumentInfoViewModel.ConvertViewModelToModel<DocumentInfoViewModel, DocumentInfo>();
                    _appDocumentInfoRepository.Insert(appDocumentInfo);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("DocumentInfoViewModel", MessageResourceHelper.NullError);
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

        public int Update(DocumentInfoViewModel appDocumentInfoViewModel)
        {
            int isSave = 0;
            try
            {
                var updateDocumentInfoViewModel = GetById(appDocumentInfoViewModel.DocumentInfoId);

                if (updateDocumentInfoViewModel != null)
                {
                    DocumentInfo appDocumentInfo = appDocumentInfoViewModel.ConvertViewModelToModel<DocumentInfoViewModel, DocumentInfo>();
                    _appDocumentInfoRepository.Update(appDocumentInfo);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("DocumentInfoViewModel", MessageResourceHelper.NullError);
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

        public int Delete(DocumentInfoViewModel appDocumentInfoViewModel)
        {
            int isSave = 0;
            try
            {
                var deleteDocumentInfoViewModel = GetById(appDocumentInfoViewModel.DocumentInfoId);

                if (deleteDocumentInfoViewModel != null)
                {
                    var viewModel = GetById(appDocumentInfoViewModel.DocumentInfoId);
                    DocumentInfo appDocumentInfo = viewModel.ConvertViewModelToModel<DocumentInfoViewModel, DocumentInfo>();
                    _appDocumentInfoRepository.Delete(appDocumentInfo);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("DocumentInfoViewModel", MessageResourceHelper.NullError);
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
                var appDocumentInfoViewModel = GetById(id);
                if (appDocumentInfoViewModel != null)
                {
                    DocumentInfo appDocumentInfo = appDocumentInfoViewModel.ConvertViewModelToModel<DocumentInfoViewModel, DocumentInfo>();
                    _appDocumentInfoRepository.Delete(appDocumentInfo);
                    isSave = Save();
                }
                else
                {
                    throw new ArgumentNullException("DocumentInfoViewModel", MessageResourceHelper.NullError);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public int Delete(List<DocumentInfoViewModel> appDocumentInfoViewModels)
        {
            int isSave = 0;
            try
            {
                foreach (var appDocumentInfoViewModel in appDocumentInfoViewModels)
                {
                    DocumentInfoViewModel viewModel = GetById(appDocumentInfoViewModel.DocumentInfoId);
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

    #region Interface : DocumentInfo

    public interface IDocumentInfoRepository : IGeneric<DocumentInfoViewModel>
    {
        int Delete(List<DocumentInfoViewModel> appDocumentInfoViewModels);
    }

    #endregion
}
