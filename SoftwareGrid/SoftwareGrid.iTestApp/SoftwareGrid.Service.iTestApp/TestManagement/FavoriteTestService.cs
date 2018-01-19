using System;
using System.Transactions;
using SoftwareGrid.Model.iTestApp.TestManagement;
using SoftwareGrid.Model.iTestApp.Utility;
using SoftwareGrid.Repository.iTestApp.Base;
using SoftwareGrid.Repository.iTestApp.TestManagement;
using SoftwareGrid.Service.iTestApp.Base;

namespace SoftwareGrid.Service.iTestApp.TestManagement
{
    public class FavoriteTestService: BaseService<FavoriteTest>, IFavoriteTestService
    {
        private readonly IFavoriteTestRepository _iFavoriteTestRepository;
        private readonly AppDbContext _dbContext;

        public FavoriteTestService(IBaseRepository<FavoriteTest> iBaseRepository, IFavoriteTestRepository iFavoriteTestRepository, AppDbContext dbContext)
            : base(iBaseRepository, dbContext)
        {
            _iFavoriteTestRepository = iFavoriteTestRepository;
            _dbContext = dbContext;
        }

        public Message AddToFavorite(FavoriteTest favoriteTest)
        {
            Message message;
            try
            {
                using (var scope = new TransactionScope())
                {
                    _dbContext.SqlConnection.Open();
                    var affectedRow = 0;
                    var isExist = _iFavoriteTestRepository.GetByUserId(favoriteTest.TestId, favoriteTest.UserId);
                    if (isExist != null)
                    {
                        affectedRow = _iFavoriteTestRepository.Delete(isExist);
                        message = affectedRow > 0
                       ? SetMessage.SetSuccessMessage("Favorite Test remove successfully.")
                       : SetMessage.SetInformationMessage("No data has been deleted.");
                        message.State = 1;// To trace that favorite test is deleted.
                    }
                    else
                    {
                        affectedRow = _iFavoriteTestRepository.InsertWithoutIdentity(favoriteTest);
                        message = affectedRow > 0
                       ? SetMessage.SetSuccessMessage("Test has been favorite successfully.")
                       : SetMessage.SetInformationMessage("No data has been saved.");
                    }
                    scope.Complete();
                }
            }
            catch (Exception exception)
            {
                return SetMessage.SetErrorMessage(exception.Message);
            }
            finally
            {
                _dbContext.SqlConnection.Close();
            }
            return message;
        }


    }

    public interface IFavoriteTestService : IBaseService<FavoriteTest>
    {
        Message AddToFavorite(FavoriteTest favoriteTest);
    }
}
