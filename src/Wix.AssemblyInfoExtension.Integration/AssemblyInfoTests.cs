using NUnit.Framework;

namespace Wix.AssemblyInfoExtension.Integration
{
    [TestFixture]
    public class AssemblyInfoTests
    {
        private const string AssemblyInfoPrefix = "assemblyInfo";

        [Test]
        [TestCase(AssemblyInfoPrefix, "Title", @".\Sample.TestLib.dll", "System.Reflection.AssemblyTitleAttribute", "Sample.TestLib")]
        [TestCase(AssemblyInfoPrefix, "Title", @".\TestApp.exe", "System.Reflection.AssemblyTitleAttribute", "TestApp")]
        [TestCase(AssemblyInfoPrefix, "Test", @".\TestApp.exe", "Sample.TestLib.AssemblyTestAttribute", "Test string")]
        public void TestAssemblyInfoFunction_Library(string prefix, string function, string assemblyPath, string attributeFullName, string expectedResult)
        {
            string[] args = { assemblyPath, attributeFullName };

            var preprocessorExtension = new AssemblyInfoPreprocessorExtension();

            var result = preprocessorExtension.EvaluateFunction(prefix, function, args);

            Assert.AreEqual(result, expectedResult, "The attribute did not contain the expected value");
        }
    }
}