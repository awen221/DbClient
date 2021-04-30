namespace DbClient
{
    public interface Db_interface
    {
        object DataAdapter_Fill_DataTable(string connectionString, string commandText);
        object DataAdapter_Fill_DataSet(string connectionString, string commandText);
        object DataReader_HasRows(string connectionString, string commandText);
        object ExecuteNonQuery(string connectionString, string commandText);
    }
}
