using MySql.Data.MySqlClient;

using DbClient;

namespace MySqlClient
{
    public class MySqlClient : DbClient<MySqlConnection, MySqlCommand, MySqlDataAdapter> { }
}
