using MySql.Data.MySqlClient;

using DbClientCommon;

namespace MySqlClient
{
    abstract public class MySqlClient_Base: DbClientBase
        <MySqlConnectionStringBuilder, MySqlConnection, MySqlCommand, MySqlDataAdapter>
    {
    }
}
