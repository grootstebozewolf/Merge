using System.Data;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FieldCore;
using Tensing.FieldVision.Scripting;
using Tensing.FieldVision.Scripting.GeneratedPlugin;

namespace MSTest
{
    /// <summary>
    /// Summary description for UnitTestGeneral
    /// </summary>
    [TestClass]
    public class UnitTestInsertMaterialMutation
    {

        #region MockIDataReaders
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
        /// Emulates a datareader for single row for InsertMaterialMutation
        /// </summary>
        /// <returns></returns>
        private IDataReader MockIDataReader()
        {
            var moq = new Mock<IDataReader>();

            bool readToggle = true;

            moq.Setup(x => x.Read())
                // Returns value of local variable 'readToggle' (note that 
                // you must use lambda and not just .Returns(readToggle) 
                // because it will not be lazy initialized then)
                .Returns(() => readToggle)
                // After 'Read()' is executed - we change 'readToggle' value 
                // so it will return false on next calls of 'Read()'
                .Callback(() => readToggle = false);

            moq.Setup(x => x.GetInt32(0))
                .Returns(100);
            moq.Setup(x => x.IsDBNull(1))
                .Returns(false);
            moq.Setup(x => x.GetInt32(1))
                .Returns(1);
            moq.Setup(x => x.GetDecimal(2))
                .Returns(2);
            moq.Setup(x => x.IsDBNull(3))
                .Returns(false);
            moq.Setup(x => x.GetInt32(3))
                .Returns(3);
            moq.Setup(x => x.IsDBNull(4))
                .Returns(false);
            moq.Setup(x => x.GetInt32(4))
                .Returns(0);
            return moq.Object;
        }

        /// <summary>
        /// Multiple rows
        /// </summary>
        /// <param name="objectsToEmulate">Add list of testdata</param>
        /// <returns></returns>
        private IDataReader MockIDataReader(List<TestData> objectsToEmulate)
        {
            var moq = new Mock<IDataReader>();

            // This var stores current position in 'objectsToEmulate' list
            int count = -1;

            moq.Setup(x => x.Read())
                // Return 'True' while list still has an item
                .Returns(() => count < objectsToEmulate.Count - 1)
                // Go to next position
                .Callback(() => count++);
            //Example
            //moq.Setup(x => x["Char"])
            //    .Returns(() => ojectsToEmulate[count].ValidChar);
            moq.Setup(x => x.GetInt32(0))
                .Returns(() => objectsToEmulate[count].ValidMaterialId);
            moq.Setup(x => x.IsDBNull(1))
                .Returns(() => objectsToEmulate[count].IsStockIddbNull);
            moq.Setup(x => x.GetInt32(1))
                .Returns(() => objectsToEmulate[count].ValidStockId);
            moq.Setup(x => x.GetDecimal(2))
                .Returns(() => objectsToEmulate[count].ValidQuantity);
            moq.Setup(x => x.IsDBNull(3))
                .Returns(() => objectsToEmulate[count].IsFromWarehouseIddbNull);
            moq.Setup(x => x.GetInt32(3))
                .Returns(() => objectsToEmulate[count].ValidFromWarehouseId);
            moq.Setup(x => x.IsDBNull(4))
                .Returns(() => objectsToEmulate[count].IsSerialnumberRequired == null);
            moq.Setup(x => x.GetInt32(4))
                .Returns(() => objectsToEmulate[count].IsSerialnumberRequired??0);
            return moq.Object;
        }

        private class TestData
        {
            public int? IsSerialnumberRequired { get; set; }
            public int ValidMaterialId { get; set; }
            public bool IsStockIddbNull { get; set; }
            public int ValidStockId { get; set; }
            public decimal ValidQuantity { get; set; }
            public bool IsFromWarehouseIddbNull { get; set; }
            public int ValidFromWarehouseId { get; set; }
        }

        #endregion

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize() { FCXmlCollection.GetValueString = "stock"; }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void NegativeBookingNotAllowed()
        {
            //Negatieve boeking of 0 boeking niet toegestaan
            //Dit moet afgecheckt worden bij insertMaterialMutation
            Core.MockDataReader = MockIDataReader();
            ScriptingSystem.strMmt = "1";
            Assert.AreEqual(MWF.MutationStatus.InvalidQuantity, MWF.Instance.InsertMaterialMutation(null, "internalId100", "", -1, null, null, "Notes",null,null));
        }

