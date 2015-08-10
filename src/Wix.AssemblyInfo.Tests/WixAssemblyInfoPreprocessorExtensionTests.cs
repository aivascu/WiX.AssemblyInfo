using NUnit.Framework;

namespace Wix.AssemblyInfoExtension.Tests
{
    [TestFixture]
    public class WixAssemblyInfoPreprocessorExtensionTests
    {
        [Test]
        [TestCase("", "", "")]
        [TestCase(null, null, null)]
        [TestCase("Wrong", "Wrong", "Wrong")]
        [TestCase("", "System.Reflection.AssemblyTitleAttribute", @".\Sample.TestLib.dll")]
        [TestCase(null, "System.Reflection.AssemblyTitleAttribute", @".\Sample.TestLib.dll")]
        [TestCase("   ", "System.Reflection.AssemblyTitleAttribute", @".\Sample.TestLib.dll")]
        [TestCase("myWrongPrefix", "System.Reflection.AssemblyTitleAttribute", @".\Sample.TestLib.dll")]
        public void TestWrongPrefix(string prefix, string function, string assemblyPath)
        {
            var preprocessorExtension = new WixAssemblyInfoPreprocessorExtension();
            string[] args = {
                assemblyPath
            };

            var result = preprocessorExtension.EvaluateFunction(prefix, function, args);

            Assert.IsNull(result, "The prefix somehow got processed");
        }

        [Test]
        [TestCase("fileVersion", "ProductName", @".\Sample.TestLib.dll")]
        public void TestFileVersionFunction(string prefix, string function, string assemblyPath)
        {
            var preprocessorExtension = new WixAssemblyInfoPreprocessorExtension();
            string[] args = {
                assemblyPath
            };

            var result = preprocessorExtension.EvaluateFunction(prefix, function, args);

            Assert.AreEqual(result, "Sample.TestLib");
        }

        [Test]
        [TestCase("assemblyInfo", "AssemblyTitle", @".\Sample.TestLib.dll", "System.Reflection.AssemblyTitleAttribute")]
        public void TestAssemblyInfoFunction(string prefix, string function, string assemblyPath, string attributeFullName)
        {
            var preprocessorExtension = new WixAssemblyInfoPreprocessorExtension();
            string[] args = {
                assemblyPath,
                attributeFullName
            };

            var result = preprocessorExtension.EvaluateFunction(prefix, function, args);

            Assert.IsNull(result, "The prefix somehow got processed");
        }
    }
}