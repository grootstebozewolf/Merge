using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data.SqlServerCe;
using System.Diagnostics;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tensing.FieldVision.Scripting.GeneratedPlugin;

namespace MSTest.MWFTest
{
    [TestClass]
    public class MWFFSSTest
    {
        const string ConnStr = @"Data Source=C:\My Test\FSS-dev\FieldService.sdf";
        [TestMethod]
        public void SmokeTestOrganisationStatusUpdate()
        {
            SqlCeConnection sqlCeConnection = new SqlCeConnection(ConnStr);
            SqlCeCommand command;
            ScriptingSystem.SqlCollection = new Collection<ScriptingSystem.MockSql>();
            MWF.Instance.OrganisationStatusUpdate(30);
            Assert.AreEqual(2,ScriptingSystem.SqlCollection.Count);
            var objParams = new Hashtable();
            objParams.Add(MSTestConst.ActivityIdKey, Guid.Empty);
            objParams.Add(MSTestConst.CTverifydt, MSTestConst.CTverifydtValue /*"2011-10-21T09:33:14"*/);
            objParams.Add(MSTestConst.UserIdKey, MSTestConst.UserIdValueNL /*8*/);
            objParams.Add(MSTestConst.OrganisationIdKey, MSTestConst.OrganisationIdNL);
            foreach(ScriptingSystem.MockSql mockSql in ScriptingSystem.SqlCollection)
            {
                command = new SqlCeCommand(
                                mockSql.ParseQuery(objParams),
                                sqlCeConnection
                                );
                try
                {
                    sqlCeConnection.Open();
                    int numResults = command.ExecuteNonQuery();
                }
                catch (Exception objException)
                {
                    throw new Exception("Fout tijdens uitlezen gegegevens.", objException);
                }
                finally
                {
                    if (sqlCeConnection != null) sqlCeConnection.Close();
                }
            }
        }

        [TestMethod]
        public void SmokeTestOrganisationCreateLabour()
        {
            SqlCeConnection sqlCeConnection = new SqlCeConnection(ConnStr);
            SqlCeCommand command;
            ScriptingSystem.SqlCollection = new Collection<ScriptingSystem.MockSql>();
            MWF.Instance.OrganisationCreateLabour(1);
            Assert.AreEqual(1, ScriptingSystem.SqlCollection.Count);
            var objParams = new Hashtable();
            objParams.Add(MSTestConst.ActivityIdKey, Guid.Empty);
            objParams.Add(MSTestConst.CTverifydt, MSTestConst.CTverifydtValue /*"2011-10-21T09:33:14"*/);
            objParams.Add(MSTestConst.UserIdKey, MSTestConst.UserIdValueNL);
            objParams.Add(MSTestConst.OrganisationIdKey, MSTestConst.OrganisationIdNL);
            objParams.Add("app.day_labour_id", Guid.Empty);
            foreach (ScriptingSystem.MockSql mockSql in ScriptingSystem.SqlCollection)
            {
                command = new SqlCeCommand(
                                mockSql.ParseQuery(objParams),
                                sqlCeConnection
                                );
                try
                {
                    sqlCeConnection.Open();
                    int numResults = command.ExecuteNonQuery();
                }
                catch (Exception objException)
                {
                    throw new Exception("Fout tijdens uitlezen gegegevens.", objException);
                }
                finally
                {
                    if (sqlCeConnection != null) sqlCeConnection.Close();
                }
            }
        }

        [TestMethod]
        public void SmokeTestStatusUpdateWithCancelreason()
        {
            SqlCeConnection sqlCeConnection = new SqlCeConnection(ConnStr);
            SqlCeCommand command;
            ScriptingSystem.SqlCollection = new Collection<ScriptingSystem.MockSql>();
            MWF.Instance.StatusUpdateWithCancelreason(30, "1", "Cancelnotes");
            Assert.AreEqual(3, ScriptingSystem.SqlCollection.Count);
            var objParams = new Hashtable();
            objParams.Add(MSTestConst.ActivityIdKey, Guid.Empty);
            objParams.Add(MSTestConst.CTverifydt, MSTestConst.CTverifydtValue /*"2011-10-21T09:33:14"*/);
            objParams.Add(MSTestConst.UserIdKey, MSTestConst.UserIdValueNL);
            objParams.Add(MSTestConst.OrganisationIdKey, MSTestConst.OrganisationIdNL);
            objParams.Add("app.day_labour_id", Guid.Empty);
            foreach (ScriptingSystem.MockSql mockSql in ScriptingSystem.SqlCollection)
            {
                command = new SqlCeCommand(
                                mockSql.ParseQuery(objParams),
                                sqlCeConnection
                                );
                try
                {
                    sqlCeConnection.Open();
                    int numResults = command.ExecuteNonQuery();
                }
                catch (Exception objException)
                {
                    throw new Exception("Fout tijdens uitlezen gegegevens.", objException);
                }
                finally
                {
                    if (sqlCeConnection != null) sqlCeConnection.Close();
                }
            }
        }
    }
}

