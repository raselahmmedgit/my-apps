using SoftwareGrid.Model.iTestApp.TestManagement;
using SoftwareGrid.Repository.iTestApp.Base;

namespace SoftwareGrid.Repository.iTestApp.TestManagement
{
    public class TestCategoryRepository: BaseRepository<TestCategory>, ITestCategoryRepository
    {
        private readonly AppDbContext _dbContext;
        public TestCategoryRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }
    }

    public interface ITestCategoryRepository : IBaseRepository<TestCategory>
    {
    }
}
