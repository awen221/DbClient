using MySql.Data.MySqlClient;

using DbClientCommon;

namespace MySqlClient
{
    public class MySqlClient_Base : DbClientBase<MySqlConnection, MySqlCommand, MySqlDataAdapter> { }
}
