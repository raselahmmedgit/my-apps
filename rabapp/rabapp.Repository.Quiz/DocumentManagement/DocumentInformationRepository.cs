using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rabapp.Model.Quiz.DocumentManagement;
using rabapp.Repository.Common;
using rabapp.ViewModel.Quiz.ViewModels;

namespace rabapp.Repository.Quiz.DocumentManagement
{
    public class DocumentInformationRepository : BaseRepository<DocumentInformationViewModel>, IDocumentInformationRepository
    {
        private readonly AppDbContext _appDbContext;
        public DocumentInformationRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

    }

    public interface IDocumentInformationRepository : IBaseRepository<DocumentInformationViewModel>
    {
    }
}
