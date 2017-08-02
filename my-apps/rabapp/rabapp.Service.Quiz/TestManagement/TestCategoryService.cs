using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rabapp.Model.Quiz.TestManagement;
using rabapp.Repository.Quiz.TestManagement;
using rabapp.Service.Common;
using rabapp.Repository.Common;
using rabapp.ViewModel.Quiz.ViewModels;

namespace rabapp.Service.Quiz.TestManagement
{
    public class TestCategoryService : BaseService<TestCategoryViewModel>, ITestCategoryService
    {
        private readonly ITestCategoryRepository _iTestCategoryRepository;
        private readonly AppDbContext _appDbContext;

        public TestCategoryService(IBaseRepository<TestCategoryViewModel> iBaseRepository, ITestCategoryRepository iTestCategoryRepository, AppDbContext appDbContext)
            : base(iBaseRepository, appDbContext)
        {
            _iTestCategoryRepository = iTestCategoryRepository;
            _appDbContext = appDbContext;
        }
    }

    public interface ITestCategoryService : IBaseService<TestCategoryViewModel>
    {
    }
}
