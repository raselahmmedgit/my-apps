using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace SPEDU.Data.Infrastructure
{
    public interface IRepositoryBase<T> where T : class
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        T GetById(long id);
        T GetById(string id);
        T Get(Expression<Func<T, bool>> where);
        List<T> GetAll();
        List<T> GetMany(Expression<Func<T, bool>> where);
    }
}
