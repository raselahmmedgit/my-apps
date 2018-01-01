using SoftwareGrid.Model.iTestApp.SecurityManagement;
using SoftwareGrid.Model.iTestApp.TestManagement;
using SoftwareGrid.Repository.iTestApp.Base;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;

namespace SoftwareGrid.Repository.iTestApp.TestManagement
{
    public class TestRepository: BaseRepository<Test>, ITestRepository
    {
        private readonly DbContext _dbContext;
        public TestRepository(DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
        
        public IEnumerable<Test> Search(int userId, string keyword, int currentPage = 0, int take = 50)
        {
            int skip = Convert.ToInt32(currentPage * take);
            var query = @"SELECT 
                            [T].*
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

            return _dbContext.SqlConnection.Query<Test>(query, new { UserId = userId, Keyword = keyword, Skip = skip, Take = take }).ToList();
        }
        
        public Test GetById(int testId)
        {
            var query = @"SELECT 
                         [T].*
                        ,[TC].[TestCategoryName] 
                         FROM [dbo].[Test] [T] 
                         INNER JOIN [dbo].[TestCategory] [TC] ON [TC].[TestCategoryId] = [T].[TestCategoryId] 
                         WHERE [T].[TestId] = @TestId";

            return _dbContext.SqlConnection.Query<Test>(query, new { TestId = testId }).FirstOrDefault();

        }
       
        public IEnumerable<Test> GetTestIconForHomePage(int currentPage = 0, int take = 18)
        {
            int skip = Convert.ToInt32(currentPage * take);
            var query = @"SELECT 
                            [T].*
                            FROM [dbo].[Test] [T]  WHERE 1 = 1 AND T.IsPublished = 1
                            ORDER BY [T].[TestId] DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            return _dbContext.SqlConnection.Query<Test>(query, new { Skip = skip, Take = take }).ToList();
        }

        public IEnumerable<Test> GetTestByTestCategoryId(int testCategoryId, int currentPage = 0, int take = 15)
        {
            int skip = Convert.ToInt32(currentPage * take);

            
            if (testCategoryId > 0)
            {
                var query = @"SELECT 
                            [T].*, ISNULL(COUNT(*) OVER(),0) AS TotalRecordCount
                            FROM [dbo].[Test] [T] 
                            INNER JOIN [dbo].[TestCategory] [TC] ON [TC].[TestCategoryId] = [T].[TestCategoryId]
                            WHERE 1 = 1 AND T.IsPublished = 1
                            AND [TC].[TestCategoryId] = @TestCategoryId
                            ORDER BY [T].[TestId] DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
                return _dbContext.SqlConnection.Query<Test>(query, new { TestCategoryId = testCategoryId, Skip = skip, Take = take }).ToList();
            }
            else
            {
                var query = @"SELECT 
                            [T].*, ISNULL(COUNT(*) OVER(),0) AS TotalRecordCount
                            FROM [dbo].[Test] [T] WHERE T.IsPublished = 1
                            ORDER BY [T].[TestId] DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
                return _dbContext.SqlConnection.Query<Test>(query, new { Skip = skip, Take = take }).ToList();
            }
            
        }

        public IEnumerable<Test> GetTestWithUserByTestCategoryId(int userId,int testCategoryId, int currentPage = 0, int take = 15)
        {
            int skip = Convert.ToInt32(currentPage * take);


            if (testCategoryId > 0)
            {
                var query = @"SELECT 
                            [T].*, ISNULL(COUNT(*) OVER(),0) AS TotalRecordCount,ISNULL(FT.Sl,0) FavoriteId 
                            FROM [dbo].[Test] [T] 
                            INNER JOIN [dbo].[TestCategory] [TC] ON [TC].[TestCategoryId] = [T].[TestCategoryId]
                            LEFT JOIN FavoriteTest FT ON FT.TestId = T.TestId AND FT.UserId = @UserId 
                            WHERE 1 = 1 AND T.IsPublished = 1
                            AND [TC].[TestCategoryId] = @TestCategoryId
                            ORDER BY [T].[TestId] DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
                return _dbContext.SqlConnection.Query<Test>(query, new { UserId = userId, TestCategoryId = testCategoryId, Skip = skip, Take = take }).ToList();
            }
            else
            {
                var query = @"SELECT 
                            [T].*, ISNULL(COUNT(*) OVER(),0) AS TotalRecordCount,
                            ISNULL(FT.Sl,0) FavoriteId 
                            FROM [dbo].[Test] [T]
                            LEFT JOIN FavoriteTest FT ON FT.TestId = T.TestId AND FT.UserId = @UserId 
                            WHERE T.IsPublished = 1
                            ORDER BY [T].[TestId] DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
                return _dbContext.SqlConnection.Query<Test>(query, new {UserId=userId, Skip = skip, Take = take }).ToList();
            }

        }


        public IEnumerable<Test> GetTestTakenByUserId(int userId, int currentPage = 0, int take = 15)
        {
            int skip = Convert.ToInt32(currentPage * take);

            var query = @"SELECT T.*,
                        ISNULL(COUNT(*) OVER(),0) AS TotalRecordCount,
                        ISNULL(TT.Score,0)Score,
                        TT.TakenId
                        FROM TestTaken TT 
                        INNER JOIN Test T ON TT.TestId = T.TestId AND TT.UserId = @UserId
                        ORDER BY TT.TakenID DESC 
                        OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            return _dbContext.SqlConnection.Query<Test>(query, new { UserId = userId, Skip = skip, Take = take }).ToList();

        }

        public IEnumerable<Test> GetFavoriteTestByUserId(int userId, int currentPage = 0, int take = 15)
        {
            int skip = Convert.ToInt32(currentPage * take);

            var query = @"SELECT T.*,
                        ISNULL(COUNT(*) OVER(),0) AS TotalRecordCount,
                        FT.SL FavoriteId
                        FROM FavoriteTest FT 
                        INNER JOIN Test T ON FT.TestId = T.TestId AND FT.UserId = @UserId
                        ORDER BY FT.SL DESC 
                        OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            return _dbContext.SqlConnection.Query<Test>(query, new { UserId = userId, Skip = skip, Take = take }).ToList();

        }


    }

    public interface ITestRepository : IBaseRepository<Test>
    {
        IEnumerable<Test> Search(int userId, string keyword, int currentPage = 0, int take = 50);
        IEnumerable<Test> GetTestIconForHomePage(int currentPage = 0, int take = 50);
        IEnumerable<Test> GetTestByTestCategoryId(int testCategoryId, int currentPage = 0, int take = 15);
        IEnumerable<Test> GetTestTakenByUserId(int userId, int currentPage = 0, int take = 15);
        IEnumerable<Test> GetFavoriteTestByUserId(int userId, int currentPage = 0, int take = 15);

        IEnumerable<Test> GetTestWithUserByTestCategoryId(int userId, int testCategoryId, int currentPage = 0,
            int take = 15);
        Test GetById(int testId);
    }
}
