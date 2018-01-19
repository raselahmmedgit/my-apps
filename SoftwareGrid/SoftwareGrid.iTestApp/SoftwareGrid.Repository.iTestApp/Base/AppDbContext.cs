using System.Data.SqlClient;
using SoftwareGrid.Model.iTestApp.Utility;

namespace SoftwareGrid.Repository.iTestApp.Base
{
    public class AppDbContext
    {
        public SqlConnection SqlConnection;
        public AppDbContext()
        {
            SqlConnection = new SqlConnection(AppDbConfig.ConnectionString);
        }
    }
}
