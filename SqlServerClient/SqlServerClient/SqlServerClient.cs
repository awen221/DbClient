using System.Data;

using System.Data.SqlClient;

using DbClient;

namespace SqlServerClient
{
    public class SqlServerClient : DbClient<SqlConnection, SqlCommand, SqlDataAdapter>
    {
        public void SqlBulkCopy_WriteToServer(string connectString, string destinationTableName, DataTable dataTable)
        {
            ConnectionProcess(connectString, destinationTableName, delegate (SqlConnection sqlConnection)
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlConnection))
                {
                    sqlBulkCopy.DestinationTableName = destinationTableName;
                    sqlBulkCopy.WriteToServer(dataTable);
                }
                return null;
            });
        }
    }
}
