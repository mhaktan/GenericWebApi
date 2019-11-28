using Dapper;
using SdCommonService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SdCommonService.Helpers
{
    public class QueryHelper
    {
        public string GetWhereClause(List<Parameter> _parameters)
        {
            string filter = "";
            if (_parameters != null)
            {
                filter = " WHERE ";
                foreach (var item in _parameters)
                {
                    filter += item.ColumnName + "=" + item.Value + " AND ";

                }
                filter = filter.Substring(0, filter.Length - 5);

            }

            return filter;

        }

        public string GetOrderByClause(OrderBy _orderBy)
        {
            string orderBy = "";
            if (_orderBy != null)
            {
                orderBy = " ORDER BY ";
                foreach (var item in _orderBy.ColumnName)
                {
                    orderBy += string.Concat(item, ",");

                }
                orderBy = orderBy.Substring(0, orderBy.Length - 1);
                orderBy = string.Concat(orderBy, " ", _orderBy.OrderType);

            }
            return orderBy;

        }

        public IEnumerable<dynamic> ExecuteQuery(string _query)
        {
            IEnumerable<dynamic> result = null;
            try
            {
                using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionName"].ToString()))
                {
                    result = sqlConnection.Query(_query);
                }
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
                //log 
            }

        }
    }
}