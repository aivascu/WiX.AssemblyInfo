using System;
using System.IO;
using NSubstitute;
using NUnit.Framework;
using Wix.AssemblyInfoExtension.Utility;

namespace Wix.AssemblyInfoExtension.Tests
{
    [TestFixture]
    public class FileVersionInfoTests
    {
        private const string FileVersionPrefix = "fileVersion";
        private const string AssemblyInfoPrefix = "assemblyInfo";

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
            // Arrange
            string fullPath;
            string[] args = { assemblyPath };
            var systemReflectionWrapper = Substitute.For<ISystemReflectionWrapper>();
            var pathHelper = Substitute.For<IPathHelper>();
            var reflectionHelper = Substitute.For<IReflectionHelper>();
            var preprocessorExtension = new AssemblyInfoPreprocessorExtension(pathHelper, reflectionHelper, systemReflectionWrapper);

            // Act
            var result = preprocessorExtension.EvaluateFunction(prefix, function, args);

            // Assert
            pathHelper.DidNotReceiveWithAnyArgs().TryPath(Arg.Any<string>(), out fullPath);
            systemReflectionWrapper.DidNotReceiveWithAnyArgs().GetFileVersionInfo(Arg.Any<string>());
            reflectionHelper.DidNotReceiveWithAnyArgs().GetPropertyValueByName(Arg.Any<object>(), Arg.Any<string>());
            Assert.IsNull(result, "The prefix somehow got processed");
        }

        [Test]
        [TestCase(FileVersionPrefix, "ProductName", @".\Sample.TestLib.dll")]
        public void TestFileVersionFunction(string prefix, string function, string assemblyPath)
        {
            // Arrange
            string[] args = { assemblyPath };
            string fullPath = @"C:\Sample.TestLib.dll";
            string productName = "Sample.TestLib";

            var systemReflectionWrapper = Substitute.For<ISystemReflectionWrapper>();
            var systemPathWrapper = Substitute.For<ISystemPathWrapper>();
            var pathHelper = Substitute.ForPartsOf<PathHelper>(systemPathWrapper);
            var fileVersionInfo = Substitute.ForPartsOf<FileVersionInfoWrapper>();
            var reflectionHelper = Substitute.ForPartsOf<ReflectionHelper>();
            var preprocessorExtension = new AssemblyInfoPreprocessorExtension(pathHelper, reflectionHelper, systemReflectionWrapper);

            fileVersionInfo.ProductName = productName;
            systemPathWrapper.FileExists(assemblyPath).Returns(true);
            systemPathWrapper.GetFullPath(assemblyPath).Returns(fullPath);
            systemReflectionWrapper.GetFileVersionInfo(fullPath).Returns(fileVersionInfo);

            // Act
            var result = preprocessorExtension.EvaluateFunction(prefix, function, args);

            // Assert
            Assert.AreEqual(result, productName, "Wrong product name!");
        }

