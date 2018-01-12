using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Dapper;
using SoftwareGrid.Model.iTestApp.QuestionManagement;
using SoftwareGrid.Model.iTestApp.UserManagement;

namespace SoftwareGrid.Repository.iTestApp.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        #region Private Variable
        private readonly DbContext _dbContext;
        #endregion

        #region Constructor
        public BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Virtual Method
        public virtual int Insert(T entity)
        {
            SetOtherKeyValue(entity);
            var query = QB<T>.Insert();
            return _dbContext.SqlConnection.Query<int>(query, entity).FirstOrDefault();
        }
        public virtual int InsertWithoutIdentity(T entity)
        {
            SetPrimaryKeyValue(entity);
            SetOtherKeyValue(entity);
            var query = QB<T>.InsertWithoutIdentityColumn();
            return _dbContext.SqlConnection.Query<int>(query, entity).FirstOrDefault();
        }
        public virtual int Update(T entity)
        {
            SetOtherKeyValue(entity);
            var query = QB<T>.Update();
            return _dbContext.SqlConnection.Query<int>(query, entity).FirstOrDefault();
        }
        public virtual int Delete(T entity)
        {
            var query = QB<T>.Delete();
            return _dbContext.SqlConnection.Query<int>(query, entity).FirstOrDefault();
        }
        public virtual T Get(T entity)
        {
            var query = QB<T>.SelectByPrimaryKey();
            return _dbContext.SqlConnection.Query<T>(query, entity).FirstOrDefault();
        }
        public virtual IEnumerable<T> GetAll()
        {
            var query = QB<T>.Select();
            return _dbContext.SqlConnection.Query<T>(query).ToList();
        }
        public virtual IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            var query = QB<T>.Select();
            return _dbContext.SqlConnection.Query<T>(query, includeProperties).ToList();
        }
        public virtual int GetNewId()
        {
            var query = QB<T>.GetNewId();
            return _dbContext.SqlConnection.Query<int>(query).FirstOrDefault();
        }
        internal T SetPrimaryKeyValue(T entity)
        {
            var primaryKey = QB<T>.GetPrimaryKeyColumns()[0];
            var newIdQuery = QB<T>.GetNewId();
            var newId = _dbContext.SqlConnection.Query<int>(newIdQuery, entity).Single();
            PropertyInfo propertyInfo = entity.GetType().GetProperty(Convert.ToString(primaryKey));
            propertyInfo.SetValue(entity, Convert.ChangeType(newId, propertyInfo.PropertyType), null);
            return entity;
        }
        internal T SetOtherKeyValue(T entity)
        {
            var user = System.Web.HttpContext.Current.Session["Web.UI.LoggedInUser"] as User;
            if (user == null)
            {
                user = new User {UserId=0};
            }
            PropertyInfo propertyInfo = entity.GetType().GetProperty(Convert.ToString("CreatedDate"));
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(entity, Convert.ChangeType(GetServerDate(), propertyInfo.PropertyType), null);
            }
           
            PropertyInfo propertyInfoUser = entity.GetType().GetProperty(Convert.ToString("CreatedByUserId"));
            if (propertyInfoUser != null)
            {
                if (Nullable.GetUnderlyingType(propertyInfoUser.PropertyType) == null)
                {
                    propertyInfoUser.SetValue(entity, Convert.ChangeType(user.UserId, propertyInfoUser.PropertyType), null);
                }
            }
            return entity;
        }
        internal DateTime GetServerDate()
        {
            return _dbContext.SqlConnection.Query<DateTime>("SELECT GETUTCDATE()").Single();
        }

        public virtual T GetWithNavigationProperty(T entity)
        {
            var query = QB<T>.SelectByPrimaryKey();
            var foreignKeyList = new List<string>();
            var data = _dbContext.SqlConnection.Query<T>(query, entity).FirstOrDefault();
            if (data != null)
            {
                var virtualAttributes = typeof(T).GetProperties().Where(p => p.GetMethod.IsVirtual);
                var foreignAttribute = typeof(T).GetProperties().Where(prop => prop.IsDefined(typeof(ForeignKeyAttribute), false));
                var enumerable = foreignAttribute as PropertyInfo[] ?? foreignAttribute.ToArray();
                if (enumerable.Any())
                {
                    foreignKeyList.AddRange(enumerable.Select(atr => atr.Name));
                }
                var propertyInfos = virtualAttributes as PropertyInfo[] ?? virtualAttributes.ToArray();
                if (propertyInfos.Any())
                {
                    foreach (var attributes in propertyInfos)
                    {
                        var type = attributes.PropertyType;
                        string name;
                        string primaryKey = string.Empty;
                        var isList = false;

                        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                        {
                            var itemType = type.GetGenericArguments()[0];
                            name = itemType.FullName + "," + itemType.Assembly.FullName;
                            isList = true;
                        }
                        else
                        {
                            name = type.FullName + "," + type.Assembly.FullName;
                        }

                        var classType = Type.GetType(name, true);
                        var o = (Activator.CreateInstance(classType));
                        var keyAttribute = Type.GetType(name, true).GetProperties().FirstOrDefault(prop => prop.IsDefined(typeof(KeyAttribute), false));
                        if (keyAttribute != null)
                        {
                            primaryKey = keyAttribute.Name;
                        }

                        var tableAttribute = o.GetType().GetCustomAttributes(true).FirstOrDefault(a => a.GetType().Name == "TableAttribute");

                        if (tableAttribute != null)
                        {
                            var innerQuery = string.Empty;
                            var attribute = (TableAttribute)tableAttribute;
                            if (!string.IsNullOrEmpty(primaryKey))
                            {
                                if (foreignKeyList.Count > 0 && foreignKeyList.Contains(primaryKey))
                                {
                                    innerQuery = "SELECT * FROM " + attribute.Schema + ".[" + attribute.Name +
                                                 "] WHERE " + primaryKey + "=@" + primaryKey;
                                }
                                else
                                {
                                    innerQuery = "SELECT * FROM " + attribute.Schema + ".[" + attribute.Name +
                                                 "] " + QB<T>.GetWhereClause();
                                }

                                var innerValue = _dbContext.SqlConnection.Query<dynamic>(innerQuery, data).FirstOrDefault();

                                if (innerValue != null)
                                {
                                    PropertyInfo propertyInfo = data.GetType().GetProperty(Convert.ToString(classType.Name));
                                    if (propertyInfo != null)
                                    {
                                        propertyInfo.SetValue(data, Convert.ChangeType((QuestionCategory)innerValue, propertyInfo.PropertyType), null);
                                    }
                                }

                            }
                            
                        }
                    }
                }

            }
            return data;

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
        T GetWithNavigationProperty(T entity);
        IEnumerable<T> GetAll();
        IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        int GetNewId();

    }
}
