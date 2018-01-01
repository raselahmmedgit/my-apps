using System;
using System.Linq;
using Dapper;
using SoftwareGrid.Model.iTestApp.TestManagement;
using SoftwareGrid.Repository.iTestApp.Base;

namespace SoftwareGrid.Repository.iTestApp.TestManagement
{
    public class TestTakenRepository:BaseRepository<TestTaken>,ITestTakenRepository
    {
        private readonly ITestTakenDetailsRepository _iTestTakenDetailsRepository;
        private readonly ITestRepository _iTestRepository;
        private readonly DbContext _dbContext;
        public TestTakenRepository(
             ITestTakenDetailsRepository iTestTakenDetailsRepository
            ,ITestRepository iTestRepository
            ,DbContext dbContext
            ) : base(dbContext)
        {
            _iTestTakenDetailsRepository = iTestTakenDetailsRepository;
            _iTestRepository = iTestRepository;
            _dbContext = dbContext;
        }

        public TestTaken GetById(int takenId, bool withDetails = false, bool navigationproperty = false)
        {
            const string query = @"SELECT *FROM TestTaken WHERE TakenId = @TakenId";

            var data = _dbContext.SqlConnection.Query<TestTaken>(query, new { TakenId = takenId }).FirstOrDefault();

            if (withDetails)
            {
                if (data != null)
                {
                    data.TestTakenDetails = _iTestTakenDetailsRepository.GetByTakenId(takenId);
                }
            }
            if (navigationproperty)
            {
                if (data != null)
                {
                    data.Test = _iTestRepository.Get(new Test { TestId = data.TestId });
                }
            }

            return data;

        }

    }

    public interface ITestTakenRepository:IBaseRepository<TestTaken>
    {
        TestTaken GetById(int takenId, bool withDetails = false, bool navigationproperty = false);
    }
}
