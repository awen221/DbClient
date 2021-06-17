using System.Data;
using System.Data.Common;

namespace DbClient
{
    public class DbClient <TDbConnection, TDbCommand, TDbDataAdapter> : IDb_interface
        where TDbConnection : DbConnection, new()
        where TDbCommand : DbCommand, new()
        where TDbDataAdapter : DbDataAdapter, new()
    {
        protected delegate object Delegate_Connection(TDbConnection Connection);
        protected object ConnectionProcess(string ConnectionString, Delegate_Connection delegate_Connection)
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
        protected object CommandProcess(string ConnectionString, string CommandText, Delegate_Command delegate_Command)
        {
            return ConnectionProcess(ConnectionString, delegate (TDbConnection Connection)
            {
                object result = null;
                using (var Command = new TDbCommand())
                {
                    Command.CommandText = CommandText;
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
        protected object DataAdapterProcess(string ConnectionString, string CommandText, Delegate_DataAdapter delegate_DataAdapter)
        {
            return CommandProcess(ConnectionString, CommandText, delegate (TDbCommand Command)
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



        public DataTable DataAdapter_Fill_DataTable(string ConnectionString, string CommandText)
        {
            return (DataTable)DataAdapterProcess(ConnectionString, CommandText, delegate (TDbDataAdapter DataAdapter)
            {
                var data = new DataTable();
                DataAdapter.Fill(data);
                return data;
            });
        }
        public DataSet DataAdapter_Fill_DataSet(string ConnectionString, string CommandText)
        {
            return (DataSet)DataAdapterProcess(ConnectionString, CommandText, delegate (TDbDataAdapter DataAdapter)
            {
                var data = new DataSet();
                DataAdapter.Fill(data);
                return data;
            });
        }
        public bool DataReader_HasRows(string ConnectionString, string CommandText)
        {
            return (bool)CommandProcess(ConnectionString, CommandText, delegate (TDbCommand Command)
            {
                using (var DataReader = Command.ExecuteReader())
                {
                    return DataReader.HasRows;
                }
            });
        }
        public int ExecuteNonQuery(string ConnectionString, string CommandText)
        {
            return (int)CommandProcess(ConnectionString, CommandText, delegate (TDbCommand Command)
            {
                return Command.ExecuteNonQuery();
            });
        }
    }
}
