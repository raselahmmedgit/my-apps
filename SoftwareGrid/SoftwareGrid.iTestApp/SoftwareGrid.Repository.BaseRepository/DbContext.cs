using System.Data.SqlClient;
using SoftwareGrid.Model.Utility;

namespace SoftwareGrid.Repository.Base
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