        [TestMethod]
        public void DecimalBookingOnlyAllowed()
        {
            Core.MockDataReader = MockIDataReader();
            ScriptingSystem.strMmt = "1";
            Assert.AreEqual(MWF.MutationStatus.Ok, MWF.Instance.InsertMaterialMutation(null, "internalId100", "", (decimal)0.5, null, null, "Notes",null, null));
            Assert.AreEqual(@"INSERT INTO fv_materialmutation (
 id,
 activity_id,
 serviceorder_id,
 materialmutationtype_id,
 materialmutationreason_id,
 engineer_id,
 material_id,
 quantity,
 notes,
 serialnumber,
 fromwarehouse_id,
 towarehouse_id,
 stock_id,
 stock_quantity,
 verifydt,
 mutationdt,
 isactive,
 radiostatus_id,
 organisation_id
) VALUES ( 
 /* id - uniqueidentifier */ NEWID(),
 /* activity_id - uniqueidentifier */ CASE WHEN $flow.isorderrelated$+0 = 0 THEN null ELSE '$flow.activity_id$' END,
 /* serviceorder_id - uniqueidentifier */ CASE WHEN '$flow.serviceorder_id$' = '00000000-0000-0000-0000-000000000000' THEN NULL ELSE '$flow.serviceorder_id$' END,
 /* materialmutationtype_id - int */ $flow.mmtid$,
 /* materialmutationreason_id - int */ null,
 /* engineer_id - int */ $app.userid$,
 /* material_id - int */ 100,
 /* quantity - real */ 0.5,
 /* notes - varchar(255) */ 'Notes',
 /* serialnumber - varchar(50) */ null,
 /* fromwarehouse_id - int */ 3,
 /* towarehouse_id - int */ null,
 /* stock_id - int */ CASE WHEN 1=0 THEN null ELSE 1 END,
 /* stock_quantity - real */ CASE WHEN 2=0 THEN null ELSE 2 END,
 /* verifydt - datetime */ '$app.verifydt$',
 /* mutationdt - datetime */ GETDATE(),
 /* isactive - int */ 1,
 /* radiostatus_id - int */ CASE WHEN $flow.isorderrelated$+0 = 0 THEN 0 ELSE -1 END,
 /* organisation_id - int */ $app.organisationid$
)", ScriptingSystem.MockDoNCSql, "Insert komt niet overeen.");
        }

        [TestMethod]
        public void InsertMaterialMutationMaterialNotFound()
        {
            Core.MockDataReader = EmptyIDataReader();
            ScriptingSystem.strMmt = "1";
            Assert.AreEqual(MWF.MutationStatus.MaterialNotFound, MWF.Instance.InsertMaterialMutation(null, "1", "", 1, null, null,"", null,null));
        }

        
        
       [TestMethod]
        public void TestMock()
       {
           var moq = new Mock<IDataReader>();
           Assert.IsNotNull(moq);
       }

        [TestMethod]
        public void InsertMaterialMutation()
        {
            Core.MockDataReader = MockIDataReader();
            ScriptingSystem.strMmt = "1";
            Assert.AreEqual(MWF.MutationStatus.Ok, MWF.Instance.InsertMaterialMutation(null, "internalId100", "", 2, null, null, "Notes",null,null));
            Assert.AreEqual(@"INSERT INTO fv_materialmutation (
 id,
 activity_id,
 serviceorder_id,
 materialmutationtype_id,
 materialmutationreason_id,
 engineer_id,
 material_id,
 quantity,
 notes,
 serialnumber,
 fromwarehouse_id,
 towarehouse_id,
 stock_id,
 stock_quantity,
 verifydt,
 mutationdt,
 isactive,
 radiostatus_id,
 organisation_id
) VALUES ( 
 /* id - uniqueidentifier */ NEWID(),
 /* activity_id - uniqueidentifier */ CASE WHEN $flow.isorderrelated$+0 = 0 THEN null ELSE '$flow.activity_id$' END,
 /* serviceorder_id - uniqueidentifier */ CASE WHEN '$flow.serviceorder_id$' = '00000000-0000-0000-0000-000000000000' THEN NULL ELSE '$flow.serviceorder_id$' END,
 /* materialmutationtype_id - int */ $flow.mmtid$,
 /* materialmutationreason_id - int */ null,
 /* engineer_id - int */ $app.userid$,
 /* material_id - int */ 100,
 /* quantity - real */ 2,
 /* notes - varchar(255) */ 'Notes',
 /* serialnumber - varchar(50) */ null,
 /* fromwarehouse_id - int */ 3,
 /* towarehouse_id - int */ null,
 /* stock_id - int */ CASE WHEN 1=0 THEN null ELSE 1 END,
 /* stock_quantity - real */ CASE WHEN 2=0 THEN null ELSE 2 END,
 /* verifydt - datetime */ '$app.verifydt$',
 /* mutationdt - datetime */ GETDATE(),
 /* isactive - int */ 1,
 /* radiostatus_id - int */ CASE WHEN $flow.isorderrelated$+0 = 0 THEN 0 ELSE -1 END,
 /* organisation_id - int */ $app.organisationid$
)", ScriptingSystem.MockDoNCSql,"Insert komt niet overeen.");
        }

