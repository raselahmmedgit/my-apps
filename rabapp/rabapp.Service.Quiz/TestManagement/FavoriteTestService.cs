using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using rabapp.Model.Quiz.ViewModels;
using rabapp.Repository.Common;
using rabapp.Repository.Quiz.TestManagement;
using rabapp.Service.Common;

namespace rabapp.Service.Quiz.TestManagement
{
    public class FavoriteTestService : BaseService<FavoriteTestViewModel>, IFavoriteTestService
    {
        private readonly IFavoriteTestRepository _iFavoriteTestRepository;
        private readonly AppDbContext _appDbContext;

        public FavoriteTestService(IBaseRepository<FavoriteTestViewModel> iBaseRepository
            , IFavoriteTestRepository iFavoriteTestRepository
            , AppDbContext appDbContext)
            : base(iBaseRepository, appDbContext)
        {
            _iFavoriteTestRepository = iFavoriteTestRepository;
            _appDbContext = appDbContext;
        }

        public Message AddToFavorite(FavoriteTestViewModel favoriteTestViewModel)
        {
            Message message;
            try
            {
                using (var scope = new TransactionScope())
                {
                    _appDbContext.SqlConnection.Open();
                    var affectedRow = 0;
                    var isExist = _iFavoriteTestRepository.GetByUserId(favoriteTestViewModel.TestId, favoriteTestViewModel.UserId);
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
                        affectedRow = _iFavoriteTestRepository.InsertWithoutIdentity(favoriteTestViewModel);
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
                _appDbContext.SqlConnection.Close();
            }
            return message;
        }

    }

    public interface IFavoriteTestService : IBaseService<FavoriteTestViewModel>
    {
        Message AddToFavorite(FavoriteTestViewModel favoriteTestViewModel);
    }
}
