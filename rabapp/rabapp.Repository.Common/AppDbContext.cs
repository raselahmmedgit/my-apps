using rabapp.Model.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rabapp.Repository.Common
{
    public class AppDbContext
    {
        public SqlConnection SqlConnection;
        public AppDbContext()
        {
            SqlConnection = new SqlConnection(DbConfig.ConnectionString);
        }
    }
}
