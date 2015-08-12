using System;
using System.IO;
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
            string[] args = {
                assemblyPath
            };
            var preprocessorExtension = new AssemblyInfoPreprocessorExtension();

            var result = preprocessorExtension.EvaluateFunction(prefix, function, args);

            Assert.IsNull(result, "The prefix somehow got processed");
        }

        [Test]
        [TestCase("fileVersion", "ProductName", @".\Sample.TestLib.dll")]
        public void TestFileVersionFunction(string prefix, string function, string assemblyPath)
        {
            string[] args = {
                assemblyPath
            };
            var preprocessorExtension = new AssemblyInfoPreprocessorExtension();

            var result = preprocessorExtension.EvaluateFunction(prefix, function, args);

            Assert.AreEqual(result, "Sample.TestLib");
        }

        [Test]
        [TestCase("fileVersion", "SomethingWrong", @".\Sample.TestLib.dll")]
        public void TestFileVersionFunction_Exception_WrongAttribute(string prefix, string function, string assemblyPath)
        {
            string[] args = {
                assemblyPath
            };
            var preprocessorExtension = new AssemblyInfoPreprocessorExtension();

            Assert.Throws<InvalidOperationException>(() => preprocessorExtension.EvaluateFunction(prefix, function, args), "An incorrect attribute got processed!");
        }

        [Test]
        [TestCase("fileVersion", "ProductName", "Some Wrong Path")]
        public void TestFileVersionFunction_Exception_WrongFilePath(string prefix, string function, string assemblyPath)
        {
            string[] args = {
                assemblyPath
            };
            var preprocessorExtension = new AssemblyInfoPreprocessorExtension();

            Assert.Throws<FileNotFoundException>(() => preprocessorExtension.EvaluateFunction(prefix, function, args), "Sample.TestLib");
        }

        [Test]
        [TestCase("assemblyInfo", "AssemblyTitle", @".\Sample.TestLib.dll", "System.Reflection.AssemblyTitleAttribute")]
        public void TestAssemblyInfoFunction(string prefix, string function, string assemblyPath, string attributeFullName)
        {
            string[] args = {
                assemblyPath,
                attributeFullName
            };
            var preprocessorExtension = new AssemblyInfoPreprocessorExtension();

            var result = preprocessorExtension.EvaluateFunction(prefix, function, args);

            Assert.IsNull(result, "The prefix somehow got processed");
        }
    }
}