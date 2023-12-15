using System;
using System.Linq;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Collections.Generic;


using DeleteUPC.Helpers;
using System.Globalization;
using SMIoracle64;
using Microsoft.AspNetCore.Http;

using System.Threading;

using Newtonsoft.Json;

namespace DeleteUPC.Models
{
    public class DUPC
    {

        private readonly ILogger _logger;
        public static string TempString = "";

        public DUPC(ILogger logger)

        {
            _logger = logger;
        }


        public massWebClientResponse ValidateUPC(string TempUPC)
        {
            massWebClientResponse resp = new massWebClientResponse();


            var sql = "";

            sql = "select a.UPC from UPC_ALT_MASTER a, UPC_ID_ITEM_XREF b where a.upc_id = b.UPC_ID and (a.UPC = :TempUPC or b.item_nbr = :TempUPC) ";

            DataSet ds = new DataSet();

            DataRow dRow;



            try
            {

                ds.Tables.Add(new DataTable("Table"));
                ds.Tables[0].Columns.Add("UPC", typeof(string));

                dRow = ds.Tables[0].NewRow();


                using (OracleConnection conn = Connections.GetConnection(SMI_DATABASES.ITEM_DB))
                using (OracleCommand cmd = new OracleCommand(sql, conn))

                {

                    {
                        cmd.Parameters.Add("TempUPC", OracleDbType.Varchar2, TempUPC, ParameterDirection.Input);
                        conn.Open();
                        cmd.CommandType = CommandType.Text;


                        using (OracleDataReader dr = cmd.ExecuteReader())


                        {
                            while (dr.Read())
                            {

                                dRow = ds.Tables[0].NewRow();

                                dRow["UPC"] = Convert.ToString(dr["UPC"]).Trim();

                                ds.Tables[0].Rows.Add(dRow);


                            }


                        }
                    }



                    conn.Close();
                    conn.Dispose();

                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        resp.Success = true;
                        resp.Message = "Success";
                        resp.AppDataSet = ds;

                        return resp;
                    }
                    else
                    {

                        resp.Success = false;
                        resp.Message = "Failure";
                        resp.AppDataSet = ds;

                        return resp;
                    }


                }


            }

            catch (Exception ex)
            {

                GetOracleRecords.ErrorMessage = ex.Message + " Query: " + sql;
                resp.Success = true;
                resp.Message = "Failure";
                resp.AppDataSet = ds;

                return resp;
            }
        }

