using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using rabapp.Repository.Common;

namespace rabapp.Service.Common
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        #region Private Variable

        private readonly IBaseRepository<T> _iBaseRepository;
        private readonly AppDbContext _appDbContext;

        #endregion

        #region Constructor
        public BaseService(IBaseRepository<T> iBaseRepository, AppDbContext appDbContext)
        {
            _iBaseRepository = iBaseRepository;
            _appDbContext = appDbContext;
        }
        #endregion

        #region Public Virtual Method
        public virtual Message InsertOrUpdate(T entity)
        {
            Message message;
            try
            {
                _appDbContext.SqlConnection.Open();
                var isExist = _iBaseRepository.Get(entity);
                var affectedRow = 0;
                if (isExist == null)
                {
                    affectedRow = _iBaseRepository.Insert(entity);
                    message = affectedRow > 0
                        ? SetMessage.SetSuccessMessage("Information has been saved successfully.")
                        : SetMessage.SetInformationMessage("No data has been saved.");
                }
                else
                {
                    affectedRow = _iBaseRepository.Update(entity);
                    message = affectedRow > 0
                       ? SetMessage.SetSuccessMessage("Information has been updated successfully.")
                       : SetMessage.SetInformationMessage("No data has been updated.");
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
        public virtual Message InsertOrUpdateWithoutIdentity(T entity)
        {
            Message message;
            try
            {
                _appDbContext.SqlConnection.Open();
                var isExist = _iBaseRepository.Get(entity);
                var affectedRow = 0;
                if (isExist == null)
                {
                    affectedRow = _iBaseRepository.InsertWithoutIdentity(entity);
                    message = affectedRow > 0
                        ? SetMessage.SetSuccessMessage("Information has been saved successfully.")
                        : SetMessage.SetInformationMessage("No data has been saved.");
                }
                else
                {
                    affectedRow = _iBaseRepository.Update(entity);
                    message = affectedRow > 0
                       ? SetMessage.SetSuccessMessage("Information has been updated successfully.")
                       : SetMessage.SetInformationMessage("No data has been updated.");
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
        public virtual Message Delete(T entity)
        {
            Message message;
            try
            {
                _appDbContext.SqlConnection.Open();
                var affectedRow = 0;
                affectedRow = _iBaseRepository.Delete(entity);
                message = affectedRow > 0
                    ? SetMessage.SetSuccessMessage("Information has been deleted successfully.")
                    : SetMessage.SetInformationMessage("No data has been deleted.");
            }
            catch (Exception exception)
            {
                message = exception.Message.Substring(0, 50) == "The DELETE statement conflicted with the REFERENCE"
                    ? SetMessage.SetInformationMessage("You can't delete this information because it is already used by other.")
                    : SetMessage.SetErrorMessage(exception.Message);
            }
            finally
            {
                _appDbContext.SqlConnection.Close();
            }
            return message;
        }
        public virtual T Get(T entity)
        {
            try
            {
                _appDbContext.SqlConnection.Open();
                return _iBaseRepository.Get(entity);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                _appDbContext.SqlConnection.Close();
            }
        }
        public virtual IEnumerable<T> GetAll()
        {
            try
            {
                _appDbContext.SqlConnection.Open();
                return _iBaseRepository.GetAll();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                _appDbContext.SqlConnection.Close();
            }
        }
        public virtual IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                _appDbContext.SqlConnection.Open();
                return _iBaseRepository.AllIncluding(includeProperties);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                _appDbContext.SqlConnection.Close();
            }
        }

        #endregion
    }
    public interface IBaseService<T> where T : class
    {
        Message InsertOrUpdate(T entity);
        Message InsertOrUpdateWithoutIdentity(T entity);
        Message Delete(T entity);
        T Get(T entity);
        IEnumerable<T> GetAll();
        IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);

    }
}
