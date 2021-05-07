namespace DbClient
{
    public interface Db_interface
    {
        object DataAdapter_Fill_DataTable(string connectString, string commandText);
        object DataAdapter_Fill_DataSet(string connectString, string commandText);
        object DataReader_HasRows(string connectString, string commandText);
        object ExecuteNonQuery(string connectString, string commandText);
    }
}
