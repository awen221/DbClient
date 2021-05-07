using System.Data;
using System.Data.Common;

namespace DbClient
{
    public class DbClient <TDbConnection, TDbCommand, TDbDataAdapter>
        : Db_interface
        where TDbConnection : DbConnection, new()
        where TDbCommand : DbCommand, new()
        where TDbDataAdapter : DbDataAdapter, new()
    {
        protected delegate object Delegate_Connection(TDbConnection Connection);
        protected object ConnectionProcess(string connectString, object para, Delegate_Connection delegate_Connection)
        {
            object result = null;

            using (var Connection = new TDbConnection())
            {
                Connection.ConnectionString = connectString;

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
        protected object CommandProcess(string connectString, string commandText, Delegate_Command delegate_Command)
        {
            return ConnectionProcess(connectString, commandText, delegate (TDbConnection Connection)
            {
                object result = null;
                using (var Command = new TDbCommand())
                {
                    Command.CommandText = commandText;
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
        protected object DataAdapterProcess(string connectString, string commandText, Delegate_DataAdapter delegate_DataAdapter)
        {
            return CommandProcess(connectString, commandText, delegate (TDbCommand Command)
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



        public object DataAdapter_Fill_DataTable(string connectString, string commandText)
        {
            var result = DataAdapterProcess(connectString, commandText, delegate (TDbDataAdapter DataAdapter)
            {
                var data = new DataTable();
                DataAdapter.Fill(data);
                return data;
            });

            return (DataTable)result;
        }
        public object DataAdapter_Fill_DataSet(string connectString, string commandText)
        {
            var result = DataAdapterProcess(connectString, commandText, delegate (TDbDataAdapter DataAdapter)
            {
                var data = new DataSet();
                DataAdapter.Fill(data);
                return data;
            });

            return (DataSet)result;
        }
        public object DataReader_HasRows(string connectString, string commandText)
        {
            return (bool)CommandProcess(connectString, commandText, delegate (TDbCommand Command)
            {
                using (var DataReader = Command.ExecuteReader())
                {
                    return DataReader.HasRows;
                }
            });
        }
        public object ExecuteNonQuery(string connectString, string commandText)
        {
            return (int)CommandProcess(connectString, commandText, delegate (TDbCommand Command)
            {
                return Command.ExecuteNonQuery();
            });
        }
    }
}
