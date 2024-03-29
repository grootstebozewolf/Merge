﻿using Tensing.FieldVision.Scripting.GeneratedPlugin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MSTest
{
    
    
    /// <summary>
    ///This is a test class for MWF_ConcreateUpdateMaterialMutationTest and is intended
    ///to contain all MWF_ConcreateUpdateMaterialMutationTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MWF_ConcreateUpdateMaterialMutationTest
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
        ///A test for DoUpdate
        ///</summary>
        [TestMethod()]
        public void DoUpdateSmokeTest()
        {
            MWF.ConcreateUpdateMaterialMutation target = new MWF.ConcreateUpdateMaterialMutation();
            Guid materialMutationId = new Guid();
            Decimal quantity = new Decimal();
            string notes = string.Empty;
            string serialnumber = string.Empty;
            Nullable<int> materialMutationReasonId = new Nullable<int>();
            MWF.MutationStatus expected = MWF.MutationStatus.Ok;
            MWF.MutationStatus actual;
            actual = target.DoUpdate(materialMutationId, quantity, notes, serialnumber, materialMutationReasonId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConcreateUpdateMaterialMutation Constructor
        ///</summary>
        [TestMethod()]
        public void MWF_ConcreateUpdateMaterialMutationConstructorSmokeTest()
        {
            MWF.ConcreateUpdateMaterialMutation target = new MWF.ConcreateUpdateMaterialMutation();
        }
    }
}
