using Microsoft.VisualStudio.TestTools.UnitTesting;
using helloLearn.Business;
using System.Collections.Generic;

namespace UnitTestHelloLearn
{
    [TestClass]
    public class UnitTest_Business_FileHandler
    {
        [TestMethod]
        public void TestProcessFileName()
        {
            string inputString = "Configure data in Dynamics 365 model-driven apps";
            string expectedString = "configure-data-dynamics-365-model-driven-apps";

            string actualResult = string.Empty;
            FileHandler fileHandler = new FileHandler();
            actualResult = fileHandler.ProcessFileNames(inputString);

            Assert.AreEqual<string>(expectedString, actualResult);
        }
    }
}
