using System;
using System.Configuration;
using System.Data.SqlClient;

namespace SmebyFX_blog.Data.BaseClasses
{
    public abstract class DaoBase
    {
        protected T Run<T>(Func<SqlConnection, T> query)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                return query(connection);
            }
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["blogDBconnectionString"].ConnectionString);
        }
    }
}
