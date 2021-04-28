using System.Data;

using System.Data.SqlClient;

using DbClientCommon;

namespace SqlServerClient
{
    public class SqlServerClient_Base : DbClientBase<SqlConnection, SqlCommand, SqlDataAdapter>
    {
        public void SqlBulkCopy_WriteToServer(string destinationTableName, DataTable dataTable)
        {
            ConnectionProcess(destinationTableName, delegate (SqlConnection sqlConnection)
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
