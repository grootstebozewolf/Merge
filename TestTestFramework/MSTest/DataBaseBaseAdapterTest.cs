using Moq;
using Tensing.FieldVision.Scripting.GeneratedPlugin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;

namespace MSTest
{
    
    
    /// <summary>
    ///This is a test class for DataBaseBaseAdapterTest and is intended
    ///to contain all DataBaseBaseAdapterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DataBaseBaseAdapterTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for ExecuteSelectSQL
        ///</summary>
        //[TestMethod()]
        public void ExecuteSelectSQLTest()
        {
            object cnDb = new Mock<FieldCore.DataBase>().Object;
            DataBaseBaseAdapter target = new DataBaseBaseAdapter(cnDb);
            string strSql = string.Empty;
            IDataReader expected = null;
            IDataReader actual;
            actual = target.ExecuteSelectSQL(strSql);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CanAdapt
        ///</summary>
        [TestMethod()]
        public void CanAdaptTest()
        {
            object cnDb = new Mock<FieldCore.DataBase>().Object;
            DataBaseBaseAdapter target = new DataBaseBaseAdapter(cnDb);
            Type type = typeof(FieldCore.DataBase);
            bool expected = true;
            bool actual;
            actual = target.CanAdapt(type);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CanAdapt
        ///</summary>
        [TestMethod()]
        public void CanAdaptTestNotSupportedType()
        {
            object cnDb = new Mock<FieldCore.DataBase>().Object;
            DataBaseBaseAdapter target = new DataBaseBaseAdapter(cnDb);
            Type type = typeof(String);
            bool expected = false;
            bool actual;
            actual = target.CanAdapt(type);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for DataBaseBaseAdapter Constructor
        ///</summary>
        [TestMethod()]
        public void DataBaseBaseAdapterConstructorSmokeTest()
        {
            object cnDb = new Mock<FieldCore.DataBase>().Object;
            DataBaseBaseAdapter target = new DataBaseBaseAdapter(cnDb);
        }
    }
}
