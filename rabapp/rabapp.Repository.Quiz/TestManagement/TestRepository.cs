using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using rabapp.Model.Quiz.QuestionManagement;
using rabapp.Model.Quiz.TestManagement;
using rabapp.Model.Quiz.ViewModels;
using rabapp.Repository.Common;

namespace rabapp.Repository.Quiz.TestManagement
{
    public class TestRepository : BaseRepository<TestViewModel>, ITestRepository
    {
        private readonly AppDbContext _appDbContext;
        public TestRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public IEnumerable<TestViewModel> Search(int userId, string keyword, int currentPage = 0, int take = 50)
        {
            int skip = Convert.ToInt32(currentPage * take);
            var query = @"SELECT [T].*
                           ,[TC].[TestCategoryName] 
                           ,ISNULL(COUNT(*) OVER(),0) AS TotalRecordCount
                            FROM [dbo].[Test] [T] 
                            INNER JOIN [dbo].[TestCategory] [TC] ON [TC].[TestCategoryId] = [T].[TestCategoryId]
                            WHERE 1=1 AND [T].[CreatedByUserId] = CASE WHEN @UserId=0 THEN  [T].[CreatedByUserId] ELSE @UserId END
                            AND (LOWER([T].[TestName]) LIKE  '%' + LOWER(@Keyword) + '%'
                            OR LOWER([T].[Tags]) LIKE  '%' + LOWER(@Keyword) + '%'
                            OR LOWER([T].[Price]) LIKE  '%' + LOWER(@Keyword) + '%'
                            OR LOWER([T].[Discount]) LIKE  '%' + LOWER(@Keyword) + '%'
                            OR LOWER([T].[Duration]) LIKE  '%' + LOWER(@Keyword) + '%'
                            OR LOWER([T].[NoOfQuestion]) LIKE  '%' + LOWER(@Keyword) + '%') 
                            ORDER BY [T].[TestId] DESC OFFSET Convert(INT,@Skip) ROWS FETCH NEXT Convert(INT,@Take) ROWS ONLY";

            return _appDbContext.SqlConnection.Query<TestViewModel>(query, new { UserId = userId, Keyword = keyword, Skip = skip, Take = take }).ToList();
        }

        public TestViewModel GetById(int testId)
        {
            var query = @"SELECT [T].*
                        ,[TC].[TestCategoryName] 
                         FROM [dbo].[Test] [T] 
                         INNER JOIN [dbo].[TestCategory] [TC] ON [TC].[TestCategoryId] = [T].[TestCategoryId] 
                         WHERE [T].[TestId] = @TestId";

            return _appDbContext.SqlConnection.Query<TestViewModel>(query, new { TestId = testId }).FirstOrDefault();

        }

        public IEnumerable<TestViewModel> GetTestIconForHomePage(int currentPage = 0, int take = 18)
        {
            int skip = Convert.ToInt32(currentPage * take);
            var query = @"SELECT [T].*
                            FROM [dbo].[Test] [T]  WHERE 1 = 1 AND [T].[IsPublished] = 1
                            ORDER BY [T].[TestId] DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            return _appDbContext.SqlConnection.Query<TestViewModel>(query, new { Skip = skip, Take = take }).ToList();
        }

        public IEnumerable<TestViewModel> GetTestByTestCategoryId(int testCategoryId, int currentPage = 0, int take = 15)
        {
            int skip = Convert.ToInt32(currentPage * take);

            if (testCategoryId > 0)
            {
                var query = @"SELECT [T].*, ISNULL(COUNT(*) OVER(),0) AS TotalRecordCount
                            FROM [dbo].[Test] [T] 
                            INNER JOIN [dbo].[TestCategory] [TC] ON [TC].[TestCategoryId] = [T].[TestCategoryId]
                            WHERE 1 = 1 AND [T].[IsPublished] = 1 AND [TC].[TestCategoryId] = @TestCategoryId
                            ORDER BY [T].[TestId] DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
                return _appDbContext.SqlConnection.Query<TestViewModel>(query, new { TestCategoryId = testCategoryId, Skip = skip, Take = take }).ToList();
            }
            else
            {
                var query = @"SELECT [T].*, ISNULL(COUNT(*) OVER(),0) AS TotalRecordCount
                            FROM [dbo].[Test] [T] WHERE [T].[IsPublished] = 1
                            ORDER BY [T].[TestId] DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
                return _appDbContext.SqlConnection.Query<TestViewModel>(query, new { Skip = skip, Take = take }).ToList();
            }

        }

    }

    public interface ITestRepository : IBaseRepository<TestViewModel>
    {
        IEnumerable<TestViewModel> Search(int userId, string keyword, int currentPage = 0, int take = 50);
        IEnumerable<TestViewModel> GetTestIconForHomePage(int currentPage = 0, int take = 50);
        IEnumerable<TestViewModel> GetTestByTestCategoryId(int testCategoryId, int currentPage = 0, int take = 15);
        TestViewModel GetById(int testId);
    }
}
