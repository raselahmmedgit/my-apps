using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace SoftwareGrid.Repository.iTestApp.Base
{
    public class ExecuteQuery
    {
        #region Private Variable
        private readonly DbContext _dbContext;
        #endregion

        #region Constructor
        public ExecuteQuery(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Public Method
        public object Execute(string commandText, string[] paramNames, string[] paramValues, bool isStoredProcedure = false)
        {
            dynamic data = new object();
            var parameters = new DynamicParameters();
            for (int i = 0; i < paramNames.Count(); i++)
            {
                parameters.Add("@" + paramNames[i], paramValues[i]);
            }
            if (isStoredProcedure)
            {
                data = _dbContext.SqlConnection.Query<dynamic>(commandText, parameters,
                    commandType: CommandType.StoredProcedure);
            }
            else
            {
                data = _dbContext.SqlConnection.Query<dynamic>(commandText, parameters);
            }

            var dataTable = ConvertToDataTable(data);
            var obj = GetTableRows(dataTable);
            return obj;
        }
        #endregion

        #region  Private Method
        private DataTable ConvertToDataTable(IEnumerable<dynamic> items)
        {
            if (items == null || !items.Any()) return new DataTable();
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
