using System.Linq;
using Dapper;
using SoftwareGrid.Model.iTestApp.TestManagement;
using SoftwareGrid.Repository.iTestApp.Base;

namespace SoftwareGrid.Repository.iTestApp.TestManagement
{
    public class FavoriteTestRepository: BaseRepository<FavoriteTest>, IFavoriteTestRepository
    {
        private readonly AppDbContext _dbContext;
        
        public FavoriteTestRepository(AppDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;

        }

        public FavoriteTest GetByUserId(int testId, int userId )
        {
            const string query = @"SELECT *FROM FavoriteTest WHERE TestId = @TestId AND UserId=@UserId";

            var data = _dbContext.SqlConnection.Query<FavoriteTest>(query, new { TestId = testId, UserId = userId }).FirstOrDefault();
            return data;

        }

    }

    public interface IFavoriteTestRepository : IBaseRepository<FavoriteTest>
    {
        FavoriteTest GetByUserId(int testId, int userId);
    }
}
