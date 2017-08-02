using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using rabapp.Model.Quiz.TestManagement;
using rabapp.Repository.Common;
using rabapp.ViewModel.Quiz.ViewModels;

namespace rabapp.Repository.Quiz.TestManagement
{
    public class FavoriteTestRepository : BaseRepository<FavoriteTestViewModel>, IFavoriteTestRepository
    {
        private readonly AppDbContext _appDbContext;

        public FavoriteTestRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
            _appDbContext = appDbContext;

        }

        public FavoriteTestViewModel GetByUserId(int testId, int userId)
        {
            const string query = @"SELECT *FROM FavoriteTest WHERE TestId = @TestId AND UserId=@UserId";
            var favoriteTestViewModel = _appDbContext.SqlConnection.Query<FavoriteTestViewModel>(query, new { TestId = testId, UserId = userId }).FirstOrDefault();
            return favoriteTestViewModel;

        }

    }

    public interface IFavoriteTestRepository : IBaseRepository<FavoriteTestViewModel>
    {
        FavoriteTestViewModel GetByUserId(int testId, int userId);
    }
}
