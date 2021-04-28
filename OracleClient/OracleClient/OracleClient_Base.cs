using DbClientCommon;
using Oracle.ManagedDataAccess.Client;

namespace OracleClient
{
    public class OracleClient_Base : DbClientBase<OracleConnection, OracleCommand, OracleDataAdapter> { }
}
