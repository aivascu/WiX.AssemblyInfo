using NUnit.Framework;

namespace Wix.AssemblyInfoExtension.Integration
{
    [TestFixture]
    public class AssemblyInfoTests
    {
        private const string AssemblyInfoPrefix = "assemblyInfo";

        [Test]
        [TestCase(AssemblyInfoPrefix, "Title", @".\Sample.TestLib.dll", "System.Reflection.AssemblyTitleAttribute")]
        public void TestAssemblyInfoFunction_Library(string prefix, string function, string assemblyPath, string attributeFullName)
        {
            string[] args = { assemblyPath, attributeFullName };

            var preprocessorExtension = new AssemblyInfoPreprocessorExtension();

            var result = preprocessorExtension.EvaluateFunction(prefix, function, args);

            Assert.AreEqual(result, "Sample.TestLib", "The attribute did not contain the expected value");
        }

        [Test]
        [TestCase(AssemblyInfoPrefix, "Title", @".\TestApp.exe", "System.Reflection.AssemblyTitleAttribute")]
        public void TestAssemblyInfoFunction_Application(string prefix, string function, string assemblyPath, string attributeFullName)
        {
            string[] args = { assemblyPath, attributeFullName };

            var preprocessorExtension = new AssemblyInfoPreprocessorExtension();

            var result = preprocessorExtension.EvaluateFunction(prefix, function, args);

            Assert.AreEqual(result, "TestApp", "The attribute did not contain the expected value");
        }
    }
}