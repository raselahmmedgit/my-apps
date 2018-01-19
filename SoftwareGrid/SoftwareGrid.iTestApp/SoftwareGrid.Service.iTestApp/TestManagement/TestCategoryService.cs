using SoftwareGrid.Model.iTestApp.TestManagement;
using SoftwareGrid.Repository.iTestApp.Base;
using SoftwareGrid.Repository.iTestApp.TestManagement;
using SoftwareGrid.Service.iTestApp.Base;

namespace SoftwareGrid.Service.iTestApp.TestManagement
{
    public class TestCategoryService : BaseService<TestCategory>, ITestCategoryService
    {
        private readonly ITestCategoryRepository _iTestCategoryRepository;
        private readonly AppDbContext _dbContext;

        public TestCategoryService(IBaseRepository<TestCategory> iBaseRepository, ITestCategoryRepository iTestCategoryRepository, AppDbContext dbContext)
            : base(iBaseRepository, dbContext)
        {
            _iTestCategoryRepository = iTestCategoryRepository;
            _dbContext = dbContext;
        }
    }

    public interface ITestCategoryService : IBaseService<TestCategory>
    {
    }
}
