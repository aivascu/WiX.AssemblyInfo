using NUnit.Framework;

namespace Wix.AssemblyInfoExtension.Integration
{
    [TestFixture]
    public class FileVersionInfoTests
    {
        private const string FileVersionPrefix = "fileVersion";

        [Test]
        [TestCase(FileVersionPrefix, "ProductName", @".\Sample.TestLib.dll")]
        public void TestFileVersionFunction(string prefix, string function, string assemblyPath)
        {
            // Arrange
            string[] args = { assemblyPath };
            string productName = "Sample.TestLib";

            var preprocessorExtension = new AssemblyInfoPreprocessorExtension();

            // Act
            var result = preprocessorExtension.EvaluateFunction(prefix, function, args);

            // Assert
            Assert.AreEqual(result, productName, "Wrong product name!");
        }
    }
}