        [TestMethod]
        public void InsertMaterialMutationNoStockRejected()
        {
            Core.MockDataReader = MockIDataReader(new List<TestData> { new TestData { ValidMaterialId = 101, IsStockIddbNull = true, ValidStockId = -1, ValidQuantity = -1, ValidFromWarehouseId =-1} });
            MWF.ShowLangAlertYesNoReturnValue = 0;
            ScriptingSystem.strMmt = "1";
            Assert.AreEqual(MWF.MutationStatus.Canceled, MWF.Instance.InsertMaterialMutation(null, "internalId101", "", 1, null, null, "12345",null,null));
        }

        [TestMethod]
        public void InsertMaterialMutationNoStock()
        {
            Core.MockDataReader = MockIDataReader(new List<TestData> { new TestData { ValidMaterialId = 101, IsStockIddbNull = true, ValidStockId = -1, ValidQuantity = -1, ValidFromWarehouseId = -1 } });
            MWF.ShowLangAlertYesNoReturnValue = 1;
            ScriptingSystem.strMmt = "1";
            Assert.AreEqual(MWF.MutationStatus.Ok, MWF.Instance.InsertMaterialMutation(null, "6035", "", 1, 13, null, "12345",null,null));
//            Assert.AreEqual(@"SELECT 
// m.id, 
// st.id,
// st.quantity, 
// st.warehouse_id
//FROM 
// fv_material m
// LEFT OUTER JOIN fv_stock st
//  ON st.material_id = m.id
//  AND st.warehouse_id = CASE WHEN 0=0 THEN  st.warehouse_id ELSE 0 END
//WHERE
// m.external_id LIKE(CASE WHEN '6035'='' THEN  m.external_id ELSE '%6035%' END)
// AND m.description LIKE(CASE WHEN ''='' THEN  m.description ELSE '%%' END)", Core.CnDB.ExecuteSelectSQLParamValue, true,"Check No Stock Existing_id valid SQL");
            Assert.AreEqual(@"INSERT INTO fv_materialmutation (
 id,
 activity_id,
 serviceorder_id,
 materialmutationtype_id,
 materialmutationreason_id,
 engineer_id,
 material_id,
 quantity,
 notes,
 serialnumber,
 fromwarehouse_id,
 towarehouse_id,
 stock_id,
 stock_quantity,
 verifydt,
 mutationdt,
 isactive,
 radiostatus_id,
 organisation_id
) VALUES ( 
 /* id - uniqueidentifier */ NEWID(),
 /* activity_id - uniqueidentifier */ CASE WHEN $flow.isorderrelated$+0 = 0 THEN null ELSE '$flow.activity_id$' END,
 /* serviceorder_id - uniqueidentifier */ CASE WHEN '$flow.serviceorder_id$' = '00000000-0000-0000-0000-000000000000' THEN NULL ELSE '$flow.serviceorder_id$' END,
 /* materialmutationtype_id - int */ $flow.mmtid$,
 /* materialmutationreason_id - int */ null,
 /* engineer_id - int */ $app.userid$,
 /* material_id - int */ 101,
 /* quantity - real */ 1,
 /* notes - varchar(255) */ '12345',
 /* serialnumber - varchar(50) */ null,
 /* fromwarehouse_id - int */ 13,
 /* towarehouse_id - int */ null,
 /* stock_id - int */ CASE WHEN 0=0 THEN null ELSE 0 END,
 /* stock_quantity - real */ CASE WHEN 0=0 THEN null ELSE 0 END,
 /* verifydt - datetime */ '$app.verifydt$',
 /* mutationdt - datetime */ GETDATE(),
 /* isactive - int */ 1,
 /* radiostatus_id - int */ CASE WHEN $flow.isorderrelated$+0 = 0 THEN 0 ELSE -1 END,
 /* organisation_id - int */ $app.organisationid$
)", ScriptingSystem.MockDoNCSql, "Insert komt niet overeen.");
        }

