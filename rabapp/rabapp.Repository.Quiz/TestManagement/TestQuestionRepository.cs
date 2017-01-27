using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using rabapp.Model.Quiz.TestManagement;
using rabapp.Repository.Common;
using rabapp.Model.Quiz.ViewModels;

namespace rabapp.Repository.Quiz.TestManagement
{
    public class TestQuestionRepository : BaseRepository<TestQuestionViewModel>, ITestQuestionRepository
    {
        private readonly AppDbContext _appDbContext;
        public TestQuestionRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void DeleteByTestId(int testId)
        {
            var query = @"DELETE FROM [dbo].[TestQuestion] WHERE [TestId] = @TestId";

            var queryDelete = _appDbContext.SqlConnection.Execute(query, new { TestId = testId });

        }
    }

    public interface ITestQuestionRepository : IBaseRepository<TestQuestionViewModel>
    {
        void DeleteByTestId(int testId);
    }
}
