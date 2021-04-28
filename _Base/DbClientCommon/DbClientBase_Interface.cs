using System.Data;

namespace DbClientCommon
{
    public interface DbClientBase_Interface
    {
        void Init(string connectionString);

        bool DataReader_HasRows(string sql);
        int ExecuteNonQuery(string sql);
        DataTable FillDataTable(string sql);
        DataSet FillDataSet(string sql);
    }
}
