using System;
using System.Windows.Forms;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Tensing.FieldVision.Scripting.GeneratedPlugin;

namespace MSTest
{
    [TestClass]
    public class TestMouseUpDelegate
    {
        [TestMethod]
        public void SmokeTest()
        {
            var mockControl = new Mock<Control>();
            Castle.DynamicProxy.Generators.AttributesToAvoidReplicating.Add(
                typeof (System.Security.Permissions.UIPermissionAttribute));
            mockControl.Object.MouseUp += MWF.Instance.GridMaterialMouseUp;
        }
    }
}
