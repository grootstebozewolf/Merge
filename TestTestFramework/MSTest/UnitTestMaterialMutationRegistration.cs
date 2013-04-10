using System;
using System.Collections;
using System.Data;
using System.Data.SqlServerCe;
using System.Globalization;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDILReader;
using Tensing.FieldVision.Scripting.GeneratedPlugin;

namespace MSTest
{
    [TestClass]
    public class UnitTestMaterialMutationRegistration: BaseTest
    {
        [TestMethod]
        public void SmokeTestMethodCreateOrderAfterRegistration()
        {
            var sqlCeConnection = new SqlCeConnection(ConnStr);
            SqlCeCommand command;

            var objParams = new Hashtable();
            objParams.Add(MSTestConst.ActivityIdKey,Guid.Empty);
            objParams.Add(MSTestConst.ServiceorderId, Guid.Empty);
            objParams.Add("app.userid",8);
            objParams.Add("flow.matid",1);
            objParams.Add("form.txtQuantity",1);
            objParams.Add("form.cmbFromWarehouse", "");
            objParams.Add("app.verifydt", DateTime.Now.ToString(CultureInfo.InvariantCulture));
            objParams.Add(MSTestConst.OrganisationIdKey, MSTestConst.OrganisationIdNL);

            string strSql = new ScriptingSystem.MockSql(SqlLib.CreateOrderAfterRegistration).ParseQuery(objParams);
            command = new SqlCeCommand(strSql, sqlCeConnection);

            try
            {
                sqlCeConnection.Open();
                int numresults = command.ExecuteNonQuery();
                Assert.AreEqual(1, numresults);
            }
            catch (Exception objException)
            {
                throw new Exception(String.Format("Fout tijdens uitlezen gegegevens.\r\nQuery:\r\n{0}", strSql),
                                    objException);
            }
            finally
            {
                if (sqlCeConnection != null) sqlCeConnection.Close();
            }
        }

        #region Overrides of BaseTest


        [TestInitialize]
        public override void SetupEnviroment()
        {
            Globals.LoadOpCodes();
            _configuration = new ScreenLib.ScreenConfiguration(_xrScreen, _assemblyScripting);
            SqlCeConnection sqlCeConnection = new SqlCeConnection(ConnStr);
            SqlCeCommand command;
            string strSql;
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
                                throw new Exception(
                                    String.Format("Fout tijdens uitlezen gegegevens.\r\nQuery:\r\n{0}", strSql),
                                    objException);
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
        #endregion

        [TestCleanup]
        public void CleanUp()
        {
            SqlCeConnection sqlCeConnection = new SqlCeConnection(ConnStr);
            SqlCeCommand command;
            string strSql;
            using (StreamReader objReader = new StreamReader(@"C:\Mobility\Solutions\Tensing FSS-dev\9 Tests\FSS Test Framework\MSTest\SQL\CleanUpMaterialMutation.sqlce"))
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
                        throw new Exception(
                            String.Format("Fout tijdens uitlezen gegegevens.\r\nQuery:\r\n{0}", strSql),
                            objException);
                    }
                    finally
                    {
                        if (sqlCeConnection != null) sqlCeConnection.Close();
                    }
                }
            }
        }
    }
}
