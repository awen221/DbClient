using System.Data;

namespace DbClient
{
    public interface Db_interface
    {
        DataTable DataAdapter_Fill_DataTable(string ConnectionString, string CommandText);
        DataSet DataAdapter_Fill_DataSet(string ConnectionString, string CommandText);
        bool DataReader_HasRows(string ConnectionString, string CommandText);
        int ExecuteNonQuery(string ConnectionString, string CommandText);
    }
}
