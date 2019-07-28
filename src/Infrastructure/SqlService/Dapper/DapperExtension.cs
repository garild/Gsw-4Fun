using Dapper;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SqlServices.Dapper
{
    public static class DapperExtension
    {
        public static IEnumerable<dynamic> Query(this SqlConnection sqlConnection, string query)
        {
            return sqlConnection.Query<dynamic>(query).ToList();
        }

        public static dynamic ExecuteQuery(this SqlConnection sqlConnection, string query)
        {
            return sqlConnection.Query<dynamic>(query).FirstOrDefault();
        }

    }

    public class DapperContribExtension<T> where T: class
    {
        private readonly SqlConnection _sqlConnection;

        public DapperContribExtension(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public void AsInsert( T data)
        {
            _sqlConnection.Insert(data);
        }
    }
}
