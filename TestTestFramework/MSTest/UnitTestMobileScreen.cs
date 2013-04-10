using System;
using System.Collections;
using System.Data;
using System.Data.SqlServerCe;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDILReader;

namespace MSTest
{
    [TestClass]
    public class UnitTestMobileScreen: BaseTest
    {
        [TestInitialize()]
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
            using (StreamReader objReader = new StreamReader(@"C:\Mobility\Solutions\Tensing FSS-dev\9 Tests\FSS Test Framework\MSTest\SQL\CleanUpMaterialMutation.sqlce"))
            {
                strSql = objReader.ReadToEnd();
                command = new SqlCeCommand(
                    strSql,
                    sqlCeConnection
                    );
            }
            try
            {
                sqlCeConnection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception objException)
            {
                throw new Exception(String.Format("Fout tijdens opschonen gegegevens.\r\nQuery:\r\n{0}", strSql), objException);
            }
            finally
            {
                if (sqlCeConnection != null) sqlCeConnection.Close();
            }
            
            using (StreamReader objReader = new StreamReader(@"C:\Mobility\Solutions\Tensing FSS-dev\9 Tests\FSS Test Framework\MSTest\SQL\CleanUpCheckMultipleStockExistingMaterialnumber.sqlce"))
            {
                strSql = objReader.ReadToEnd();
                command = new SqlCeCommand(
                    strSql,
                    sqlCeConnection
                    );
            }
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
            
            using (StreamReader objReader = new StreamReader(@"C:\Mobility\Solutions\Tensing FSS-dev\9 Tests\FSS Test Framework\MSTest\SQL\SetupEnviromentCheckMultipleStockExistingMaterialnumber.sqlce"))
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

        public void CleanUpPDADatabase()
        {
            SqlCeConnection sqlCeConnection = new SqlCeConnection(ConnStr);
            SqlCeCommand command;
            string strSql;
            using (StreamReader objReader = new StreamReader(@"C:\Mobility\Solutions\Tensing FSS-dev\9 Tests\FSS Test Framework\MSTest\SQL\CleanUpMaterialMutation.sqlce"))
            {
                strSql = objReader.ReadToEnd();
                command = new SqlCeCommand(
                    strSql,
                    sqlCeConnection
                    );
            }
            try
            {
                sqlCeConnection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception objException)
            {
                throw new Exception("Fout tijdens opschonen gegegevens.", objException);
            }
            finally
            {
                if (sqlCeConnection != null) sqlCeConnection.Close();
            }
            
            using (StreamReader objReader = new StreamReader(@"C:\Mobility\Solutions\Tensing FSS-dev\9 Tests\FSS Test Framework\MSTest\SQL\CleanUpCheckMultipleStockExistingMaterialnumber.sqlce"))
            {
                strSql = objReader.ReadToEnd();
                command = new SqlCeCommand(
                    strSql,
                    sqlCeConnection
                    );
            }
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

        [TestMethod]
        public void TestFromWarehouseQuery()
        {
            var sqlCeConnection = new SqlCeConnection(ConnStr);
            SqlCeCommand command;
            
            var objParams = new Hashtable();
            objParams.Add(MSTestConst.UserIdKey, MSTestConst.UserIdValueNL);
            objParams.Add(MSTestConst.LocaleIdKey, MSTestConst.LocaleidValueNL);
            objParams.Add(MSTestConst.OrganisationIdKey, MSTestConst.OrganisationIdNL);
            objParams.Add(MSTestConst.CTw, MSTestConst.CTwValue /*1616776867*/);

            string strSql = _configuration.GetQueryFromComponent(96, "cmbFromWarehouse", objParams);
            Assert.IsTrue(strSql.Contains("fv_translation"));
            command = new SqlCeCommand(strSql, sqlCeConnection);

            try
            {
                sqlCeConnection.Open();
                IDataReader dr = command.ExecuteReader();
                Assert.AreEqual(true, dr.Read());
                Assert.AreEqual("Klant magazijn", dr.GetString(1));
                Assert.AreEqual(true, dr.Read());
                Assert.AreEqual("CityStock", dr.GetString(1));
                Assert.AreEqual(true, dr.Read());
                Assert.AreEqual("Wagenvoorraad MvA", dr.GetString(1));
                Assert.AreEqual(false, dr.Read());
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
        [TestMethod]
        public void TestSearchMaterialQuery()
        {
            var sqlCeConnection = new SqlCeConnection(ConnStr);
            SqlCeCommand command;
            
            var objParams = new Hashtable();
            objParams.Add(MSTestConst.UserIdKey, MSTestConst.CTuseridValueBE);
            objParams.Add("sub.txtArtikelnummer", "50098");
            objParams.Add("sub.txtArtikelnaam_s", "");
            objParams.Add("flow.zoek","1");
            objParams.Add("sub.cmbWarehouse", "null");
            objParams.Add(MSTestConst.LocaleIdKey, MSTestConst.LocaleidValueNL);
            objParams.Add(MSTestConst.OrganisationIdKey, MSTestConst.OrganisationIdNL);
            objParams.Add(MSTestConst.CTmat, MSTestConst.CTmatValue /*1424776183*/);
            string strSql = _configuration.GetQueryFromComponent(27, "grResultaatArtikelen", objParams);
            Assert.IsTrue(strSql.Contains("fv_translation"));
            command = new SqlCeCommand(strSql, sqlCeConnection);

            try
            {
                //Deze query geeft de volgende data terug
                sqlCeConnection.Open();
                IDataReader dr = command.ExecuteReader();
                Assert.AreEqual(false, dr.Read());
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

        [TestMethod]
        public void TestSearchMaterialWarehouseQuery()
        {
            var sqlCeConnection = new SqlCeConnection(ConnStr);
            SqlCeCommand command;
            
            var objParams = new Hashtable();
            objParams.Add(MSTestConst.UserIdKey, MSTestConst.UserIdValueNL);
            objParams.Add("sub.txtArtikelnummer", "12");
            objParams.Add("sub.txtArtikelnaam_s", "");
            objParams.Add(MSTestConst.CTzoek, MSTestConst.CTzoekValue /*"1"*/);
            objParams.Add("sub.cmbWarehouse", "16");
            objParams.Add(MSTestConst.LocaleIdKey, MSTestConst.LocaleidValueNL);
            objParams.Add(MSTestConst.OrganisationIdKey, MSTestConst.OrganisationIdNL);
            objParams.Add(MSTestConst.CTmat, MSTestConst.CTmatValue /*1424776183*/);
            string strSql = _configuration.GetQueryFromComponent(27, "grResultaatArtikelen", objParams);
            Assert.IsTrue(strSql.Contains("fv_translation"));
            command = new SqlCeCommand(strSql, sqlCeConnection);

            try
            {
                //Deze query geeft de volgende data terug
                sqlCeConnection.Open();
                IDataReader dr = command.ExecuteReader();
                Assert.AreEqual(false, dr.Read());
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