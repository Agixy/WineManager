using System.Data.SqlClient;

namespace WineManagerService
{
    public abstract class SqlExecuter
    {        
        public SqlDataReader ExecuteSqlQuery(string connectionString, string sql)
        {
            SqlConnection connection = new SqlConnection(connectionString);
           
            connection.Open();
     
            SqlCommand command = new SqlCommand(sql, connection);
            var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                return reader;
            }

            return null;
        }
    }
}
