using System.Linq;
using Dapper;
using SoftwareGrid.Model.iTestApp.SecurityManagement;
using SoftwareGrid.Repository.iTestApp.Base;
using System.Collections.Generic;
using System;

namespace SoftwareGrid.Repository.iTestApp.UserManagement
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly DbContext _dbContext;
        public UserRepository(DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<User> Search(string keyword, int currentPage = 0, int take = 50)
        {
            int skip = Convert.ToInt32(currentPage * take);
            var query = @"SELECT [U].*, ISNULL(COUNT(*) OVER(),0) AS TotalRecordCount 
                            FROM [dbo].[User] [U] 
                            WHERE 1 = 1
							AND (LOWER([U].[FirstName]) LIKE  '%' + LOWER(@Keyword) + '%'
                            OR LOWER([U].[LastName]) LIKE  '%' + LOWER(@Keyword) + '%'
                            OR LOWER([U].[Email]) LIKE  '%' + LOWER(@Keyword) + '%'
                            OR LOWER([U].[MobileNo]) LIKE  '%' + LOWER(@Keyword) + '%'
                            OR LOWER([U].[UserType]) LIKE  '%' + LOWER(@Keyword) + '%'
                            OR LOWER([U].[IsActive]) LIKE  '%' + LOWER(@Keyword) + '%')
                            ORDER BY [U].[UserId] 
                            DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
            return _dbContext.SqlConnection.Query<User>(query, new { Keyword = keyword, Skip = skip, Take = take }).ToList();
        }


        public dynamic DynamicSearch(string keyword, int currentPage = 0, int take = 50)
        {
            int skip = Convert.ToInt32(currentPage * take);
            const string query = @"SELECT [U].*, ISNULL(COUNT(*) OVER(),0) AS TotalRecordCount 
                            FROM [dbo].[User] [U] 
                            WHERE 1 = 1
							AND (LOWER([U].[FirstName]) LIKE  '%' + LOWER(@Keyword) + '%'
                            OR LOWER([U].[LastName]) LIKE  '%' + LOWER(@Keyword) + '%'
                            OR LOWER([U].[Email]) LIKE  '%' + LOWER(@Keyword) + '%'
                            OR LOWER([U].[MobileNo]) LIKE  '%' + LOWER(@Keyword) + '%'
                            OR LOWER([U].[UserType]) LIKE  '%' + LOWER(@Keyword) + '%'
                            OR LOWER([U].[IsActive]) LIKE  '%' + LOWER(@Keyword) + '%')
                            ORDER BY [U].[UserId] 
                            DESC OFFSET Convert(INT,@Skip) ROWS FETCH NEXT Convert(INT,@Take) ROWS ONLY";

           var paramName = new [] {"Keyword", "Skip", "Take"};
           var paramValue = new [] { keyword, Convert.ToString(skip),  Convert.ToString(take) };

           return new ExecuteQuery(_dbContext).Execute(query, paramName, paramValue);
        }


        public User GetUser(string userName)
        {
            return _dbContext.SqlConnection.Query<User>("SELECT *FROM dbo.[User] Where Email =@Email", new { Email = userName }).FirstOrDefault();

        }

        public User GetUser(string userName, string password)
        {
            return _dbContext.SqlConnection.Query<User>("SELECT *FROM dbo.[User] Where Email =@Email AND Password=@Password ", new User { Email = userName, Password = password }).FirstOrDefault();
        }
    }

    public interface IUserRepository : IBaseRepository<User>
    {
        IEnumerable<User> Search(string keyword, int currentPage = 0, int take = 50);
        dynamic DynamicSearch(string keyword, int currentPage = 0, int take = 50);
        User GetUser(string userName);
        User GetUser(string userName, string password);
    }
}
