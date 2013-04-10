using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDILReader;

namespace MSTest
{
    [TestClass]
    public class UnitTestAllMaterialMutationButtons: BaseTest
    {
        [TestMethod]
        public void TestMethodAllMaterialMutationButtons()
        {
            foreach (ScreenLib.ScreenConfiguration.Screen.Component component in _configuration.GetComponentsByOnClickOpenReference(96))
            {
                string script = ((ScreenLib.ScreenConfiguration.Screen.FVButton)component).OnClick();
                Assert.IsTrue(script.Contains("isorderrelated"), string.Format(@"Script in Component {0}:
{1}
Did not contain isorderrelated", component.id, script));
                                Assert.IsTrue(script.Contains("MatRegChange"), string.Format(@"Script in Component {0}:
{1}
Did not contain MatRegChange", component.id, script));
            }
            foreach(ScreenLib.ScreenConfiguration.Screen.Component component in _configuration.GetComponentsByOnClickOpenReference(110))
            {
                string script = ((ScreenLib.ScreenConfiguration.Screen.FVButton)component).OnClick();
                Assert.IsTrue(script.Contains("isorderrelated"), string.Format(@"Script in Component {0}:
{1}
Did not contain isorderrelated", component.id, script));
                Assert.IsTrue(script.Contains("MatRegChange"), string.Format(@"Script in Component {0}:
{1}
Did not contain MatRegChange", component.id, script));
            }
            foreach (ScreenLib.ScreenConfiguration.Screen.Component component in _configuration.GetComponentsByOnClickOpenReference(111))
            {
                string script = ((ScreenLib.ScreenConfiguration.Screen.FVButton)component).OnClick();
                Assert.IsTrue(script.Contains("isorderrelated"), string.Format(@"Script in Component {0}:
{1}
Did not contain isorderrelated", component.id, script));
                Assert.IsTrue(script.Contains("MatRegChange"), string.Format(@"Script in Component {0}:
{1}
Did not contain MatRegChange", component.id, script));
                
            }
        }

        [TestInitialize]
        public override void SetupEnviroment()
        {
            Globals.LoadOpCodes();
            _configuration = new ScreenLib.ScreenConfiguration(_xrScreen, _assemblyScripting);
        }
    }
}
