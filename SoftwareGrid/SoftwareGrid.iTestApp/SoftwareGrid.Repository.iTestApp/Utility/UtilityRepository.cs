using System;
using System.Linq;
using Dapper;
using SoftwareGrid.Model.iTestApp.ViewModels;
using SoftwareGrid.Repository.iTestApp.Base;

namespace SoftwareGrid.Repository.iTestApp.Utility
{
    public class UtilityRepository : IUtilityRepository
    {
        private readonly AppDbContext _dbContext;
        public UtilityRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public DashboardSummaryViewModel GetDashboardSummaryCount(int userId)
        {
            const string query = @"DECLARE @UserType INT
                        DECLARE @TotalTestCount INT
                        DECLARE @TotalQuestionCount INT
                        DECLARE @TotalTestTaken INT
                        SELECT @UserType = UserType FROM [User] WHERE UserId = @UserId
                        SELECT @TotalTestCount = ISNULL(COUNT(*),0) FROM Test WHERE CreatedByUserId = CASE WHEN @UserType = 1 THEN CreatedByUserId ELSE @UserId END 
                        SELECT @TotalQuestionCount = ISNULL(COUNT(*),0) FROM Question WHERE CreatedByUserId = CASE WHEN @UserType = 1 THEN CreatedByUserId ELSE @UserId END 
                        SELECT @TotalTestTaken = ISNULL(COUNT(*),0) FROM TestTaken
                        SELECT @TotalTestCount TestCount,@TotalQuestionCount QuestionCount, @TotalTestTaken TestTaken";
            return _dbContext.SqlConnection.Query<DashboardSummaryViewModel>(query, new { UserId = userId }).FirstOrDefault();
        }

        public DateTime GetServerDate()
        {
            return _dbContext.SqlConnection.Query<DateTime>("SELECT GETUTCDATE()").Single();
        }

    }
    public interface IUtilityRepository
    {
        DashboardSummaryViewModel GetDashboardSummaryCount(int userId);
        DateTime GetServerDate();
    }
}
