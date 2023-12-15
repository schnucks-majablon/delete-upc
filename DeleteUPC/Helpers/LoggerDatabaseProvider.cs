using System;
using Oracle.ManagedDataAccess.Client;
using SMIoracle64;
using System.Data;

namespace DeleteUPC.Helpers
{
    public class LoggerDatabaseProvider : ILogger
    {
        public string LogError(string appName, string errorMsg, string stackTrace)
        {
            string sql = "IMPROD.mass_app_error_log_proc";
            OracleConnection conn;
            OracleCommand cmd;

            try
            {
                using (conn = Connections.GetConnection(SMI_DATABASES.ITEM_DB))
                using (cmd = new OracleCommand(sql, conn))
                {
                    conn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("iapp_name", OracleDbType.Varchar2, appName, ParameterDirection.Input);
                    cmd.Parameters.Add("ierror_msg", OracleDbType.Varchar2, errorMsg, ParameterDirection.Input);
                    cmd.Parameters.Add("istack_trace", OracleDbType.Varchar2, stackTrace, ParameterDirection.Input);
                    cmd.Parameters.Add("icreate_id", OracleDbType.Varchar2, WebHelpers.GetUserlogin, ParameterDirection.Input);
                    cmd.Parameters.Add("oerror_id", OracleDbType.Int64).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                }

                // return error id from the procedure to display to user
                return (cmd.Parameters["oerror_id"].Value.ToString());

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