        [TestMethod]
        public void InsertMaterialMutationNoCheck()
        {
            FCXmlCollection.GetValueString = "nocheck";
            Core.MockDataReader = MockIDataReader(new List<TestData> { new TestData { ValidMaterialId = 101, IsStockIddbNull = true, ValidStockId = -1, ValidQuantity = -1, ValidFromWarehouseId = -1 } });
            ScriptingSystem.strMmt = "1";
            Assert.AreEqual(MWF.MutationStatus.Ok, MWF.Instance.InsertMaterialMutation(null, "6035", "", 1, 13, null, "12345", null, null));
            //            Assert.AreEqual(@"SELECT 
            // m.id, 
            // st.id,
            // st.quantity, 
            // st.warehouse_id
            //FROM 
            // fv_material m
            // LEFT OUTER JOIN fv_stock st
            //  ON st.material_id = m.id
            //  AND st.warehouse_id = CASE WHEN 0=0 THEN  st.warehouse_id ELSE 0 END
            //WHERE
            // m.external_id LIKE(CASE WHEN '6035'='' THEN  m.external_id ELSE '%6035%' END)
            // AND m.description LIKE(CASE WHEN ''='' THEN  m.description ELSE '%%' END)", Core.CnDB.ExecuteSelectSQLParamValue, true,"Check No Stock Existing_id valid SQL");
            Assert.AreEqual(@"INSERT INTO fv_materialmutation (
 id,
 activity_id,
 serviceorder_id,
 materialmutationtype_id,
 materialmutationreason_id,
 engineer_id,
 material_id,
 quantity,
 notes,
 serialnumber,
 fromwarehouse_id,
 towarehouse_id,
 stock_id,
 stock_quantity,
 verifydt,
 mutationdt,
 isactive,
 radiostatus_id,
 organisation_id
) VALUES ( 
 /* id - uniqueidentifier */ NEWID(),
 /* activity_id - uniqueidentifier */ CASE WHEN $flow.isorderrelated$+0 = 0 THEN null ELSE '$flow.activity_id$' END,
 /* serviceorder_id - uniqueidentifier */ CASE WHEN '$flow.serviceorder_id$' = '00000000-0000-0000-0000-000000000000' THEN NULL ELSE '$flow.serviceorder_id$' END,
 /* materialmutationtype_id - int */ $flow.mmtid$,
 /* materialmutationreason_id - int */ null,
 /* engineer_id - int */ $app.userid$,
 /* material_id - int */ 101,
 /* quantity - real */ 1,
 /* notes - varchar(255) */ '12345',
 /* serialnumber - varchar(50) */ null,
 /* fromwarehouse_id - int */ 13,
 /* towarehouse_id - int */ null,
 /* stock_id - int */ CASE WHEN 0=0 THEN null ELSE 0 END,
 /* stock_quantity - real */ CASE WHEN 0=0 THEN null ELSE 0 END,
 /* verifydt - datetime */ '$app.verifydt$',
 /* mutationdt - datetime */ GETDATE(),
 /* isactive - int */ 1,
 /* radiostatus_id - int */ CASE WHEN $flow.isorderrelated$+0 = 0 THEN 0 ELSE -1 END,
 /* organisation_id - int */ $app.organisationid$
)", ScriptingSystem.MockDoNCSql, "Insert komt niet overeen.");
        }