        public massWebClientResponse DisplayUPCinResults(string TempUPC)
        {
            massWebClientResponse resp = new massWebClientResponse();


            var sql = "";
            sql = sql + "Select ";
            sql = sql + "c.UPC_ID, ";
            sql = sql + "c.UPC,   ";
            sql = sql + "d.PRODUCT_DESCRIPTION,  ";
            sql = sql + "nvl(e.ITEM_DIV,'0') item_div, ";
            sql = sql + "nvl(lpad(e.ITEM_NBR,7,0), '00000000' )  item_nbr    ";

            sql = sql + "from ";

            sql = sql + "UPC_ID_XREF c, ";
            sql = sql + "UPC_ID_DESCRIPTIONS d, ";
            sql = sql + "UPC_ID_ITEM_XREF e ";

            sql = sql + "where ";

            sql = sql + "c.UPC_ID = d.UPC_ID ";
            sql = sql + "and c.UPC_ID = e.UPC_ID (+) "; // mj07312023

            sql = sql + "and (c.UPC = :TempUPC or e.item_nbr = :TempUPC) ";
            sql = sql + "order by  c.upc ";



            DataSet ds = new DataSet();

            DataRow dRow;



            try
            {

                ds.Tables.Add(new DataTable("Table"));
                ds.Tables[0].Columns.Add("ITEMDIV", typeof(string));
                ds.Tables[0].Columns.Add("ITEMNBR", typeof(string));
                ds.Tables[0].Columns.Add("UPC", typeof(string));
                ds.Tables[0].Columns.Add("PRODUCTDESCRIPTION", typeof(string));
                ds.Tables[0].Columns.Add("UPCID", typeof(string));

                dRow = ds.Tables[0].NewRow();


                using (OracleConnection conn = Connections.GetConnection(SMI_DATABASES.ITEM_DB))
                using (OracleCommand cmd = new OracleCommand(sql, conn))

                {

                    {
                        cmd.Parameters.Add("TempUPC", OracleDbType.Varchar2, TempUPC, ParameterDirection.Input);
                        conn.Open();
                        cmd.CommandType = CommandType.Text;


                        using (OracleDataReader dr = cmd.ExecuteReader())


                        {
                            while (dr.Read())
                            {

                                dRow = ds.Tables[0].NewRow();

                                dRow["ITEMDIV"] = Convert.ToString(dr["ITEM_DIV"]).Trim();
                                dRow["ITEMNBR"] = Convert.ToString(dr["ITEM_NBR"]).Trim();
                                dRow["UPC"] = Convert.ToString(dr["UPC"]).Trim();
                                dRow["PRODUCTDESCRIPTION"] = Convert.ToString(dr["PRODUCT_DESCRIPTION"]).Trim();
                                dRow["UPCID"] = Convert.ToString(dr["UPC_ID"]).Trim();

                                ds.Tables[0].Rows.Add(dRow);
                            }


                        }
                    }



                    conn.Close();
                    conn.Dispose();

                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        resp.Success = true;
                        resp.Message = "Success";
                        resp.AppDataSet = ds;

                        return resp;
                    }
                    else
                    {

                        resp.Success = false;
                        resp.Message = "Failure";
                        resp.AppDataSet = ds;

                        return resp;
                    }


                }


            }