        [Test]
        [TestCase(FileVersionPrefix, "   ", @".\Sample.TestLib.dll")]
        [TestCase(FileVersionPrefix, "", @".\Sample.TestLib.dll")]
        [TestCase(FileVersionPrefix, null, @".\Sample.TestLib.dll")]
        public void TestFileVersionFunction_Exception_EmptyAttribute(string prefix, string function, string assemblyPath)
        {
            // Arrange
            string[] args = { assemblyPath };
            string fullPath = @"C:\Sample.TestLib.dll";

            var systemReflectionWrapper = Substitute.For<ISystemReflectionWrapper>();
            var systemPathWrapper = Substitute.For<ISystemPathWrapper>();
            var pathHelper = Substitute.ForPartsOf<PathHelper>(systemPathWrapper);
            var fileVersionInfo = Substitute.ForPartsOf<FileVersionInfoWrapper>();
            var reflectionHelper = Substitute.ForPartsOf<ReflectionHelper>();
            var preprocessorExtension = new AssemblyInfoPreprocessorExtension(pathHelper, reflectionHelper, systemReflectionWrapper);

            systemPathWrapper.FileExists(assemblyPath).Returns(true);
            systemPathWrapper.GetFullPath(assemblyPath).Returns(fullPath);
            systemReflectionWrapper.GetFileVersionInfo(fullPath).Returns(fileVersionInfo);

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => preprocessorExtension.EvaluateFunction(prefix, function, args), "An incorrect attribute got processed!");
        }

        [Test]
        [TestCase(FileVersionPrefix, "SomethingWrong", @".\Sample.TestLib.dll")]
        public void TestFileVersionFunction_Exception_WrongAttribute(string prefix, string function, string assemblyPath)
        {
            // Arrange
            string[] args = { assemblyPath };
            string fullPath = @"C:\Sample.TestLib.dll";

            var systemReflectionWrapper = Substitute.For<ISystemReflectionWrapper>();
            var systemPathWrapper = Substitute.For<ISystemPathWrapper>();
            var pathHelper = Substitute.ForPartsOf<PathHelper>(systemPathWrapper);
            var fileVersionInfo = Substitute.ForPartsOf<FileVersionInfoWrapper>();
            var reflectionHelper = Substitute.ForPartsOf<ReflectionHelper>();
            var preprocessorExtension = new AssemblyInfoPreprocessorExtension(pathHelper, reflectionHelper, systemReflectionWrapper);

            systemPathWrapper.FileExists(assemblyPath).Returns(true);
            systemPathWrapper.GetFullPath(assemblyPath).Returns(fullPath);
            systemReflectionWrapper.GetFileVersionInfo(fullPath).Returns(fileVersionInfo);

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => preprocessorExtension.EvaluateFunction(prefix, function, args), "An incorrect attribute got processed!");
            systemPathWrapper.Received().IsPathRooted(assemblyPath);
        }

        [Test]
        [TestCase(FileVersionPrefix, "ProductName", "Some Wrong Path")]
        public void TestFileVersionFunction_Exception_WrongFilePath(string prefix, string function, string assemblyPath)
        {
            string[] args = { assemblyPath };

            var systemReflectionWrapper = Substitute.For<ISystemReflectionWrapper>();
            var systemPathWrapper = Substitute.For<ISystemPathWrapper>();
            var pathHelper = Substitute.ForPartsOf<PathHelper>(systemPathWrapper);
            var reflectionHelper = Substitute.For<IReflectionHelper>();

            systemPathWrapper.FileExists(assemblyPath).Returns(false);
            systemPathWrapper.DidNotReceiveWithAnyArgs().GetFullPath(Arg.Any<string>());
            systemPathWrapper.DidNotReceiveWithAnyArgs().IsPathRooted(Arg.Any<string>());
            systemReflectionWrapper.DidNotReceiveWithAnyArgs().GetFileVersionInfo(Arg.Any<string>());

            var preprocessorExtension = new AssemblyInfoPreprocessorExtension(pathHelper, reflectionHelper, systemReflectionWrapper);

            var ex = Assert.Throws<FileNotFoundException>(() => preprocessorExtension.EvaluateFunction(prefix, function, args), "The path was processed!");
            Assert.That(ex.Message, Is.StringContaining("The specified file does not exist!"), "Exception thrown by another argument!");
        }

        [Test]
        [TestCase(FileVersionPrefix, "ProductName", "   ")]
        [TestCase(FileVersionPrefix, "ProductName", "")]
        [TestCase(FileVersionPrefix, "ProductName", null)]
        public void TestFileVersionFunction_Exception_EmptyPath(string prefix, string function, string assemblyPath)
        {
            string[] args = { assemblyPath };

            var systemReflectionWrapper = Substitute.For<ISystemReflectionWrapper>();
            var systemPathWrapper = Substitute.For<ISystemPathWrapper>();
            var pathHelper = Substitute.ForPartsOf<PathHelper>(systemPathWrapper);
            var reflectionHelper = Substitute.For<IReflectionHelper>();
            var preprocessorExtension = new AssemblyInfoPreprocessorExtension(pathHelper, reflectionHelper, systemReflectionWrapper);

            systemPathWrapper.FileExists(assemblyPath).Returns(false);
            systemPathWrapper.DidNotReceiveWithAnyArgs().GetFullPath(Arg.Any<string>());
            systemPathWrapper.DidNotReceiveWithAnyArgs().IsPathRooted(Arg.Any<string>());
            systemReflectionWrapper.DidNotReceiveWithAnyArgs().GetFileVersionInfo(Arg.Any<string>());

            var ex = Assert.Throws<ArgumentNullException>(() => preprocessorExtension.EvaluateFunction(prefix, function, args), "The path was processed!");
            Assert.That(ex.Message, Is.StringContaining("The file path has not been specified!"), "Exception thrown by another argument!");
        }

        [Test]
        [TestCase(AssemblyInfoPrefix, "AssemblyTitle", @".\Sample.TestLib.dll", "System.Reflection.AssemblyTitleAttribute")]
        public void TestAssemblyInfoFunction(string prefix, string function, string assemblyPath, string attributeFullName)
        {
            string[] args = { assemblyPath, attributeFullName };
            string fullPath = @"C:\Sample.TestLib.dll";

            var systemReflectionWrapper = Substitute.For<ISystemReflectionWrapper>();
            var systemPathWrapper = Substitute.For<ISystemPathWrapper>();
            var pathHelper = Substitute.For<PathHelper>(systemPathWrapper);
            var reflectionHelper = Substitute.For<IReflectionHelper>();
            var preprocessorExtension = new AssemblyInfoPreprocessorExtension(pathHelper, reflectionHelper, systemReflectionWrapper);

            systemPathWrapper.FileExists(assemblyPath).Returns(true);
            systemPathWrapper.GetFullPath(assemblyPath).Returns(fullPath);

            var result = preprocessorExtension.EvaluateFunction(prefix, function, args);

            Assert.IsNull(result, "The prefix somehow got processed");
        }
    }
}