        [TestMethod]
        public void InsertMaterialMutationNoStockNoWarehouse()
        {
            Core.MockDataReader = MockIDataReader(new List<TestData> { new TestData { ValidMaterialId = 101, IsStockIddbNull = true, ValidStockId = -1, ValidQuantity = -1, ValidFromWarehouseId = -1 } });
            MWF.ShowLangAlertYesNoReturnValue = 1;
            ScriptingSystem.strMmt = "1";
            var ret = MWF.Instance.InsertMaterialMutation(null, "6035", "", 1, null, null, "12345", null, null);
            Assert.AreEqual(MWF.MutationStatus.Canceled, ret);
        }
        [TestMethod]
        public void DoNotInsertMaterialMutationIfAmbiguousResult()
        {
            ScriptingSystem.strMmt = "1";
            //Ambiguous multiple no stock
            Core.MockDataReader = MockIDataReader(new List<TestData> { 
                new TestData { ValidMaterialId = 101, IsStockIddbNull = true, ValidStockId = -1, ValidQuantity = -1, ValidFromWarehouseId = -1 },
                new TestData { ValidMaterialId = 102, IsStockIddbNull = true, ValidStockId = -1, ValidQuantity = -1, ValidFromWarehouseId = -1 }
            });
            Assert.AreEqual(MWF.MutationStatus.AmbiguousMutation, MWF.Instance.InsertMaterialMutation(null, "internalId101", "", 1, null, null, "12345", null,null));
            //Ambiguous 1 stock item
            Core.MockDataReader = MockIDataReader(new List<TestData> { 
                new TestData { ValidMaterialId = 101, IsStockIddbNull = false, ValidStockId = 1, ValidQuantity = 1, ValidFromWarehouseId = 1 },
                new TestData { ValidMaterialId = 102, IsStockIddbNull = true, ValidStockId = -1, ValidQuantity = -1, ValidFromWarehouseId = -1 },
                new TestData { ValidMaterialId = 103, IsStockIddbNull = true, ValidStockId = -1, ValidQuantity = -1, ValidFromWarehouseId = -1 }
            });
            Assert.AreEqual(MWF.MutationStatus.AmbiguousMutation, MWF.Instance.InsertMaterialMutation(null, "internalId101", "", 1, null, null, "12345", null, null));
            //Ambiguous 2 stock item
            Core.MockDataReader = MockIDataReader(new List<TestData> { 
                new TestData { ValidMaterialId = 101, IsStockIddbNull = false, ValidStockId = 1, ValidQuantity = 1, ValidFromWarehouseId = 1 },
                new TestData { ValidMaterialId = 101, IsStockIddbNull = false, ValidStockId = 2, ValidQuantity = 1, ValidFromWarehouseId = 1 } 
            });
            Assert.AreEqual(MWF.MutationStatus.AmbiguousMutation, MWF.Instance.InsertMaterialMutation(null, "internalId101", "", 1, null, null, "12345", null, null));
        }

        [TestMethod]
        public void WarningForEmptyWarehouse()
        {
            ScriptingSystem.strMmt = "1";
            Core.MockDataReader = MockIDataReader(new List<TestData> { 
                new TestData { ValidMaterialId = 101, IsStockIddbNull = false, ValidStockId = 1, ValidQuantity = 1, ValidFromWarehouseId = 1 }
            });
            //Take away a single item that is in stock.
            //MWF.Instance.InsertMaterialMutation(null, "materialnumber101", "", 1, null, null, "12345",null);
            MWF.Instance.ConsumeMaterialByName("materialnumber101", 1, "12354");
            
            //validates that the correct warning is shown.
            Assert.AreEqual("88.7", MWF.ShowLangAlertOKParam);
        }

        [TestMethod]
        public void NegativeBookingsAreNotAllowed()
        {
            ScriptingSystem.strMmt = "1";
            Core.MockDataReader = MockIDataReader(new List<TestData> { 
                new TestData { ValidMaterialId = 101, IsStockIddbNull = false, ValidStockId = 1, ValidQuantity = 1, ValidFromWarehouseId = 1 }
            });

            //This tries to do a NEGATIVE booking.                                   !!!
            var ret = MWF.Instance.InsertMaterialMutation(null, "internalId101", "", -1, null, null, "12345", null, null);

            Assert.AreEqual(MWF.MutationStatus.InvalidQuantity, ret);
        
        }

        [TestMethod]
        public void InsertMaterialBySerialNumber()
        {
            ScriptingSystem.strMmt = "1";
            var mockISymbolScanningDevice = new Mock<ISymbolScanningDevice>();
            mockISymbolScanningDevice.Setup(x => x.value)
                .Returns("1234001");
            Core.MockDataReader = MockIDataReader(new List<TestData> { 
                new TestData { ValidMaterialId = 138, IsStockIddbNull = false, ValidStockId = 51, ValidQuantity = 1, ValidFromWarehouseId = 15 }
            });
            MWF.FakeGetIntDataValue = 51;
            Assert.AreEqual(MWF.MutationStatus.Ok, MWF.Instance.InsertMaterialMutation(mockISymbolScanningDevice.Object));
        }

