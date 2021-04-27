using DbClientCommon;
using Oracle.ManagedDataAccess.Client;

namespace OracleClient
{
    abstract public class OracleClient_Base : DbClientBase
        <OracleConnectionStringBuilder, OracleConnection, OracleCommand, OracleDataAdapter>
    {
    }
}
