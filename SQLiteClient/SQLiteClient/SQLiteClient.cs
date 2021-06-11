
using System.Data.SQLite;

using DbClient;

namespace SQLiteClient
{
    public class SQLiteClient : DbClient<SQLiteConnection, SQLiteCommand, SQLiteDataAdapter> 
    {
        public void ConnectionCreateFile(string filename) => SQLiteConnection.CreateFile(filename);
    }
}
