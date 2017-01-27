using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rabapp.Model.Quiz.TestManagement;
using rabapp.Model.Quiz.ViewModels;
using rabapp.Repository.Common;

namespace rabapp.Repository.Quiz.TestManagement
{
    public class TestCategoryRepository : BaseRepository<TestCategoryViewModel>, ITestCategoryRepository
    {
        private readonly AppDbContext _appDbContext;
        public TestCategoryRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
        }
    }

    public interface ITestCategoryRepository : IBaseRepository<TestCategoryViewModel>
    {
    }
}
