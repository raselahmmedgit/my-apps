using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using SoftwareGrid.Model.iTestApp.TestManagement;
using SoftwareGrid.Repository.iTestApp.Base;

namespace SoftwareGrid.Repository.iTestApp.TestManagement
{
    public class TestTakenDetailsRepository:BaseRepository<TestTakenDetails>,ITestTakenDetailsRepository
    {
        private readonly DbContext _dbContext;
        public TestTakenDetailsRepository(DbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<TestTakenDetails> GetByTakenId(int takenId)
        {
            const string query = @"SELECT *FROM TestTakenDetails WHERE TakenId = @TakenId";
            return _dbContext.SqlConnection.Query<TestTakenDetails>(query, new { TakenId = takenId }).ToList();
        }
    }

    public interface ITestTakenDetailsRepository:IBaseRepository<TestTakenDetails>
    {
        IEnumerable<TestTakenDetails> GetByTakenId(int takenId);
    }
}
