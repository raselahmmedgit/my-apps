using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace rabapp.Repository.Common
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        #region Private Variable
        private readonly AppDbContext _appDbContext;
        #endregion

        #region Constructor
        public BaseRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #endregion

        #region Virtual Method
        public virtual int Insert(T entity)
        {
            SetOtherKeyValue(entity);
            var query = QueryBuilder<T>.Insert();
            return _appDbContext.SqlConnection.Query<int>(query, entity).FirstOrDefault();
        }
        public virtual int InsertWithoutIdentity(T entity)
        {
            SetPrimaryKeyValue(entity);
            SetOtherKeyValue(entity);
            var query = QueryBuilder<T>.InsertWithoutIdentityColumn();
            return _appDbContext.SqlConnection.Query<int>(query, entity).FirstOrDefault();
        }
        public virtual int Update(T entity)
        {
            SetOtherKeyValue(entity);
            var query = QueryBuilder<T>.Update();
            return _appDbContext.SqlConnection.Query<int>(query, entity).FirstOrDefault();
        }
        public virtual int Delete(T entity)
        {
            var query = QueryBuilder<T>.Delete();
            return _appDbContext.SqlConnection.Query<int>(query, entity).FirstOrDefault();
        }
        public virtual T Get(T entity)
        {
            var query = QueryBuilder<T>.SelectByPrimaryKey();
            return _appDbContext.SqlConnection.Query<T>(query, entity).FirstOrDefault();
        }
        public virtual IEnumerable<T> GetAll()
        {
            var query = QueryBuilder<T>.Select();
            return _appDbContext.SqlConnection.Query<T>(query).ToList();
        }
        public virtual IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            var query = QueryBuilder<T>.Select();
            return _appDbContext.SqlConnection.Query<T>(query, includeProperties).ToList();
        }
        public virtual int GetNewId()
        {
            var query = QueryBuilder<T>.GetNewId();
            return _appDbContext.SqlConnection.Query<int>(query).FirstOrDefault();
        }
        internal T SetPrimaryKeyValue(T entity)
        {
            var primaryKey = QueryBuilder<T>.GetPrimaryKeyColumns()[0];
            var newIdQuery = QueryBuilder<T>.GetNewId();
            var newId = _appDbContext.SqlConnection.Query<int>(newIdQuery, entity).Single();
            PropertyInfo propertyInfo = entity.GetType().GetProperty(Convert.ToString(primaryKey));
            propertyInfo.SetValue(entity, Convert.ChangeType(newId, propertyInfo.PropertyType), null);
            return entity;
        }
        internal T SetOtherKeyValue(T entity)
        {
            PropertyInfo propertyInfo = entity.GetType().GetProperty(Convert.ToString("CreatedDate"));
            propertyInfo.SetValue(entity, Convert.ChangeType(GetServerDate(), propertyInfo.PropertyType), null);

            PropertyInfo propertyInfoUser = entity.GetType().GetProperty(Convert.ToString("CreatedByUserId"));
            propertyInfoUser.SetValue(entity, Convert.ChangeType(1, propertyInfoUser.PropertyType), null);

            return entity;
        }
        internal DateTime GetServerDate()
        {
            return _appDbContext.SqlConnection.Query<DateTime>("SELECT GETUTCDATE()").Single();
        }

        #endregion

    }

    public interface IBaseRepository<T> where T : class
    {
        int Insert(T entity);
        int InsertWithoutIdentity(T entity);
        int Update(T entity);
        int Delete(T entity);
        T Get(T entity);
        IEnumerable<T> GetAll();
        IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        int GetNewId();

    }
}
