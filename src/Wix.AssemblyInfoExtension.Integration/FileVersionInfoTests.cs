using NUnit.Framework;

namespace Wix.AssemblyInfoExtension.Integration
{
    [TestFixture]
    public class FileVersionInfoTests
    {
        private const string FileVersionPrefix = "fileVersion";

        [Test]
        [TestCase(FileVersionPrefix, "ProductName", @".\Sample.TestLib.dll", "Sample.TestLib")]
        [TestCase(FileVersionPrefix, "FileDescription", @".\Sample.TestLib.dll", "Sample.TestLib")]
        public void TestFileVersionFunction(string prefix, string function, string assemblyPath, string expectedResult)
        {
            // Arrange
            string[] args = { assemblyPath };
            var preprocessorExtension = new AssemblyInfoPreprocessorExtension();

            // Act
            var result = preprocessorExtension.EvaluateFunction(prefix, function, args);

            // Assert
            Assert.AreEqual(result, expectedResult, "Wrong product name!");
        }
    }
}