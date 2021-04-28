using System.Data;
using System.Data.Common;

namespace DbClientCommon
{

    public class DbClientBase
        <TDbConnection, TDbCommand, TDbDataAdapter>
        : DbClientBase_Interface
        where TDbConnection : DbConnection, new()
        where TDbCommand : DbCommand, new()
        where TDbDataAdapter : DbDataAdapter, new()
    {

        public DbClientBase() { }

        string ConnectionString { set; get; }
        public void Init(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected delegate object Delegate_Connection(TDbConnection Connection);
        protected object ConnectionProcess(object para, Delegate_Connection delegate_Connection)
        {
            object result = null;

            using (var Connection = new TDbConnection())
            {
                Connection.ConnectionString = ConnectionString;

                Connection.Open();

                if (delegate_Connection != null)
                {
                    result = delegate_Connection.Invoke(Connection);
                }

                Connection.Close();
            }

            return result;
        }

        protected delegate object Delegate_Command(TDbCommand Command);
        protected object CommandProcess(string txt, Delegate_Command delegate_Command)
        {
            return ConnectionProcess(txt, delegate (TDbConnection Connection)
            {
                object result = null;
                using (var Command = new TDbCommand())
                {
                    Command.CommandText = txt;
                    Command.Connection = Connection;

                    if (delegate_Command != null)
                    {
                        result = delegate_Command.Invoke(Command);
                    }
                }
                return result;
            });
        }

        protected delegate object Delegate_DataAdapter(TDbDataAdapter DataAdapter);
        protected object DataAdapterProcess(string txt, Delegate_DataAdapter delegate_DataAdapter)
        {
            return CommandProcess(txt, delegate (TDbCommand Command)
            {
                object result = null;
                using (var DataAdapter = new TDbDataAdapter())
                {
                    DataAdapter.SelectCommand = Command;
                    if (delegate_DataAdapter != null)
                    {
                        result = delegate_DataAdapter.Invoke(DataAdapter);
                    }
                }
                return result;
            });
        }


        public bool DataReader_HasRows(string sql)
        {
            return (bool)CommandProcess(sql, delegate (TDbCommand Command)
            {
                using (var DataReader = Command.ExecuteReader())
                {
                    return DataReader.HasRows;
                }
            });
        }
        public int ExecuteNonQuery(string sql)
        {
            return (int)CommandProcess(sql, delegate (TDbCommand Command)
            {
                return Command.ExecuteNonQuery();
            });
        }
        public DataTable FillDataTable(string sql)
        {
            var result = DataAdapterProcess(sql, delegate (TDbDataAdapter DataAdapter)
            {
                var data = new DataTable();
                DataAdapter.Fill(data);
                return data;
            });

            return (DataTable)result;
        }
        public DataSet FillDataSet(string sql)
        {
            var result = DataAdapterProcess(sql, delegate (TDbDataAdapter DataAdapter)
            {
                var data = new DataSet();
                DataAdapter.Fill(data);
                return data;
            });

            return (DataSet)result;
        }

    }

}