        [TestMethod]
        public void InsertMaterialSerialNumberEmptyStringReturnsFalse()
        {
            ScriptingSystem.strMmt = "1";
            var mockISymbolScanningDevice = new Mock<ISymbolScanningDevice>();
            mockISymbolScanningDevice.Setup(x => x.value)
                .Returns("");
            //Core.MockDataReader = MockIDataReader(new List<TestData> { 
            //    new TestData { ValidMaterialId = 138, IsStockIddbNull = false, ValidStockId = 51, ValidQuantity = 1, ValidFromWarehouseId = 15 }
            //});
            //MWF.FakeGetIntDataValue = 51;
            Assert.AreEqual(MWF.MutationStatus.ScannerNotReady, MWF.Instance.InsertMaterialMutation(mockISymbolScanningDevice.Object));
        }

        [TestMethod]
        public void InsertMaterialSymbolScanNotInitialised()
        {
            ScriptingSystem.strMmt = "1";
            ISymbolScanningDevice objDevice = null;
            //var mockISymbolScanningDevice = new Mock<ISymbolScanningDevice>();
            //mockISymbolScanningDevice.Setup(x => x.value)
            //    .Returns("");
            //Core.MockDataReader = MockIDataReader(new List<TestData> { 
            //    new TestData { ValidMaterialId = 138, IsStockIddbNull = false, ValidStockId = 51, ValidQuantity = 1, ValidFromWarehouseId = 15 }
            //});
            //MWF.FakeGetIntDataValue = 51;
            Assert.AreEqual(MWF.MutationStatus.ScannerNotReady, MWF.Instance.InsertMaterialMutation(objDevice));
        }

        [TestMethod]
        public void InsertMaterialScannedCodeNotFound()
        {
            ScriptingSystem.strMmt = "1";
            var mockISymbolScanningDevice = new Mock<ISymbolScanningDevice>();
            mockISymbolScanningDevice.Setup(x => x.value)
                .Returns("8713074577522");
            Core.MockDataReader = EmptyIDataReader(); //Define a Dataset with no data, so No materials could be found whith *ANY* code.
            //Core.MockDataReader = MockIDataReader(new List<TestData> { 
            //    new TestData { ValidMaterialId = 138, IsStockIddbNull = false, ValidStockId = 51, ValidQuantity = 1, ValidFromWarehouseId = 15 }
            //});
            MWF.FakeGetIntDataValue = 0;
            Assert.AreEqual(MWF.MutationStatus.MaterialNotFound, MWF.Instance.InsertMaterialMutation(mockISymbolScanningDevice.Object));
        }

        [TestMethod]
        public void Retour()
        {
            ScriptingSystem.strMmt = "2";
            Core.MockDataReader = MockIDataReader(new List<TestData> { 
                new TestData { ValidMaterialId = 160, IsStockIddbNull = false, ValidStockId = 0, ValidQuantity = 2, ValidFromWarehouseId = 0 }
            });
            Assert.AreEqual(MWF.MutationStatus.Ok, MWF.Instance.InsertMaterialMutation(36, "20-004.101", "", 1, null, 13, "Defect", 1, null));
            Assert.AreEqual(@"INSERT INTO fv_materialmutation (
 id,
 activity_id,
 serviceorder_id,
 materialmutationtype_id,
 materialmutationreason_id,
 engineer_id,
 material_id,
 quantity,
 notes,
 serialnumber,
 fromwarehouse_id,
 towarehouse_id,
 stock_id,
 stock_quantity,
 verifydt,
 mutationdt,
 isactive,
 radiostatus_id,
 organisation_id
) VALUES ( 
 /* id - uniqueidentifier */ NEWID(),
 /* activity_id - uniqueidentifier */ CASE WHEN $flow.isorderrelated$+0 = 0 THEN null ELSE '$flow.activity_id$' END,
 /* serviceorder_id - uniqueidentifier */ CASE WHEN '$flow.serviceorder_id$' = '00000000-0000-0000-0000-000000000000' THEN NULL ELSE '$flow.serviceorder_id$' END,
 /* materialmutationtype_id - int */ $flow.mmtid$,
 /* materialmutationreason_id - int */ 1,
 /* engineer_id - int */ $app.userid$,
 /* material_id - int */ 160,
 /* quantity - real */ 1,
 /* notes - varchar(255) */ 'Defect',
 /* serialnumber - varchar(50) */ null,
 /* fromwarehouse_id - int */ null,
 /* towarehouse_id - int */ 13,
 /* stock_id - int */ CASE WHEN 0=0 THEN null ELSE 0 END,
 /* stock_quantity - real */ CASE WHEN 0=0 THEN null ELSE 0 END,
 /* verifydt - datetime */ '$app.verifydt$',
 /* mutationdt - datetime */ GETDATE(),
 /* isactive - int */ 1,
 /* radiostatus_id - int */ CASE WHEN $flow.isorderrelated$+0 = 0 THEN 0 ELSE -1 END,
 /* organisation_id - int */ $app.organisationid$
)", ScriptingSystem.MockDoNCSql, "Insert komt niet overeen.");
        }

