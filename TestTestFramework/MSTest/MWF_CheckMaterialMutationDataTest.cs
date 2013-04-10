using Tensing.FieldVision.Scripting.GeneratedPlugin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MSTest
{
    
    
    /// <summary>
    ///This is a test class for MWF_CheckMaterialMutationDataTest and is intended
    ///to contain all MWF_CheckMaterialMutationDataTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MWF_CheckMaterialMutationDataTest
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
        ///A test for StockWarehouseId
        ///</summary>
        [TestMethod()]
        public void StockWarehouseIdSmokeTest()
        {
            MWF.CheckMaterialMutationData target = new MWF.CheckMaterialMutationData();
            Nullable<int> expected = new Nullable<int>();
            Nullable<int> actual;
            target.StockWarehouseId = expected;
            actual = target.StockWarehouseId;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for StockQuantity
        ///</summary>
        [TestMethod()]
        public void StockQuantitySmokeTest()
        {
            MWF.CheckMaterialMutationData target = new MWF.CheckMaterialMutationData();
            Decimal expected = new Decimal();
            Decimal actual;
            target.StockQuantity = expected;
            actual = target.StockQuantity;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for StockId
        ///</summary>
        [TestMethod()]
        public void StockIdSmokeTest()
        {
            MWF.CheckMaterialMutationData target = new MWF.CheckMaterialMutationData();
            int expected = 0;
            int actual;
            target.StockId = expected;
            actual = target.StockId;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for MaterialId
        ///</summary>
        [TestMethod()]
        public void MaterialIdSmokeTest()
        {
            MWF.CheckMaterialMutationData target = new MWF.CheckMaterialMutationData();
            int expected = 0;
            int actual;
            target.MaterialId = expected;
            actual = target.MaterialId;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsStockIddbNull
        ///</summary>
        [TestMethod()]
        public void IsStockIddbNullSmokeTest()
        {
            MWF.CheckMaterialMutationData target = new MWF.CheckMaterialMutationData();
            bool expected = false;
            bool actual;
            target.IsStockIddbNull = expected;
            actual = target.IsStockIddbNull;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsMaterialMutationAllowed
        ///</summary>
        [TestMethod()]
        public void IsMaterialMutationAllowedSmokeTest()
        {
            MWF.CheckMaterialMutationData target = new MWF.CheckMaterialMutationData();
            Decimal quantity = new Decimal();
            MWF.MutationStatus expected = MWF.MutationStatus.InvalidQuantity;
            MWF.MutationStatus actual;
            actual = target.IsMaterialMutationAllowed(quantity);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CheckPositiveQuantity
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Flying Test.dll")]
        public void CheckPositiveQuantitySmokeTest()
        {
            Decimal quantity = new Decimal();
            MWF.MutationStatus expected = MWF.MutationStatus.InvalidQuantity;
            MWF.MutationStatus actual;
            actual = MWF_Accessor.CheckMaterialMutationData.CheckPositiveQuantity(quantity);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CheckMaterialMutationData Constructor
        ///</summary>
        [TestMethod()]
        public void MWF_CheckMaterialMutationDataConstructorSmokeTest()
        {
            MWF.CheckMaterialMutationData target = new MWF.CheckMaterialMutationData();
        }
    }
}
