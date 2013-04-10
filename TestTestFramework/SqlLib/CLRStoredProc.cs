using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace SqlLib
{
    public class CLRStoredProc
    {
        [Microsoft.SqlServer.Server.SqlProcedure]
        public static void GetShowPlanXML
        (
              SqlString SQL
            , out SqlXml PlanXML
            , SqlString server
            , SqlString database
            , SqlBoolean isIntegratedSecurity
            , SqlString loginIfNotIntegrated
            , SqlString passwordIfNotIntegrated
        )
        {
            //Prep connection
            string strConnectionString;

            if ((bool)isIntegratedSecurity)
            {
                strConnectionString = @"Data Source="
                    + server.ToString()
                    + @";Initial Catalog="
                    + database.ToString()
                    + @";Integrated Security=True";
            }
            else
            {
                strConnectionString = @"data source="
                    + server.ToString()
                    + @";initial catalog="
                    + database.ToString()
                    + @";Persist Security Info=True;User ID="
                    + loginIfNotIntegrated.ToString()
                    + @";Password="
                    + passwordIfNotIntegrated.ToString();
            }

            SqlConnection cn = new SqlConnection(strConnectionString);

            //Set command texts
            SqlCommand cmd_SetShowPlanXml = new SqlCommand("SET SHOWPLAN_XML ON", cn);
            SqlCommand cmd_input = new SqlCommand(SQL.ToString(), cn);

            if (cn.State != ConnectionState.Open)
            {
                cn.Open();
            }

            //Run SET SHOWPLAN_XML ON
            cmd_SetShowPlanXml.ExecuteNonQuery();

            //Run input SQL
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();

            da.SelectCommand = cmd_input;
            ds.Tables.Add(new DataTable("Results"));

            ds.Tables[0].BeginLoadData();
            da.Fill(ds, "Results");
            ds.Tables[0].EndLoadData();

            if (cn.State != ConnectionState.Closed)
            {
                cn.Close();
            }

            //Package XML as output
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            //XML is in 1st Col of 1st Row of 1st Table
            xmlDoc.InnerXml = ds.Tables[0].Rows[0][0].ToString();
            System.Xml.XmlNodeReader xnr = new System.Xml.XmlNodeReader(xmlDoc);
            PlanXML = new SqlXml(xnr);
        }

        static public void CustomerGetProcedure(SqlInt32 customerId)
        {
            using (SqlConnection conn = new SqlConnection("context connection=true"))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM fv_activity 
					WHERE customer_id = @CustomerId";
                cmd.Parameters.AddWithValue("@CustomerID", customerId);
                conn.Open();

                // Execute the comand and send the results to the caller.
                SqlContext.Pipe.ExecuteAndSend(cmd);
            }
        }

    }
}