        class BestellenTestData
        {
            public static string ArticleName = "20-004.101";
            public static decimal Quantity = 1;
            public static int? ToWarehouseId = 13;
            public static string Notes = "12345";
            public static int MaterialMutationReason = 4;
        }

        [TestMethod]
        public void Bestellen()
        {
            Core.MockDataReader = MockIDataReader(new List<TestData> { 
                new TestData { ValidMaterialId = 160, IsStockIddbNull = false, ValidStockId = 0, ValidQuantity = 2147483647, IsFromWarehouseIddbNull = false, ValidFromWarehouseId = 0 }
            });
            ScriptingSystem.strMmt = "3";
            //MWF.Instance.InsertMaterialMutation(null, "20-004.101", "", 1, null, 13, "12345", null)
            bool ret = MWF.Instance.OrderMaterialByArticleNumber(BestellenTestData.ArticleName, BestellenTestData.Quantity, BestellenTestData.ToWarehouseId, BestellenTestData.Notes, BestellenTestData.MaterialMutationReason);
            Assert.IsTrue(ret);
            Assert.AreEqual(@"INSERT INTO fv_materialmutation (
 id,
 activity_id,
 serviceorder_id,
 materialmutationtype_id,
 materialmutationreason_id,
 engineer_id,
 material_id,
 quantity,
 notes,
 serialnumber,
 fromwarehouse_id,
 towarehouse_id,
 stock_id,
 stock_quantity,
 verifydt,
 mutationdt,
 isactive,
 radiostatus_id,
 organisation_id
) VALUES ( 
 /* id - uniqueidentifier */ NEWID(),
 /* activity_id - uniqueidentifier */ CASE WHEN $flow.isorderrelated$+0 = 0 THEN null ELSE '$flow.activity_id$' END,
 /* serviceorder_id - uniqueidentifier */ CASE WHEN '$flow.serviceorder_id$' = '00000000-0000-0000-0000-000000000000' THEN NULL ELSE '$flow.serviceorder_id$' END,
 /* materialmutationtype_id - int */ $flow.mmtid$,
 /* materialmutationreason_id - int */ 4,
 /* engineer_id - int */ $app.userid$,
 /* material_id - int */ 160,
 /* quantity - real */ 1,
 /* notes - varchar(255) */ '12345',
 /* serialnumber - varchar(50) */ null,
 /* fromwarehouse_id - int */ null,
 /* towarehouse_id - int */ 13,
 /* stock_id - int */ CASE WHEN 0=0 THEN null ELSE 0 END,
 /* stock_quantity - real */ CASE WHEN 0=0 THEN null ELSE 0 END,
 /* verifydt - datetime */ '$app.verifydt$',
 /* mutationdt - datetime */ GETDATE(),
 /* isactive - int */ 1,
 /* radiostatus_id - int */ CASE WHEN $flow.isorderrelated$+0 = 0 THEN 0 ELSE -1 END,
 /* organisation_id - int */ $app.organisationid$
)", ScriptingSystem.MockDoNCSql, "Insert komt niet overeen.");
        }
        [TestMethod]
        public void SerialNumberRequiredAddMaterialWithQuantityLessThanOne()
        {
            ScriptingSystem.strMmt = "3";
            Core.MockDataReader = MockIDataReader(new List<TestData> { 
                new TestData { ValidMaterialId = 166, IsStockIddbNull = false, ValidStockId = 0, ValidQuantity = 1, ValidFromWarehouseId = 0, IsSerialnumberRequired = 1}
            });
            Assert.AreEqual(MWF.MutationStatus.QuantityNotOne, MWF.Instance.InsertMaterialMutation(36, "20-004.101", "", (decimal)0.5, null, 13, "Defect", 1, "12345678"));
        }
        [TestMethod]
        public void SerialNumberRequiredAddMaterialWitNoSerialnumber()
        {
            ScriptingSystem.strMmt = "3";
            Core.MockDataReader = MockIDataReader(new List<TestData> { 
                new TestData { ValidMaterialId = 166, IsStockIddbNull = false, ValidStockId = 0, ValidQuantity = 1, ValidFromWarehouseId = 0, IsSerialnumberRequired = 1}
            });
            Assert.AreEqual(MWF.MutationStatus.SerialNumberRequired, MWF.Instance.InsertMaterialMutation(36, "20-004.101", "", 1, null, 13, "Defect", 1, null));
        }
        [TestMethod]
        public void SerialNumberRequiredAddMaterialWitEmptyStringAsSerialnumber()
        {
            ScriptingSystem.strMmt = "3";
            Core.MockDataReader = MockIDataReader(new List<TestData> { 
                new TestData { ValidMaterialId = 166, IsStockIddbNull = false, ValidStockId = 0, ValidQuantity = 1, ValidFromWarehouseId = 0, IsSerialnumberRequired = 1}
            });
            Assert.AreEqual(MWF.MutationStatus.SerialNumberRequired, MWF.Instance.InsertMaterialMutation(36, "20-004.101", "", 1, null, 13, "Defect", 1, ""));
        }

