using Microsoft.VisualStudio.TestTools.UnitTesting;
using helloLearn.Common;
using System.Collections.Generic;

namespace UnitTestHelloLearn
{
    [TestClass]
    public class TestCommonStringService
    {
        [TestMethod]
        public void TestReplaceSpaceWithDash()
        {
            string inputString = "  There is   no smoke  without fire  ";
            string expectedString = "There-is-no-smoke-without-fire";

            string actualResult = string.Empty;
            StringService targetClass = new StringService();
            targetClass.ReplaceSpaceWithDash(inputString, out actualResult);

            Assert.AreEqual<string>(expectedString, actualResult);
        }

        [TestMethod]
        public void TestConvertToLowerCase()
        {
            string inputString = "  There is   no smoke  Without fIre  ";
            string expectedString = "there is   no smoke  without fire";

            string actualResult = string.Empty;
            StringService targetClass = new StringService();
            targetClass.ConvertToLowerCase(inputString, out actualResult);

            Assert.AreEqual<string>(expectedString, actualResult);
        }

        [TestMethod]
        public void TestRemoveSmallWords()
        {
            string inputString = "get a few likes in their WeChat Moments";
            string expectedString = "get-few-likes-their-WeChat-Moments";
            List<string> smallwords = new List<string>() {"in","a","the","and","an" };

            string actualResult = string.Empty;
            StringService targetClass = new StringService();
            targetClass.RemoveSmallWords(inputString, out actualResult,smallwords);

            Assert.AreEqual<string>(expectedString, actualResult);
        }

        [TestMethod]
        public void TestGetLCS()
        {
            string str1 = "analyze-devops-continuous-planning-intergration";
            string str2 = "2-explore-continuous-planning";
            string expectedLCS = "-continuous-planning";

            string actualResult = string.Empty;
            StringService targetClass = new StringService();
            actualResult = targetClass.GetLCS(str1, str2);

            Assert.AreEqual<string>(expectedLCS, actualResult);
        }
    }
}
