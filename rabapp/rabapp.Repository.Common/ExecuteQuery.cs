using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace rabapp.Repository.Common
{
    public class ExecuteQuery
    {
        #region Private Variable
        private readonly AppDbContext _appDbContext;
        #endregion

        #region Constructor
        public ExecuteQuery(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        #endregion

        #region Public Method
        public object Execute(string commandText, string[] paramNames, string[] paramValues, bool isStoredProcedure = false)
        {
            var parameters = new DynamicParameters();
            for (int i = 0; i < paramNames.Count(); i++)
            {
                parameters.Add("@" + paramNames[i], paramValues[i]);
            }
            var data = _appDbContext.SqlConnection.Query<dynamic>(commandText, parameters, commandType: isStoredProcedure ? CommandType.StoredProcedure : CommandType.TableDirect);
            var dataTable = ConvertToDataTable(data);
            var obj = GetTableRows(dataTable);
            return obj;
        }
        #endregion

        #region  Private Method
        private DataTable ConvertToDataTable(IEnumerable<dynamic> items)
        {
            if (items == null) return new DataTable();
            var t = new DataTable();
            var enumerable = items as dynamic[] ?? items.ToArray();
            var first = (IDictionary<string, object>)enumerable.First();
            foreach (var k in first.Keys)
            {
                var c = t.Columns.Add(k);
                var val = first[k];
                if (val != null) c.DataType = val.GetType();
            }
            foreach (var item in enumerable)
            {
                var r = t.NewRow();
                var i = (IDictionary<string, object>)item;
                foreach (var k in i.Keys)
                {
                    var val = i[k] ?? DBNull.Value;
                    r[k] = val;
                }
                t.Rows.Add(r);
            }
            return t;
        }

        private List<Dictionary<string, object>> GetTableRows(DataTable dtData)
        {
            if (dtData == null) return new List<Dictionary<string, object>>();
            var lstRows = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dtData.Rows)
            {
                var dictRow = new Dictionary<string, object>();
                foreach (DataColumn col in dtData.Columns)
                {
                    dictRow.Add(col.ColumnName, dr[col]);
                }
                lstRows.Add(dictRow);
            }
            return lstRows;
        }
        #endregion
    }
}
