using SPEDU.Data.Infrastructure;

namespace SPEDU.Data.Repositories
{
    public class Repository<T> : RepositoryBase<T> where T : class
    {
        public Repository(IDatabaseFactory iDatabaseFactory)
            : base(iDatabaseFactory)
        {
            //constructor
        }
    }
}
