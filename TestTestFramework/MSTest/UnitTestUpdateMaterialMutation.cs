using System;
using System.Data;
using System.Collections.Generic;
using FieldCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Tensing.FieldVision.Scripting.GeneratedPlugin;

namespace MSTest
{
    [TestClass]
    public class UnitTestUpdateMaterialMutation
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
            moq.Setup(x => x.GetInt32(3))
                .Returns(3);
            return moq.Object;
        }

        /// <summary>
        /// Multiple rows
        /// </summary>
        /// <param name="ojectsToEmulate">Add list of testdata</param>
        /// <returns></returns>
        private IDataReader MockIDataReader(List<TestData> ojectsToEmulate)
        {
            var moq = new Mock<IDataReader>();

            // This var stores current position in 'ojectsToEmulate' list
            int count = -1;

            moq.Setup(x => x.Read())
                // Return 'True' while list still has an item
                .Returns(() => count < ojectsToEmulate.Count - 1)
                // Go to next position
                .Callback(() => count++);
            //Example
            //moq.Setup(x => x["Char"])
            //    .Returns(() => ojectsToEmulate[count].ValidChar);
            moq.Setup(x => x.GetInt32(0))
                .Returns(() => ojectsToEmulate[count].ValidMaterialId);
            moq.Setup(x => x.IsDBNull(1))
                .Returns(() => ojectsToEmulate[count].IsStockIddbNull);
            moq.Setup(x => x.GetInt32(1))
                .Returns(() => ojectsToEmulate[count].ValidStockId);
            moq.Setup(x => x.GetDecimal(2))
                .Returns(() => ojectsToEmulate[count].ValidQuantity);
            moq.Setup(x => x.GetInt32(3))
                .Returns(() => ojectsToEmulate[count].ValidFromWarehouseId);
            return moq.Object;
        }

        private class TestData
        {
            public int ValidMaterialId { get; set; }
            public bool IsStockIddbNull { get; set; }
            public int ValidStockId { get; set; }
            public decimal ValidQuantity { get; set; }
            public int ValidFromWarehouseId { get; set; }
        }

        #endregion

        [TestInitialize()]
        public void MyTestInitialize() { FCXmlCollection.GetValueString = "stock"; }

        [TestMethod]
        public void UpdateMaterialMutation()
        {
            Core.MockDataReader = MockIDataReader(new List<TestData> { new TestData { ValidMaterialId = 1, IsStockIddbNull = false, ValidStockId = 4, ValidQuantity = 2, ValidFromWarehouseId = 1 } });
            ScriptingSystem.strMmt = "1";
            Assert.AreEqual(MWF.MutationStatus.Ok, MWF.Instance.UpdateMaterialMutation(Guid.Parse("7D5DAD6F-08C0-4D31-A1CD-18AEA8CA430E"), null, null, (decimal)2, "Verk. Aant.", "564657681168",null));
            Assert.AreEqual(@"UPDATE fv_materialmutation
SET
 quantity = 2,
 notes = 'Verk. Aant.',
 serialnumber = '564657681168',
 materialmutationreason_id = null
WHERE id = '7d5dad6f-08c0-4d31-a1cd-18aea8ca430e' AND radiostatus_id <= 0", ScriptingSystem.MockDoNCSql);
        }

        [TestMethod]
        public void UpdateMaterialMutationNoCheck()
        {
            FCXmlCollection.GetValueString = "nocheck";
            Core.MockDataReader = MockIDataReader(new List<TestData> { new TestData { ValidMaterialId = 1, IsStockIddbNull = true, ValidStockId = -1, ValidQuantity = -1, ValidFromWarehouseId = -1 } });
            ScriptingSystem.strMmt = "1";
            Assert.AreEqual(MWF.MutationStatus.Ok, MWF.Instance.UpdateMaterialMutation(Guid.Parse("7D5DAD6F-08C0-4D31-A1CD-18AEA8CA430E"), null, null, (decimal)2, "Verk. Aant.", "564657681168", null));
            Assert.AreEqual(@"UPDATE fv_materialmutation
SET
 quantity = 2,
 notes = 'Verk. Aant.',
 serialnumber = '564657681168',
 materialmutationreason_id = null
WHERE id = '7d5dad6f-08c0-4d31-a1cd-18aea8ca430e' AND radiostatus_id <= 0", ScriptingSystem.MockDoNCSql);
        }

        [TestMethod]
        public void UpdateMaterialMutationBelowStockLevelMutationAllowed()
        {
            Core.MockDataReader = MockIDataReader(new List<TestData> { new TestData { ValidMaterialId = 2, IsStockIddbNull = false, ValidStockId = 3, ValidQuantity = 5, ValidFromWarehouseId = 1 } });
            MWF.ShowLangAlertYesNoReturnValue = 1;
            ScriptingSystem.strMmt = "1";
            Assert.AreEqual(MWF.MutationStatus.Ok,MWF.Instance.UpdateMaterialMutation(Guid.Parse("7D5DAD6F-08C0-4D31-A1CD-18AEA8CA430E"), null, null, (decimal)4, "Verk. Aant.",null,2));
            Assert.AreEqual(@"UPDATE fv_materialmutation
SET
 quantity = 4,
 notes = 'Verk. Aant.',
 serialnumber = null,
 materialmutationreason_id = 2
WHERE id = '7d5dad6f-08c0-4d31-a1cd-18aea8ca430e' AND radiostatus_id <= 0", ScriptingSystem.MockDoNCSql);
        }
        [TestMethod]
        public void UpdateMaterialMutationCanceled()
        {
            Core.MockDataReader = MockIDataReader(new List<TestData> { new TestData { ValidMaterialId = 2, IsStockIddbNull = true, ValidStockId = -1, ValidQuantity = -1, ValidFromWarehouseId = -1 } });
            MWF.ShowLangAlertYesNoReturnValue = 0;
            ScriptingSystem.strMmt = "1";
            Assert.AreEqual(MWF.MutationStatus.Canceled, MWF.Instance.UpdateMaterialMutation(Guid.Parse("7D5DAD6F-08C0-4D31-A1CD-18AEA8CA430E"), null,null, (decimal)4, "Verk. Aant.", null,null));
        }
    }
}
