using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using SoftwareGrid.Model.iTestApp.TestManagement;
using SoftwareGrid.Repository.iTestApp.Base;

namespace SoftwareGrid.Repository.iTestApp.TestManagement
{
    public class TestTakenDetailsRepository:BaseRepository<TestTakenDetail>,ITestTakenDetailsRepository
    {
        private readonly AppDbContext _dbContext;
        public TestTakenDetailsRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<TestTakenDetail> GetByTakenId(int takenId)
        {
            const string query = @"SELECT *FROM TestTakenDetails WHERE TakenId = @TakenId";
            return _dbContext.SqlConnection.Query<TestTakenDetail>(query, new { TakenId = takenId }).ToList();
        }
    }

    public interface ITestTakenDetailsRepository:IBaseRepository<TestTakenDetail>
    {
        IEnumerable<TestTakenDetail> GetByTakenId(int takenId);
    }
}
