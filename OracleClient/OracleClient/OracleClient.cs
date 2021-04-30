using Oracle.ManagedDataAccess.Client;

using DbClient;

namespace OracleClient
{
    public class OracleClient : DbClient<OracleConnection, OracleCommand, OracleDataAdapter> { }
}
