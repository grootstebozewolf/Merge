using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.Properties;

namespace MSTest
{
    [TestClass]
    public class UnitTestPDADatabase: BaseTest
    {
        [TestInitialize()]
        public override void SetupEnviroment()
        {
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
            string strSql;
            SqlCeCommand command;
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
        public void DoesDatabaseConnectionWork()
        {
            SqlCeConnection sqlCeConnection = new SqlCeConnection(ConnStr);
            SqlCeCommand objCheckScalar;
            
            string strSql;
            using (StreamReader objReader = new StreamReader(@"C:\Mobility\Solutions\Tensing FSS-dev\9 Tests\FSS Test Framework\MSTest\SQL\CheckScalar.sqlce"))
            {
                strSql = objReader.ReadToEnd();
                Assert.IsNotNull(strSql, string.Format("SQL file is filled with {0}.", strSql));
                objCheckScalar = new SqlCeCommand(
                    strSql,
                    sqlCeConnection
                    );
            }
            try
            {
                sqlCeConnection.Open();
                int intScalarResult = ((int)objCheckScalar.ExecuteScalar());
                Assert.IsTrue(intScalarResult == 1, string.Format("Expected 1 but got {0}!", intScalarResult));
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
        public void DoesInsertMaterialMutationWork()
        {
            SqlCeConnection sqlCeConnection = new SqlCeConnection(ConnStr);
            SqlCeCommand objInsertMaterialMutation;
            
            string strSql;
            using (StreamReader objReader = new StreamReader(@"C:\Mobility\Solutions\Tensing FSS-dev\9 Tests\FSS Test Framework\MSTest\SQL\InsertMaterialMutation.sqlce"))
            {
                strSql = objReader.ReadToEnd();
                objInsertMaterialMutation = new SqlCeCommand(
                    strSql,
                    sqlCeConnection
                    );
            }
            try
            {
                sqlCeConnection.Open();
                int rowsAffected = objInsertMaterialMutation.ExecuteNonQuery();
                Assert.IsTrue(rowsAffected == 1, string.Format("Expected 1 but got {0}!", rowsAffected));
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
        public void CheckNoStockExistingMaterialNumber()
        {
            SqlCeConnection sqlCeConnection = new SqlCeConnection(ConnStr);
            SqlCeCommand objInsertMaterialMutation;
            
            string strSql;
            using (StreamReader objReader = new StreamReader(@"C:\Mobility\Solutions\Tensing FSS-dev\9 Tests\FSS Test Framework\MSTest\SQL\CheckNoStockExistingMaterialnumber.sqlce"))
            {
                strSql = objReader.ReadToEnd();
                objInsertMaterialMutation = new SqlCeCommand(
                    strSql,
                    sqlCeConnection
                    );
            }
            try
            {
                sqlCeConnection.Open();
                IDataReader objReader = objInsertMaterialMutation.ExecuteReader();
                Assert.IsTrue(objReader.Read());
                Assert.AreEqual(25, objReader.GetInt32(0), "Materiaal met external_id 6059 zou id 25 moeten hebben.");
                Assert.IsTrue(objReader.IsDBNull(1), "Stock is niet gevuld, dus zou niet gevonden moeten worden.");
                Assert.IsFalse(objReader.Read());
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
        public void CheckMultipleStockExistingMaterialnumber()
        {

            SqlCeConnection sqlCeConnection = new SqlCeConnection(ConnStr);
            SqlCeCommand command;
            
            string strSql;
            using (StreamReader objReader = new StreamReader(@"C:\Mobility\Solutions\Tensing FSS-dev\9 Tests\FSS Test Framework\MSTest\SQL\CheckMultipleStockExistingMaterialnumber.sqlce"))
            {
                strSql = objReader.ReadToEnd();
                command = new SqlCeCommand(
                    strSql,
                    sqlCeConnection
                    );
            }
            try
            {
                //Deze geeft als uitvoer: 
                //id    id    quantity    warehouse_id 
                //1     47    1.00        4
                //1     58    1.00        3

                sqlCeConnection.Open();
                IDataReader objReader = command.ExecuteReader();
                Assert.IsTrue(objReader.Read());
                Assert.AreEqual(1, objReader.GetInt32(0), "Materiaal met external_id 6035 zou id 47 moeten hebben.");
                Assert.IsFalse(objReader.IsDBNull(1), "Stock is gevuld, dus zou niet gevonden moeten worden.");
                Assert.AreEqual(49,objReader.GetInt32(1));
                Assert.AreEqual((decimal)1.00, objReader.GetDecimal(2));
                Assert.AreEqual(4, objReader.GetInt32(3));
                Assert.IsTrue(objReader.Read());
                Assert.AreEqual(1, objReader.GetInt32(0), "Materiaal met external_id 6035 zou id 58 moeten hebben.");
                Assert.IsFalse(objReader.IsDBNull(1), "Stock is gevuld, dus zou niet gevonden moeten worden.");
                Assert.AreEqual(60, objReader.GetInt32(1));
                Assert.AreEqual((decimal)1.00, objReader.GetDecimal(2));
                Assert.AreEqual(3, objReader.GetInt32(3));
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
        public void CheckStockFromId()
        {

            SqlCeConnection sqlCeConnection = new SqlCeConnection(ConnStr);
            SqlCeCommand command;
            
            string strSql;
            using (StreamReader objReader = new StreamReader(@"C:\Mobility\Solutions\Tensing FSS-dev\9 Tests\FSS Test Framework\MSTest\SQL\CheckStockFromId.sqlce"))
            {
                strSql = objReader.ReadToEnd();
                command = new SqlCeCommand(
                    strSql,
                    sqlCeConnection
                    );
            }
            try
            {
                //Deze geeft als uitvoer: 
                //id    id    quantity    warehouse_id  isserialnumberrequired
                //18	42	  4.00	      99            0 
                
                sqlCeConnection.Open();
                IDataReader objReader = command.ExecuteReader();
                Assert.IsTrue(objReader.Read());
                Assert.AreEqual(18, objReader.GetInt32(0), "Materiaal met external_id 6052 zou id 18 moeten hebben.");
                Assert.IsFalse(objReader.IsDBNull(1), "Stock is gevuld, dus zou niet gevonden moeten worden.");
                Assert.AreEqual(42, objReader.GetInt32(1));
                Assert.AreEqual((decimal)4.00, objReader.GetDecimal(2));
                Assert.IsFalse(objReader.Read());
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
        public void DoesUpdatetMaterialMutationWork()
        {
            SqlCeConnection sqlCeConnection = new SqlCeConnection(ConnStr);
            SqlCeCommand objInsertMaterialMutation;
            
            string strSql;
            using (StreamReader objReader = new StreamReader(@"C:\Mobility\Solutions\Tensing FSS-dev\9 Tests\FSS Test Framework\MSTest\SQL\SetupEnviromentDoesUpdatetMaterialMutationWork.sqlce"))
            {
                strSql = objReader.ReadToEnd();
                objInsertMaterialMutation = new SqlCeCommand(
                    strSql,
                    sqlCeConnection
                    );
            }
            try
            {
                sqlCeConnection.Open();
                int rowsAffected = objInsertMaterialMutation.ExecuteNonQuery();
                Assert.IsTrue(rowsAffected == 1, string.Format("Expected 1 but got {0}!", rowsAffected));
            }
            catch (Exception objException)
            {
                throw new Exception("Fout tijdens opzetten omgeving gegegevens.", objException);
            }
            finally
            {
                if (sqlCeConnection != null) sqlCeConnection.Close();
            }
            using (StreamReader objReader = new StreamReader(@"C:\Mobility\Solutions\Tensing FSS-dev\9 Tests\FSS Test Framework\MSTest\SQL\UpdateMaterialMutation.sqlce"))
            {
                strSql = objReader.ReadToEnd();
                objInsertMaterialMutation = new SqlCeCommand(
                    strSql,
                    sqlCeConnection
                    );
            }
            try
            {
                sqlCeConnection.Open();
                int rowsAffected = objInsertMaterialMutation.ExecuteNonQuery();
                Assert.IsTrue(rowsAffected == 1, string.Format("Expected 1 but got {0}!", rowsAffected));
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
