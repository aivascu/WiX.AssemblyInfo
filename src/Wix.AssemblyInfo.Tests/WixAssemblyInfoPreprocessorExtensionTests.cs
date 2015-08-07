using NUnit.Framework;

namespace Wix.AssemblyInfo.Tests
{
    [TestFixture]
    public class WixAssemblyInfoPreprocessorExtensionTests
    {
        [Test]
        [TestCase("", "", "")]
        [TestCase(null, null, null)]
        [TestCase("Wrong", "Wrong", "Wrong")]
        [TestCase("", "AssemblyTitle", @".\TestAssembly.dll")]
        [TestCase(null, "AssemblyTitle", @".\TestAssembly.dll")]
        [TestCase("   ", "AssemblyTitle", @".\TestAssembly.dll")]
        [TestCase("myWrongPrefix", "AssemblyTitle", @".\TestAssembly.dll")]
        public void TestPrefix(string prefix, string function, string assemblyPath)
        {
            var preprocessorExtension = new WixAssemblyInfoPreprocessorExtension();
            string[] args = {
                assemblyPath
            };

            var result = preprocessorExtension.EvaluateFunction(prefix, function, args);

            Assert.IsNull(result, "The prefix somehow got processed");
        }
    }
}