            catch (Exception ex)
            {

                GetOracleRecords.ErrorMessage = ex.Message + " Query: " + sql;
                resp.Success = true;
                resp.Message = "Failure";
                resp.AppDataSet = ds;

                return resp;
            }
        }


        public massWebClientResponse DisplayUPCIDinResults(string TempUPCID)
        {
            massWebClientResponse resp = new massWebClientResponse();


            var sql = "";
            sql = sql + "Select ";
            sql = sql + "c.UPC_ID, ";
            sql = sql + "c.UPC,   ";
            sql = sql + "d.PRODUCT_DESCRIPTION,  ";
            sql = sql + "nvl(e.ITEM_DIV,'0') item_div, ";
            sql = sql + "nvl(lpad(e.ITEM_NBR,7,0), '00000000' )  item_nbr    ";

            sql = sql + "from ";

            sql = sql + "UPC_ID_XREF c, ";
            sql = sql + "UPC_ID_DESCRIPTIONS d, ";
            sql = sql + "UPC_ID_ITEM_XREF e ";

            sql = sql + "where ";

            sql = sql + "c.UPC_ID = d.UPC_ID ";
            sql = sql + "and c.UPC_ID = e.UPC_ID (+) "; // mj07312023

            sql = sql + "and c.UPC_ID = :TempUPCID ";
            sql = sql + "order by  c.upc ";



            DataSet ds = new DataSet();

            DataRow dRow;



            try
            {

                ds.Tables.Add(new DataTable("Table"));
                ds.Tables[0].Columns.Add("ITEMDIV", typeof(string));
                ds.Tables[0].Columns.Add("ITEMNBR", typeof(string));
                ds.Tables[0].Columns.Add("UPC", typeof(string));
                ds.Tables[0].Columns.Add("PRODUCTDESCRIPTION", typeof(string));
                ds.Tables[0].Columns.Add("UPCID", typeof(string));

                dRow = ds.Tables[0].NewRow();


                using (OracleConnection conn = Connections.GetConnection(SMI_DATABASES.ITEM_DB))
                using (OracleCommand cmd = new OracleCommand(sql, conn))

                {

                    {
                        cmd.Parameters.Add("TempUPCID", OracleDbType.Varchar2, TempUPCID, ParameterDirection.Input);
                        conn.Open();
                        cmd.CommandType = CommandType.Text;


                        using (OracleDataReader dr = cmd.ExecuteReader())


                        {
                            while (dr.Read())
                            {

                                dRow = ds.Tables[0].NewRow();

                                dRow["ITEMDIV"] = Convert.ToString(dr["ITEM_DIV"]).Trim();
                                dRow["ITEMNBR"] = Convert.ToString(dr["ITEM_NBR"]).Trim();
                                dRow["UPC"] = Convert.ToString(dr["UPC"]).Trim();
                                dRow["PRODUCTDESCRIPTION"] = Convert.ToString(dr["PRODUCT_DESCRIPTION"]).Trim();
                                dRow["UPCID"] = Convert.ToString(dr["UPC_ID"]).Trim();

                                ds.Tables[0].Rows.Add(dRow);
                            }


                        }
                    }



                    conn.Close();
                    conn.Dispose();

                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        resp.Success = true;
                        resp.Message = "Success";
                        resp.AppDataSet = ds;

                        return resp;
                    }
                    else
                    {

                        resp.Success = false;
                        resp.Message = "Failure";
                        resp.AppDataSet = ds;

                        return resp;
                    }


                }


            }

            catch (Exception ex)
            {

                GetOracleRecords.ErrorMessage = ex.Message + " Query: " + sql;
                resp.Success = true;
                resp.Message = "Failure";
                resp.AppDataSet = ds;

                return resp;
            }
        }

        public massWebClientResponse SearchUPC(string Search)
        {
            massWebClientResponse resp = new massWebClientResponse();


            var sql = "";
            sql = sql + "Select ";
            sql = sql + "c.UPC_ID, ";
            sql = sql + "c.UPC,   ";
            sql = sql + "d.PRODUCT_DESCRIPTION,  ";
            sql = sql + "nvl(e.ITEM_DIV,'0') item_div, ";
            sql = sql + "nvl(lpad(e.ITEM_NBR,7,0), '00000000' )  item_nbr    ";

            sql = sql + "from ";

            sql = sql + "UPC_ID_XREF c, ";
            sql = sql + "UPC_ID_DESCRIPTIONS d, ";
            sql = sql + "UPC_ID_ITEM_XREF e ";

            sql = sql + "where ";
            sql = sql + "c.UPC_ID = d.UPC_ID ";
            sql = sql + "and c.UPC_ID = e.UPC_ID (+) "; // mj07312023


            sql = sql + "and upper(d.PRODUCT_DESCRIPTION) like '%' || :Search || '%' ";
            sql = sql + "order by  c.upc ";



            DataSet ds = new DataSet();

            DataRow dRow;



            try
            {

                ds.Tables.Add(new DataTable("Table"));
                ds.Tables[0].Columns.Add("ITEMDIV", typeof(string));
                ds.Tables[0].Columns.Add("ITEMNBR", typeof(string));
                ds.Tables[0].Columns.Add("UPC", typeof(string));
                ds.Tables[0].Columns.Add("PRODUCTDESCRIPTION", typeof(string));
                ds.Tables[0].Columns.Add("UPCID", typeof(string));

                dRow = ds.Tables[0].NewRow();


                using (OracleConnection conn = Connections.GetConnection(SMI_DATABASES.ITEM_DB))
                using (OracleCommand cmd = new OracleCommand(sql, conn))

                {

                    {
                        cmd.Parameters.Add("Search", OracleDbType.Varchar2, Search.ToUpper(), ParameterDirection.Input);
                        conn.Open();
                        cmd.CommandType = CommandType.Text;


                        using (OracleDataReader dr = cmd.ExecuteReader())


                        {
                            while (dr.Read())
                            {

                                dRow = ds.Tables[0].NewRow();

                                dRow["ITEMDIV"] = Convert.ToString(dr["ITEM_DIV"]).Trim();
                                dRow["ITEMNBR"] = Convert.ToString(dr["ITEM_NBR"]).Trim();
                                dRow["UPC"] = Convert.ToString(dr["UPC"]).Trim();
                                dRow["PRODUCTDESCRIPTION"] = Convert.ToString(dr["PRODUCT_DESCRIPTION"]).Trim();
                                dRow["UPCID"] = Convert.ToString(dr["UPC_ID"]).Trim();

                                ds.Tables[0].Rows.Add(dRow);
                            }


                        }
                    }



                    conn.Close();
                    conn.Dispose();

                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        resp.Success = true;
                        resp.Message = "Success";
                        resp.AppDataSet = ds;

                        return resp;
                    }
                    else
                    {

                        resp.Success = false;
                        resp.Message = "Failure";
                        resp.AppDataSet = ds;

                        return resp;
                    }


                }


            }

            catch (Exception ex)
            {

                GetOracleRecords.ErrorMessage = ex.Message + " Query: " + sql;
                resp.Success = true;
                resp.Message = "Failure";
                resp.AppDataSet = ds;

                return resp;
            }
        }

        public massWebClientResponse GetClusterNbr(string CLUSTER_NAME)
        {
            massWebClientResponse resp = new massWebClientResponse();

            var sql = "";

            DataSet ds = new DataSet();

            DataRow dRow;

            ds.Tables.Add(new DataTable("Table"));
            ds.Tables[0].Columns.Add("CLUSTERNBR", typeof(string));
            dRow = ds.Tables[0].NewRow();

            try
            {



                sql = sql + "Select cluster_nbr ";
                sql = sql + "from UPC_CLUSTER  ";
                sql = sql + "where CLUSTER_NAME = :CLUSTER_NAME ";




                using (OracleConnection conn = Connections.GetConnection(SMI_DATABASES.ITEM_DB))
                using (OracleCommand cmd = new OracleCommand(sql, conn))

                {

                    {

                        cmd.Parameters.Add("CLUSTER_NAME", OracleDbType.Varchar2, CLUSTER_NAME, ParameterDirection.Input);
                        conn.Open();
                        cmd.CommandType = CommandType.Text;


                        using (OracleDataReader dr = cmd.ExecuteReader())


                        {
                            while (dr.Read())
                            {

                                dRow = ds.Tables[0].NewRow();


                                string Mike= Convert.ToString(dr["CLUSTER_NBR"]).Trim(); 

                                dRow["CLUSTERNBR"] = Convert.ToString(dr["CLUSTER_NBR"]).Trim();

                                ds.Tables[0].Rows.Add(dRow);



                            }


                        }
                    }



                    conn.Close();
                    conn.Dispose();
                  


                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        resp.Success = true;
                        resp.Message = "Success";
                        resp.AppDataSet = ds;

                        return resp;
                    }
                    else
                    {

                        resp.Success = false;
                        resp.Message = "Failure";
                        resp.AppDataSet = ds;

                        return resp;
                    }


                }


            }

            catch (Exception ex)
            {

                GetOracleRecords.ErrorMessage = ex.Message + " Query: " + sql;
                resp.Success = true;
                resp.Message = "Failure";
                resp.AppDataSet = ds;

                return resp;
            }
        }

        public massWebClientResponse DisplayPendingRequests()
        {
            massWebClientResponse resp = new massWebClientResponse();

            var Sql = "";

            DataSet ds = new DataSet();

            DataRow dRow;

            ds.Tables.Add(new DataTable("Table"));
            ds.Tables[0].Columns.Add("ITEMDIV", typeof(string));
            ds.Tables[0].Columns.Add("ITEMNBR", typeof(string));
            ds.Tables[0].Columns.Add("UPC", typeof(string));
            ds.Tables[0].Columns.Add("UPCID", typeof(string));
            ds.Tables[0].Columns.Add("PRODUCTDESCRIPTION", typeof(string));
            ds.Tables[0].Columns.Add("SECTION", typeof(string));
            ds.Tables[0].Columns.Add("UPDATEID", typeof(string));
            ds.Tables[0].Columns.Add("OLDUPCSTATUS", typeof(string));
            ds.Tables[0].Columns.Add("UPDATEDATE", typeof(string));

            dRow = ds.Tables[0].NewRow();

            try
            {



                Sql = " SELECT ";
                Sql = Sql + "   a.item_nbr,";
                Sql = Sql + "   a.item_div,";
                Sql = Sql + "   b.upc,";
                Sql = Sql + "   b.upc_id,";
                Sql = Sql + "   c.product_description,";
                Sql = Sql + "   d.section,";
                Sql = Sql + "   d.department,";
                Sql = Sql + "   e.update_id,";
                Sql = Sql + "   e.old_upc_status,";
                Sql = Sql + "   e.update_date";
                Sql = Sql + " FROM ";
                Sql = Sql + "   improd.upc_id_item_xref a,";
                Sql = Sql + "   improd.upc_id_xref b,";
                Sql = Sql + "   improd.upc_id_descriptions c,";
                Sql = Sql + "   improd.upc_id_master d,";
                Sql = Sql + "   workspace.pending_purge e";
                Sql = Sql + " WHERE ";
                Sql = Sql + "   e.upc_id = a.upc_id(+) AND";
                Sql = Sql + "   e.upc_id = b.upc_id AND";
                Sql = Sql + "   e.upc_id = c.upc_id AND";
                Sql = Sql + "   e.upc_id = d.upc_id";



                using (OracleConnection conn = Connections.GetConnection(SMI_DATABASES.ITEM_DB))
                using (OracleCommand cmd = new OracleCommand(Sql, conn))

                {

                    {
                      //  cmd.Parameters.Add("CLUSTER_NBR", OracleDbType.Varchar2, CLUSTER_NBR, ParameterDirection.Input);
                        conn.Open();
                        cmd.CommandType = CommandType.Text;


                        using (OracleDataReader dr = cmd.ExecuteReader())


                        {
                            while (dr.Read())
                            {

                                dRow = ds.Tables[0].NewRow();

                                dRow["ITEMDIV"] = Convert.ToString(dr["item_div"]).Trim();
                                dRow["ITEMNBR"] = Convert.ToString(dr["item_nbr"]).Trim();
                                dRow["UPC"] = Convert.ToString(dr["upc"]).Trim();
                                dRow["UPCID"] = Convert.ToString(dr["upc_id"]).Trim();

                                dRow["PRODUCTDESCRIPTION"] = Convert.ToString(dr["product_description"]).Trim();
                                dRow["SECTION"] = Convert.ToString(dr["section"]).Trim();
                                dRow["UPDATEID"] = Convert.ToString(dr["update_id"]).Trim();
                                dRow["OLDUPCSTATUS"] = Convert.ToString(dr["old_upc_status"]).Trim();
                                dRow["UPDATEDATE"] = Convert.ToString(dr["update_date"]).Trim();


                                ds.Tables[0].Rows.Add(dRow);



                            }


                        }
                    }



                    conn.Close();
                    conn.Dispose();

                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        resp.Success = true;
                        resp.Message = "Success";
                        resp.AppDataSet = ds;

                        return resp;
                    }
                    else
                    {

                        resp.Success = false;
                        resp.Message = "Failure";
                        resp.AppDataSet = ds;

                        return resp;
                    }


                }


            }

            catch (Exception ex)
            {

                GetOracleRecords.ErrorMessage = ex.Message + " Query: " + Sql;
                resp.Success = true;
                resp.Message = "Failure";
                resp.AppDataSet = ds;

                return resp;
            }
        }

        public massWebClientResponse GetClusterUPCs(string CLUSTER_NBR)
        {
            massWebClientResponse resp = new massWebClientResponse();

            var sql = "";

            DataSet ds = new DataSet();

            DataRow dRow;

            ds.Tables.Add(new DataTable("Table"));
            ds.Tables[0].Columns.Add("ITEMDIV", typeof(string));
            ds.Tables[0].Columns.Add("ITEMNBR", typeof(string));
            ds.Tables[0].Columns.Add("UPC", typeof(string));
            ds.Tables[0].Columns.Add("UPCID", typeof(string));
            ds.Tables[0].Columns.Add("PRODUCTDESCRIPTION", typeof(string));

            dRow = ds.Tables[0].NewRow();

            try
            {



                sql = sql + "Select a.cluster_nbr, ";
                sql = sql + "b.UPC_ID,  ";
                sql = sql + "c.UPC,  ";
                sql = sql + "d.PRODUCT_DESCRIPTION, ";
                sql = sql + "nvl(e.ITEM_DIV,'0') item_div, ";
                sql = sql + "nvl(lpad(e.ITEM_NBR,7,0), '00000000' )  item_nbr    ";



                sql = sql + "from UPC_CLUSTER a, ";
                sql = sql + "UPC_CLUSTER_XREF b,  ";
                sql = sql + "UPC_ID_XREF c, ";
                sql = sql + "UPC_ID_DESCRIPTIONS d, ";
                sql = sql + "UPC_ID_ITEM_XREF e ";

                sql = sql + "where a.cluster_nbr = :CLUSTER_NBR ";
                sql = sql + "and a.cluster_nbr = b.CLUSTER_NBR ";
                sql = sql + "and b.UPC_ID = c.UPC_ID ";
                sql = sql + "and b.UPC_ID = d.UPC_ID ";
                sql = sql + "and b.UPC_ID = e.UPC_ID (+) "; // mj07312023
                sql = sql + "order by a.update_id, a.cluster_nbr, c.upc ";




                using (OracleConnection conn = Connections.GetConnection(SMI_DATABASES.ITEM_DB))
                using (OracleCommand cmd = new OracleCommand(sql, conn))

                {

                    {
                        cmd.Parameters.Add("CLUSTER_NBR", OracleDbType.Varchar2, CLUSTER_NBR, ParameterDirection.Input);
                        conn.Open();
                        cmd.CommandType = CommandType.Text;


                        using (OracleDataReader dr = cmd.ExecuteReader())


                        {
                            while (dr.Read())
                            {

                                dRow = ds.Tables[0].NewRow();

                                dRow["ITEMDIV"] = Convert.ToString(dr["ITEM_DIV"]).Trim();
                                dRow["ITEMNBR"] = Convert.ToString(dr["ITEM_NBR"]).Trim();
                                dRow["UPC"] = Convert.ToString(dr["UPC"]).Trim();
                                dRow["UPCID"] = Convert.ToString(dr["UPC_ID"]).Trim();

                                dRow["PRODUCTDESCRIPTION"] = Convert.ToString(dr["PRODUCT_DESCRIPTION"]).Trim();


                                ds.Tables[0].Rows.Add(dRow);



                            }


                        }
                    }



                    conn.Close();
                    conn.Dispose();

                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        resp.Success = true;
                        resp.Message = "Success";
                        resp.AppDataSet = ds;

                        return resp;
                    }
                    else
                    {

                        resp.Success = false;
                        resp.Message = "Failure";
                        resp.AppDataSet = ds;

                        return resp;
                    }


                }


            }

            catch (Exception ex)
            {

                GetOracleRecords.ErrorMessage = ex.Message + " Query: " + sql;
                resp.Success = true;
                resp.Message = "Failure";
                resp.AppDataSet = ds;

                return resp;
            }
        }


    }

    
}
