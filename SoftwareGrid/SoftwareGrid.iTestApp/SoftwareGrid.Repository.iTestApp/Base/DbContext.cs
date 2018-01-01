using System.Data.SqlClient;
using SoftwareGrid.Model.iTestApp.Utility;

namespace SoftwareGrid.Repository.iTestApp.Base
{
    public class DbContext
    {
        public SqlConnection SqlConnection;
        public DbContext()
        {
            SqlConnection = new SqlConnection(DbConfig.ConnectionString);
        }
    }
}
