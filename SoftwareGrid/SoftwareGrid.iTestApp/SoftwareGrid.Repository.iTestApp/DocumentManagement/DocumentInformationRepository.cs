using SoftwareGrid.Model.iTestApp.DocumentManagement;
using SoftwareGrid.Repository.iTestApp.Base;

namespace SoftwareGrid.Repository.iTestApp.DocumentManagement
{
    public class DocumentInformationRepository : BaseRepository<DocumentInformation>, IDocumentInformationRepository
    {
        private readonly AppDbContext _dbContext;
        public DocumentInformationRepository(AppDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }

    public interface IDocumentInformationRepository : IBaseRepository<DocumentInformation>
    {
    }
}
