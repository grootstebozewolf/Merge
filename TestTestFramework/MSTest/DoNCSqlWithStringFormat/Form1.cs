using System;
using System.Collections;
using System.Data;
using System.Data.SqlServerCe;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDILReader;

namespace MSTest
{

    //Form name: Form1
    [TestClass]
    public class Form1 : BaseTest

    {
        [TestInitialize()]
        public override void SetupEnviroment()
        {
            Globals.LoadOpCodes();
            _configuration = new ScreenLib.ScreenConfiguration(_xrScreen, _assemblyScripting);
            SqlCeConnection sqlCeConnection = new SqlCeConnection(ConnStr);
            SqlCeCommand command;
            String strSql;
            if (MSTest.Properties.Settings.Default.RecreateDB)
            {
                try
                {
                    if (File.Exists(_cMyTestFieldserviceSdf))
                    {
                        File.Delete(_cMyTestFieldserviceSdf);
                    }
                    SqlCeEngine engine = new SqlCeEngine(ConnStr);
                    engine.CreateDatabase();
                    engine.Dispose();
                    using (StreamReader objReader = new StreamReader(_cMyTestImportTxt))
                    {
                        while (!objReader.EndOfStream)
                        {
                            strSql = objReader.ReadLine();
                            command = new SqlCeCommand(
                                strSql,
                                sqlCeConnection
                                );
                            try
                            {
                                sqlCeConnection.Open();
                                int numResults = command.ExecuteNonQuery();
                            }
                            catch (Exception objException)
                            {
                                throw new Exception(String.Format("Fout tijdens uitlezen gegegevens.\r\nQuery:\r\n{0}", strSql), objException);
                            }
                            finally
                            {
                                if (sqlCeConnection != null) sqlCeConnection.Close();
                            }
                        }
                    }
                }
                catch (System.IO.IOException)
                {

                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        
        // Scripting name: Fo1_Fr_T_C
        [TestMethod]
        public void TestFormFo1_Fr_T_C_SqlInScripting1()
        {
            var sqlCeConnection = new SqlCeConnection(ConnStr);
            SqlCeCommand command;
            //MSBuild always nest testresults three levels deep. (TestResults\UniqueFolder\Out)
            var objParams = new Hashtable();
            //objParams.Add("app.userid", 7);
            
            string strSql = _configuration.GetQueryInScriptFromForm(1, "Fo1_Fr_T_C", objParams, 1);


            command = new SqlCeCommand(strSql, sqlCeConnection);

            try
            {
                //TODO: Expected result for query
                /*SELECT * FROM fv_activity WHERE activitystatustype_id = {0}*/
                sqlCeConnection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception objException)
            {
                throw new Exception(String.Format("Fout tijdens uitlezen gegegevens.\r\nQuery:\r\n{0}", strSql), objException);
            }
            finally
            {
                if (sqlCeConnection != null) sqlCeConnection.Close();
            }
        }

    }
}
