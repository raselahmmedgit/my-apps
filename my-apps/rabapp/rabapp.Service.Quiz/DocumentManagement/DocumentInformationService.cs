using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rabapp.Repository.Common;
using rabapp.Repository.Quiz.DocumentManagement;
using rabapp.Service.Common;
using rabapp.ViewModel.Quiz.ViewModels;

namespace rabapp.Service.Quiz.DocumentManagement
{
    public class DocumentInformationService : BaseService<DocumentInformationViewModel>, IDocumentInformationService
    {
        private readonly IDocumentInformationRepository _iDocumentInformationRepository;
        private readonly AppDbContext _appDbContext;

        public DocumentInformationService(IBaseRepository<DocumentInformationViewModel> iBaseRepository, IDocumentInformationRepository iDocumentInformationRepository, AppDbContext appDbContext)
            : base(iBaseRepository, appDbContext)
        {
            _iDocumentInformationRepository = iDocumentInformationRepository;
            _appDbContext = appDbContext;
        }
    }

    public interface IDocumentInformationService : IBaseService<DocumentInformationViewModel>
    {
    }
}
