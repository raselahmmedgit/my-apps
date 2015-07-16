using System.Linq;

namespace SPEDU.Business
{
    public interface IGeneric<T> where T : class
    {
        int CreateOrUpdate(T entity);
        int Create(T entity);
        int Update(T entity);
        int Delete(T entity);
        int Delete(long id);
        //int Delete(Expression<Func<T, bool>> where);
        T GetById(long id);
        //T GetById(string id);
        //T Get(Expression<Func<T, bool>> where);
        IQueryable<T> GetAll();
        //IQueryable<T> GetMany(Expression<Func<T, bool>> where);

        //void Save();
        int Save();
    }
}
