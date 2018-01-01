using System.Collections.Generic;
using SoftwareGrid.Model.iTestApp.TestManagement;
using SoftwareGrid.Repository.iTestApp.Base;
using Dapper;
using System.Linq;

namespace SoftwareGrid.Repository.iTestApp.TestManagement
{
    public class TestWiseQuestionRepository : BaseRepository<TestWiseQuestion>, ITestWiseQuestionRepository
    {
        private readonly DbContext _dbContext;
        public TestWiseQuestionRepository(DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteByTestId(int testId)
        {
            const string query = @"DELETE FROM [dbo].TestWiseQuestion WHERE TestId = @TestId";
            var queryDelete = _dbContext.SqlConnection.Execute(query, new { TestId = testId });
        }

        public IEnumerable<TestWiseQuestion> GetAllByTestId(int testId)
        {
            const string query = @"SELECT *FROM [dbo].TestWiseQuestion WHERE TestId = @TestId";
            return _dbContext.SqlConnection.Query<TestWiseQuestion>(query, new { TestId = testId }).ToList();
        }
    }

    public interface ITestWiseQuestionRepository : IBaseRepository<TestWiseQuestion>
    {
        void DeleteByTestId(int testId);
        IEnumerable<TestWiseQuestion> GetAllByTestId(int testId);
    }
}
