using System;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDILReader;

namespace $rootnamespace$
{
    [TestClass]
    public class $safeitemrootname$ : BaseTest
    {
        [TestInitialize()]
        public override void SetupEnviroment()
        {
            Globals.LoadOpCodes();
            _configuration = new ScreenLib.ScreenConfiguration(_xrScreen, _assemblyScripting);
            var sqlCeConnection = Factory.CreateConnection(ConnStr);
            //SqlCeCommand command;
            String strSql;
            if (MSTest.Properties.Settings.Default.RecreateDB)
            {
                try
                {
                    if (File.Exists(_cMyTestFieldserviceSdf))
                    {
                        File.Delete(_cMyTestFieldserviceSdf);
                    }
                    SqlCeEngine engine = Factory.CreateSqlEngine(ConnStr);
                    engine.CreateDatabase();
                    engine.Dispose();
                    using (StreamReader objReader = new StreamReader(_cMyTestImportTxt))
                    {
                        while (!objReader.EndOfStream)
                        {
                            strSql = objReader.ReadLine();
                            var command = Factory.CreateCommand(
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
                                throw Factory.CreateException(String.Format("Fout tijdens uitlezen gegegevens.\r\nQuery:\r\n{0}", strSql), objException);
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

    }
}
