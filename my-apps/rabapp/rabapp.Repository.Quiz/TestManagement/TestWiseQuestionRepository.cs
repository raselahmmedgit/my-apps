using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using rabapp.Repository.Common;
using rabapp.ViewModel.Quiz.ViewModels;

namespace rabapp.Repository.Quiz.TestManagement
{
    public class TestWiseQuestionRepository : BaseRepository<TestWiseQuestionViewModel>, ITestWiseQuestionRepository
    {
        private readonly AppDbContext _appDbContext;
        public TestWiseQuestionRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void DeleteByTestId(int testId)
        {
            const string query = @"DELETE FROM [dbo].TestWiseQuestion WHERE TestId = @TestId";
            var queryDelete = _appDbContext.SqlConnection.Execute(query, new { TestId = testId });
        }

        public IEnumerable<TestWiseQuestionViewModel> GetAllByTestId(int testId)
        {
            const string query = @"SELECT *FROM [dbo].TestWiseQuestion WHERE TestId = @TestId";
            return _appDbContext.SqlConnection.Query<TestWiseQuestionViewModel>(query, new { TestId = testId }).ToList();
        }
    }

    public interface ITestWiseQuestionRepository : IBaseRepository<TestWiseQuestionViewModel>
    {
        void DeleteByTestId(int testId);
        IEnumerable<TestWiseQuestionViewModel> GetAllByTestId(int testId);
    }
}
