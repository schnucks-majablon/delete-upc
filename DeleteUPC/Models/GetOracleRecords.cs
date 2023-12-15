using Oracle.ManagedDataAccess.Client;

using Oracle.ManagedDataAccess;
//using System.Data.OracleClient;
using SMIoracle64;
//using Oracle64;

using System;
using System.Data;
using System.Collections.Generic;
using System.Collections;

namespace DeleteUPC.Models
{
    public class GetOracleRecords
    {

      

            public static string ErrorMessage;
            public static string Stage;
            public static string TData;

            //    public static Int32 Return_dsd_upc_id;
            //    public static Int32 Return_whse_upc_id;



            public Helpers.massWebClientResponse GetDataSet(string query)
            {
                DataSet ds = new DataSet();
                Helpers.massWebClientResponse resp = new Helpers.massWebClientResponse();


                DataRow dRow;


                try
                {

                    ds.Tables.Add(new DataTable("Table"));
                    ds.Tables[0].Columns.Add("comm_class_code", typeof(string));
                    ds.Tables[0].Columns.Add("description", typeof(string));

                    dRow = ds.Tables[0].NewRow();


                    using (OracleConnection conn = Connections.GetConnection(SMI_DATABASES.ITEM_DB))
                    using (OracleCommand cmd = new OracleCommand(query, conn))




                    //using (conn = SMIoracle64.Connections.GetConnection(SMI_DATABASES.ITEM_DB))
                    //using (cmd = new OracleCommand(query, conn))
                    {





                        {
                            conn.Open();
                            cmd.CommandType = CommandType.Text;


                            using (OracleDataReader dr = cmd.ExecuteReader())


                            {
                                while (dr.Read())
                                {
                                    dRow["comm_class_code"] = Convert.ToString(dr["comm_class_code"]).Trim();
                                    dRow["description"] = Convert.ToString(dr["description"]).Trim();
                                    ds.Tables[0].Rows.Add(dRow);
                                }


                            }
                        }



                        // Oracle.ManagedDataAccess.Client.OracleCommand cmd = new Oracle.ManagedDataAccess.Client.OracleCommand(query, conn);

                        // mjOracle.ManagedDataAccess.Client.OracleDataAdapter dr = new Oracle.ManagedDataAccess.Client.OracleDataAdapter(cmd);


                        //  Oracle.ManagedDataAccess.Client.OracleCommand cmd = new Oracle.ManagedDataAccess.Client.OracleCommand(query, conn);

                        // Oracle.ManagedDataAccess.Client.OracleDataAdapter dr = new Oracle.ManagedDataAccess.Client.OracleDataAdapter(cmd);
                        //mj   dr.Fill(ds);

                        conn.Close();
                        conn.Dispose();

                        resp.Success = true;
                        resp.Message = "Success";
                        resp.AppDataSet = ds;

                        return resp;


                    }


                }

                catch (Exception ex)
                {

                    GetOracleRecords.ErrorMessage = ex.Message + " Query: " + query;
                    resp.Success = true;
                    resp.Message = "Failure";
                    resp.AppDataSet = ds;

                    return resp;

                }


            }




