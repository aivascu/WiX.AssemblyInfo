using System;
using System.IO;
using NSubstitute;
using NUnit.Framework;
using Wix.AssemblyInfoExtension.Utility;

namespace Wix.AssemblyInfoExtension.Tests
{
    [TestFixture]
    public class WixAssemblyInfoPreprocessorExtensionTests
    {
        private const string fileVersionPrefix = "fileVersion";
        private const string assemblyInfoPrefix = "assemblyInfo";

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
        [TestCase(fileVersionPrefix, "ProductName", @".\Sample.TestLib.dll")]
        public void TestFileVersionFunction(string prefix, string function, string assemblyPath)
        {
            string[] args = { assemblyPath };
            var systemReflectionWrapper = Substitute.For<ISystemReflectionWrapper>();
            var pathHelper = Substitute.ForPartsOf<PathHelper>();
            var reflectionHelper = Substitute.ForPartsOf<IReflectionHelper>();
            var preprocessorExtension = new AssemblyInfoPreprocessorExtension(pathHelper, reflectionHelper, systemReflectionWrapper);

            var result = preprocessorExtension.EvaluateFunction(prefix, function, args);

            Assert.AreEqual(result, "Sample.TestLib");
        }

        [Test]
        [TestCase(fileVersionPrefix, "SomethingWrong", @".\Sample.TestLib.dll")]
        [TestCase(fileVersionPrefix, "   ", @".\Sample.TestLib.dll")]
        [TestCase(fileVersionPrefix, "", @".\Sample.TestLib.dll")]
        [TestCase(fileVersionPrefix, null, @".\Sample.TestLib.dll")]
        public void TestFileVersionFunction_Exception_WrongAttribute(string prefix, string function, string assemblyPath)
        {
            string[] args = { assemblyPath };
            string fullPath = @"C:\Sample.TestLib.dll";

            var systemReflectionWrapper = Substitute.For<ISystemReflectionWrapper>();
            var systemPathWrapper = Substitute.For<ISystemPathWrapper>();
            var pathHelper = Substitute.ForPartsOf<PathHelper>(systemPathWrapper);
            var fileVersionInfo = Substitute.ForPartsOf<FileVersionInfoWrapper>();
            var reflectionHelper = Substitute.ForPartsOf<ReflectionHelper>();
            var preprocessorExtension = new AssemblyInfoPreprocessorExtension(pathHelper, reflectionHelper, systemReflectionWrapper);

            systemPathWrapper.FileExists(Arg.Any<string>()).Returns(true);
            systemPathWrapper.GetFullPath(Arg.Any<string>()).Returns(fullPath);
            systemReflectionWrapper.GetFileVersionInfo(fullPath).Returns(fileVersionInfo);

            Assert.Throws<InvalidOperationException>(() => preprocessorExtension.EvaluateFunction(prefix, function, args), "An incorrect attribute got processed!");
            systemPathWrapper.Received().IsPathRooted(Arg.Any<string>());
        }

        [Test]
        [TestCase(fileVersionPrefix, "ProductName", "Some Wrong Path")]
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

            Assert.Throws<FileNotFoundException>(() => preprocessorExtension.EvaluateFunction(prefix, function, args), "Sample.TestLib");
        }

        [Test]
        [TestCase(assemblyInfoPrefix, "AssemblyTitle", @".\Sample.TestLib.dll", "System.Reflection.AssemblyTitleAttribute")]
        public void TestAssemblyInfoFunction(string prefix, string function, string assemblyPath, string attributeFullName)
        {
            string[] args = { assemblyPath, attributeFullName };
            var systemReflectionWrapper = Substitute.For<ISystemReflectionWrapper>();
            var pathHelper = Substitute.For<IPathHelper>();
            var reflectionHelper = Substitute.For<IReflectionHelper>();
            var preprocessorExtension = new AssemblyInfoPreprocessorExtension(pathHelper, reflectionHelper, systemReflectionWrapper);

            var result = preprocessorExtension.EvaluateFunction(prefix, function, args);

            Assert.IsNull(result, "The prefix somehow got processed");
        }
    }
}