        [TestMethod]
        public void InsertMaterialWithSerialnumberAndQuantityTwo()
        {
            ScriptingSystem.strMmt = "3";
            Core.MockDataReader = MockIDataReader(new List<TestData> { 
                new TestData { ValidMaterialId = 166, IsStockIddbNull = false, ValidStockId = 0, ValidQuantity = 10, ValidFromWarehouseId = 0, IsSerialnumberRequired = 0}
            });
            Assert.AreEqual(MWF.MutationStatus.QuantityNotOne, MWF.Instance.InsertMaterialMutation(36, "20-004.102", "", 2, null, 13, "Defect", 1, "12345678"));
        }

        [TestMethod]
        public void ReturnMaterialWithSerialNumberQuantityOneAndSerialnumberCorrect()
        {
            Core.MockDataReader = MockIDataReader(new List<TestData> { 
                new TestData { ValidMaterialId = 160, IsStockIddbNull = false, ValidStockId = 12, ValidQuantity = 2147483647, IsFromWarehouseIddbNull = false, ValidFromWarehouseId = 0, IsSerialnumberRequired = 1 }
            });
            ScriptingSystem.strMmt = "3";
            //MWF.Instance.InsertMaterialMutation(null, "20-004.101", "", 1, null, 13, "12345", null)
            Assert.AreEqual(MWF.MutationStatus.Ok, MWF.Instance.InsertMaterialMutation(null, "20-004.101", "", 1, null, 15, "Defect", 4, "12345678"));
            Assert.AreEqual(@"INSERT INTO fv_materialmutation (
 id,
 activity_id,
 serviceorder_id,
 materialmutationtype_id,
 materialmutationreason_id,
 engineer_id,
 material_id,
 quantity,
 notes,
 serialnumber,
 fromwarehouse_id,
 towarehouse_id,
 stock_id,
 stock_quantity,
 verifydt,
 mutationdt,
 isactive,
 radiostatus_id,
 organisation_id
) VALUES ( 
 /* id - uniqueidentifier */ NEWID(),
 /* activity_id - uniqueidentifier */ CASE WHEN $flow.isorderrelated$+0 = 0 THEN null ELSE '$flow.activity_id$' END,
 /* serviceorder_id - uniqueidentifier */ CASE WHEN '$flow.serviceorder_id$' = '00000000-0000-0000-0000-000000000000' THEN NULL ELSE '$flow.serviceorder_id$' END,
 /* materialmutationtype_id - int */ $flow.mmtid$,
 /* materialmutationreason_id - int */ 4,
 /* engineer_id - int */ $app.userid$,
 /* material_id - int */ 160,
 /* quantity - real */ 1,
 /* notes - varchar(255) */ 'Defect',
 /* serialnumber - varchar(50) */ '12345678',
 /* fromwarehouse_id - int */ null,
 /* towarehouse_id - int */ 15,
 /* stock_id - int */ CASE WHEN 12=0 THEN null ELSE 12 END,
 /* stock_quantity - real */ CASE WHEN 0=0 THEN null ELSE 0 END,
 /* verifydt - datetime */ '$app.verifydt$',
 /* mutationdt - datetime */ GETDATE(),
 /* isactive - int */ 1,
 /* radiostatus_id - int */ CASE WHEN $flow.isorderrelated$+0 = 0 THEN 0 ELSE -1 END,
 /* organisation_id - int */ $app.organisationid$
)", ScriptingSystem.MockDoNCSql, "Insert komt niet overeen.");
        }
    }
}