            public String DoQuery(string QueryName, string[] jsonParmNames, string[] jsonParmValues, string[] jsonParmTypes)
            {


                //   Int[] RetArray = new Int[5];
                string[] RetArray = new string[5];

                String Data = "";
                DateTime tmpValue;

                //OracleConnection conn;
                //OracleCommand cmd;




                try
                {

                    // string connectionStringValue = Oracle64.Connections.GetConnectionString(Connections.SMI_DATABASES.ITEM_DB);

                    //  OracleConnection conn;
                    //  conn = Oracle64.Connections.GetConnection(Connections.SMI_DATABASES.ITEM_DB);

                    // using (Oracle.ManagedDataAccess.Client.OracleConnection conn = new Oracle.ManagedDataAccess.Client.OracleConnection(connectionStringValue))



                    //using (conn = SMIoracle64.Connections.GetConnection(SMI_DATABASES.ITEM_DB))
                    //using (cmd = new OracleCommand(QueryName, conn))

                    //    string connectionStringValue = SMIoracle64.Connections.GetConnectionString(Connections.SMI_DATABASES.ITEM_DB);
                    //using (Oracle.ManagedDataAccess.Client.OracleConnection conn = new Oracle.ManagedDataAccess.Client.OracleConnection(connectionStringValue))

                    using (OracleConnection conn = Connections.GetConnection(SMI_DATABASES.ITEM_DB))
                    using (OracleCommand cmd = new OracleCommand(QueryName, conn))



                    {

                        conn.Open();

                        // Oracle.ManagedDataAccess.Client.OracleCommand cmd = new Oracle.ManagedDataAccess.Client.OracleCommand(QueryName, conn);
                        //       Oracle.ManagedDataAccess.Client.OracleCommand cmd = new Oracle.ManagedDataAccess.Client.OracleCommand("IMPROD.create_new_thirdparty_giftcard_proc", conn);

                        // Oracle.ManagedDataAccess.Client.OracleCommand cmd = new Oracle.ManagedDataAccess.Client.OracleCommand(QueryName, conn);

                        // Oracle.ManagedDataAccess.Client.OracleDataAdapter dr = new Oracle.ManagedDataAccess.Client.OracleDataAdapter(cmd);

                        cmd.CommandType = CommandType.StoredProcedure;


                        //     for (int i = jsonParmNames.GetLength(0) - 1; i >= 0; i--)
                        for (int i = 0; i < jsonParmNames.GetLength(0); i++)
                        {


                            switch (jsonParmTypes[i])
                            {
                                case "N":
                                    cmd.Parameters.Add(jsonParmNames[i], OracleDbType.Int64);
                                    cmd.Parameters[jsonParmNames[i]].Value = jsonParmValues[i];
                                    break;
                                case "2":
                                    cmd.Parameters.Add(jsonParmNames[i], OracleDbType.Decimal);
                                    cmd.Parameters[jsonParmNames[i]].Value = jsonParmValues[i];
                                    break;
                                case "S":
                                    cmd.Parameters.Add(jsonParmNames[i], OracleDbType.Varchar2);
                                    cmd.Parameters[jsonParmNames[i]].Value = jsonParmValues[i];
                                    break;
                                case "D":
                                    cmd.Parameters.Add(jsonParmNames[i], OracleDbType.Date);
                                    string Tempdate = jsonParmValues[i];
                                    DateTime dateTime = DateTime.Parse(Tempdate);
                                    Tempdate = dateTime.ToString("MM/dd/yy");
                                    cmd.Parameters[jsonParmNames[i]].Value = Tempdate;
                                    break;
                                case "1":
                                    cmd.Parameters.Add(jsonParmNames[i], OracleDbType.Char);
                                    cmd.Parameters[jsonParmNames[i]].Value = jsonParmValues[i];
                                    break;
                                case "O":
                                    OracleParameter output = cmd.Parameters.Add(jsonParmNames[i], OracleDbType.Varchar2, 500);
                                    output.Direction = ParameterDirection.Output;
                                    break;
                                case "R":
                                    OracleParameter refoutput = cmd.Parameters.Add(jsonParmNames[i], OracleDbType.RefCursor, 500);
                                    refoutput.Direction = ParameterDirection.Output;
                                    break;
                                case "P":
                                    cmd.Parameters.Add(jsonParmNames[i], OracleDbType.Int64).Direction = ParameterDirection.Output;
                                    break;

                            }
                        }



                        cmd.ExecuteNonQuery();




                        conn.Close();
                        conn.Dispose();
                    }


                }

                catch (Exception ex)

                {
                    GetOracleRecords.ErrorMessage = ex.Message + " Query: " + QueryName;


                }



                return TData;








            }


            public string DoUpdate(string query)
            {

                //OracleConnection conn;
                //OracleCommand cmd;

                try
                {

                    // string connectionStringValue = Oracle64.Connections.GetConnectionString(Connections.SMI_DATABASES.ITEM_DB);

                    //  OracleConnection conn;
                    //  conn = Oracle64.Connections.GetConnection(Connections.SMI_DATABASES.ITEM_DB);

                    // using (Oracle.ManagedDataAccess.Client.OracleConnection conn = new Oracle.ManagedDataAccess.Client.OracleConnection(connectionStringValue))


                    //using (conn = SMIoracle64.Connections.GetConnection(SMI_DATABASES.ITEM_DB))
                    //using (cmd = new OracleCommand(query, conn))

                    //  string connectionStringValue = SMIoracle64.Connections.GetConnectionString(Connections.SMI_DATABASES.ITEM_DB);

                    using (OracleConnection conn = Connections.GetConnection(SMI_DATABASES.ITEM_DB))
                    using (OracleCommand cmd = new OracleCommand(query, conn))

                    // using (Oracle.ManagedDataAccess.Client.OracleConnection conn = new Oracle.ManagedDataAccess.Client.OracleConnection(connectionStringValue))
                    {

                        conn.Open();

                        // Oracle.ManagedDataAccess.Client.OracleCommand cmd = new Oracle.ManagedDataAccess.Client.OracleCommand(query, conn);

                        //  Oracle.ManagedDataAccess.Client.OracleCommand cmd = new Oracle.ManagedDataAccess.Client.OracleCommand(query, conn);

                        // Oracle.ManagedDataAccess.Client.OracleDataAdapter dr = new Oracle.ManagedDataAccess.Client.OracleDataAdapter(cmd);

                        cmd.ExecuteNonQuery();

                        conn.Close();
                        conn.Dispose();
                        return "ok";
                    }


                }

                catch (Exception ex)

                {
                    GetOracleRecords.ErrorMessage = ex.Message + " Query: " + query;
                    //Console.WriteLine(ex.Message);
                    return "not ok";
                }


            }




        }
    }
