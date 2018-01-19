using System;
using SoftwareGrid.Model.iTestApp.ViewModels;
using SoftwareGrid.Repository.iTestApp.Base;
using SoftwareGrid.Repository.iTestApp.Utility;

namespace SoftwareGrid.Service.iTestApp.Utility
{
    public class UtilityService : IUtilityService
    {
        private readonly UtilityRepository _iUtilityRepository;
        private readonly AppDbContext _dbContext;

        public UtilityService(UtilityRepository iUtilityRepository, AppDbContext dbContext)
        {
            _iUtilityRepository = iUtilityRepository;
            _dbContext = dbContext;
        }

        public DashboardSummaryViewModel GetDashboardSummaryCount(int userId)
        {
            try
            {
                _dbContext.SqlConnection.Open();

                return _iUtilityRepository.GetDashboardSummaryCount(userId);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                _dbContext.SqlConnection.Close();
            }
        }

        public DateTime GetServerDate()
        {
            try
            {
                _dbContext.SqlConnection.Open();

                return _iUtilityRepository.GetServerDate();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                _dbContext.SqlConnection.Close();
            }
        }
    }
    public interface IUtilityService 
    {
        DashboardSummaryViewModel GetDashboardSummaryCount(int userId);
        DateTime GetServerDate();
    }
}
