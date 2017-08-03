using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using rabapp.Model.Quiz.QuestionManagement;
using rabapp.Model.Quiz.TestManagement;
using rabapp.Repository.Common;
using rabapp.ViewModel.Quiz.ViewModels;
using rabapp.ViewModel.Quiz.TestManagement;

namespace rabapp.Repository.Quiz.TestManagement
{
    public class TestTakenRepository : BaseRepository<TestTakenViewModel>, ITestTakenRepository
    {
        private readonly ITestTakenDetailsRepository _iTestTakenDetailsRepository;
        private readonly ITestRepository _iTestRepository;
        private readonly AppDbContext _appDbContext;
        public TestTakenRepository(
             ITestTakenDetailsRepository iTestTakenDetailsRepository
            , ITestRepository iTestRepository
            , AppDbContext dbContext
            )
            : base(dbContext)
        {
            _iTestTakenDetailsRepository = iTestTakenDetailsRepository;
            _iTestRepository = iTestRepository;
            _appDbContext = dbContext;
        }

        public TestTakenViewModel GetById(int testTakenId, bool isWithTestTakenDetails = false, bool navigationproperty = false)
        {
            const string query = @"SELECT * FROM [dbo].[TestTaken] WHERE [TestTakenId] = @TestTakenId";

            var data = _appDbContext.SqlConnection.Query<TestTakenViewModel>(query, new { TakenId = testTakenId }).FirstOrDefault();

            if (isWithTestTakenDetails)
            {
                if (data != null)
                {
                    data.TestTakenDetailsViewModels = _iTestTakenDetailsRepository.GetByTestTakenId(testTakenId);
                }
            }
            if (navigationproperty)
            {
                if (data != null)
                {
                    data.TestViewModel = _iTestRepository.Get(new TestViewModel { TestId = data.TestId });
                }
            }

            return data;

        }

    }

    public interface ITestTakenRepository : IBaseRepository<TestTakenViewModel>
    {
        TestTakenViewModel GetById(int testTakenId, bool isWithTestTakenDetails = false, bool navigationproperty = false);
    }
}
