using SoftwareGrid.Model.iTestApp.DocumentManagement;
using SoftwareGrid.Repository.iTestApp.Base;
using SoftwareGrid.Repository.iTestApp.DocumentManagement;
using SoftwareGrid.Service.iTestApp.Base;

namespace SoftwareGrid.Service.iTestApp.DocumentManagement
{
    public class DocumentInformationService : BaseService<DocumentInformation>, IDocumentInformationService
    {
        private readonly IDocumentInformationRepository _iDocumentInformationRepository;
        private readonly DbContext _dbContext;

        public DocumentInformationService(IBaseRepository<DocumentInformation> iBaseRepository, IDocumentInformationRepository iDocumentInformationRepository, DbContext dbContext)
            : base(iBaseRepository, dbContext)
        {
            _iDocumentInformationRepository = iDocumentInformationRepository;
            _dbContext = dbContext;
        }
    }

    public interface IDocumentInformationService : IBaseService<DocumentInformation>
    {
    }
}
