using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tensing.FieldVision.Scripting.GeneratedPlugin;

namespace MSTest
{
    [TestClass]
    public class DeleteMaterialMutation
    {
        [TestMethod]
        public void DeleteMaterialMutationTest()
        {
            MWF.Instance.DeleteMaterialMutation(Guid.NewGuid());
        }
    }
}
