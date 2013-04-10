using System.Data;
using Moq;
using Tensing.FieldVision.Scripting.GeneratedPlugin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace MSTest
{
    
    
    /// <summary>
    ///This is a test class for MWF_ConsumeMaterialHandleCheckDataListTest and is intended
    ///to contain all MWF_ConsumeMaterialHandleCheckDataListTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MWF_ConsumeMaterialHandleCheckDataListTest
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
        [DeploymentItem("Flying Test.dll")]
        public void NoMaterialFoundSmokeTest()
        {
            MWF_Accessor.ConsumeMaterialHandleCheckDataList target = new MWF_Accessor.ConsumeMaterialHandleCheckDataList();
            MWF.MutationStatus expected = MWF.MutationStatus.MaterialNotFound;
            MWF.MutationStatus actual;
            actual = target.NoMaterialFound();
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// Emulates a datareader for single row for InsertMaterialMutation
        /// </summary>
        /// <returns></returns>
        private IDataReader EmptyIDataReader()
        {
            var moq = new Mock<IDataReader>();
            moq.Setup(x => x.Read())
                // Returns value of local variable 'readToggle' (note that 
                // you must use lambda and not just .Returns(readToggle) 
                // because it will not be lazy initialized then)
                .Returns(() => false);
            return moq.Object;
        }
        /// <summary>
        ///A test for MaterialFound
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Flying Test.dll")]
        public void MaterialFoundSmokeTest()
        {
            MWF_Accessor.ConsumeMaterialHandleCheckDataList target = new MWF_Accessor.ConsumeMaterialHandleCheckDataList();
            List<MWF.CheckMaterialMutationData> checkDataList = new List<MWF.CheckMaterialMutationData>();
            checkDataList.Add(new MWF.CheckMaterialMutationData{IsSerialnumberRequired = false,IsStockIddbNull = false, MaterialId = 1, StockId = 1, StockQuantity = 1,StockWarehouseId = 1});
            Decimal quantity = new Decimal(); // TODO: Initialize to an appropriate value
            Nullable<int> fromWarehouseId = new Nullable<int>(); // TODO: Initialize to an appropriate value
            Nullable<int> toWarehouseId = new Nullable<int>(); // TODO: Initialize to an appropriate value
            string notes = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<int> materialMutationReason = new Nullable<int>(); // TODO: Initialize to an appropriate value
            string serialnumber = string.Empty; // TODO: Initialize to an appropriate value
            MWF.MutationStatus expected = MWF.MutationStatus.InvalidQuantity; // TODO: Initialize to an appropriate value
            MWF.MutationStatus actual;
            actual = target.MaterialFound(checkDataList, quantity, fromWarehouseId, toWarehouseId, notes, materialMutationReason, serialnumber);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AmbiguousResult
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Flying Test.dll")]
        public void AmbiguousResultSmokeTest()
        {
            MWF_Accessor.ConsumeMaterialHandleCheckDataList target = new MWF_Accessor.ConsumeMaterialHandleCheckDataList(); // TODO: Initialize to an appropriate value
            MWF.MutationStatus expected = MWF.MutationStatus.AmbiguousMutation; // TODO: Initialize to an appropriate value
            MWF.MutationStatus actual;
            actual = target.AmbiguousResult();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConsumeMaterialHandleCheckDataList Constructor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Flying Test.dll")]
        public void MWF_ConsumeMaterialHandleCheckDataListConstructorSmokeTest()
        {
            MWF_Accessor.ConsumeMaterialHandleCheckDataList target = new MWF_Accessor.ConsumeMaterialHandleCheckDataList();
        }
    }
}
