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

namespace rabapp.Repository.Quiz.TestManagement
{
    public class TestTakenDetailsRepository : BaseRepository<TestTakenDetailsViewModel>, ITestTakenDetailsRepository
    {
        private readonly AppDbContext _appDbContext;
        public TestTakenDetailsRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<TestTakenDetailsViewModel> GetByTestTakenId(int testTakenId)
        {
            const string query = @"SELECT * FROM [dbo].[TestTakenDetails] WHERE [TestTakenId] = @TestTakenId";
            return _appDbContext.SqlConnection.Query<TestTakenDetailsViewModel>(query, new { TestTakenId = testTakenId }).ToList();
        }
    }

    public interface ITestTakenDetailsRepository : IBaseRepository<TestTakenDetailsViewModel>
    {
        IEnumerable<TestTakenDetailsViewModel> GetByTestTakenId(int testTakenId);
    }
}
