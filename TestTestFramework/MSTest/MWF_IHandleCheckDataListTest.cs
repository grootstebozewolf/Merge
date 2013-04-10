using Tensing.FieldVision.Scripting.GeneratedPlugin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace MSTest
{
    
    
    /// <summary>
    ///This is a test class for MWF_IHandleCheckDataListTest and is intended
    ///to contain all MWF_IHandleCheckDataListTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MWF_IHandleCheckDataListTest
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


        internal virtual MWF.IHandleCheckDataList Create_MWF_IHandleCheckDataList()
        {
            MWF.IHandleCheckDataList target = new MWF.ConsumeMaterialNoCheck();
            return target;
        }

        /// <summary>
        ///A test for NoMaterialFound
        ///</summary>
        [TestMethod()]
        public void NoMaterialFoundSmokeTest()
        {
            MWF.IHandleCheckDataList target = Create_MWF_IHandleCheckDataList();
            MWF.MutationStatus expected = MWF.MutationStatus.MaterialNotFound;
            MWF.MutationStatus actual;
            actual = target.NoMaterialFound();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for MaterialFound
        ///</summary>
        [TestMethod()]
        public void MaterialFoundTest()
        {
            MWF.IHandleCheckDataList target = Create_MWF_IHandleCheckDataList();
            List<MWF.CheckMaterialMutationData> checkDataList = new List<MWF.CheckMaterialMutationData>();
            checkDataList.Add(new MWF.CheckMaterialMutationData{IsStockIddbNull = true,MaterialId = 1});
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
            MWF.IHandleCheckDataList target = Create_MWF_IHandleCheckDataList(); // TODO: Initialize to an appropriate value
            MWF.MutationStatus expected = MWF.MutationStatus.AmbiguousMutation; // TODO: Initialize to an appropriate value
            MWF.MutationStatus actual;
            actual = target.AmbiguousResult();
            Assert.AreEqual(expected, actual);
        }
    }
}
