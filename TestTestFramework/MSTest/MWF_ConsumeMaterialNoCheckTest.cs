using Tensing.FieldVision.Scripting.GeneratedPlugin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace MSTest
{
    
    
    /// <summary>
    ///This is a test class for MWF_ConsumeMaterialNoCheckTest and is intended
    ///to contain all MWF_ConsumeMaterialNoCheckTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MWF_ConsumeMaterialNoCheckTest
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
        ///A test for NoMaterialFound
        ///</summary>
        [TestMethod()]
        public void NoMaterialFoundSMokeTest()
        {
            MWF.ConsumeMaterialNoCheck target = new MWF.ConsumeMaterialNoCheck(); // TODO: Initialize to an appropriate value
            MWF.MutationStatus expected =MWF.MutationStatus.MaterialNotFound; // TODO: Initialize to an appropriate value
            MWF.MutationStatus actual;
            actual = target.NoMaterialFound();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for MaterialFound
        ///</summary>
        [TestMethod()]
        public void MaterialFoundSmokeTest()
        {
            MWF.ConsumeMaterialNoCheck target = new MWF.ConsumeMaterialNoCheck();
            List<MWF.CheckMaterialMutationData> checkDataList = new List<MWF.CheckMaterialMutationData>();
            checkDataList.Add(new MWF.CheckMaterialMutationData{IsSerialnumberRequired = false,IsStockIddbNull = false,MaterialId = 1,StockId = 1,StockQuantity = 1,StockWarehouseId = 1});
            Decimal quantity = new Decimal();
            Nullable<int> fromWarehouseId = new Nullable<int>();
            Nullable<int> toWarehouseId = new Nullable<int>();
            string notes = string.Empty;
            Nullable<int> materialMutationReason = new Nullable<int>();
            string serialnumber = string.Empty;
            MWF.MutationStatus expected = MWF.MutationStatus.InvalidQuantity;
            MWF.MutationStatus actual;
            actual = target.MaterialFound(checkDataList, quantity, fromWarehouseId, toWarehouseId, notes, materialMutationReason, serialnumber);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AmbiguousResult
        ///</summary>
        [TestMethod()]
        public void AmbiguousResultSmokeTest()
        {
            MWF.ConsumeMaterialNoCheck target = new MWF.ConsumeMaterialNoCheck();
            MWF.MutationStatus expected = MWF.MutationStatus.AmbiguousMutation;
            MWF.MutationStatus actual;
            actual = target.AmbiguousResult();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConsumeMaterialNoCheck Constructor
        ///</summary>
        [TestMethod()]
        public void MWF_ConsumeMaterialNoCheckConstructorSmokeTest()
        {
            MWF.ConsumeMaterialNoCheck target = new MWF.ConsumeMaterialNoCheck();
            //Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
