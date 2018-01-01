using SoftwareGrid.Model.iTestApp.TestManagement;
using SoftwareGrid.Repository.iTestApp.Base;

namespace SoftwareGrid.Repository.iTestApp.TestManagement
{
    public class TestCategoryRepository: BaseRepository<TestCategory>, ITestCategoryRepository
    {
        private readonly DbContext _dbContext;
        public TestCategoryRepository(DbContext dbContext)
            : base(dbContext)
        {
        }
    }

    public interface ITestCategoryRepository : IBaseRepository<TestCategory>
    {
    }
}
