using NSubstitute;
using NUnit.Framework;
using Wix.AssemblyInfoExtension.Infrastructure;

namespace Wix.AssemblyInfoExtension.Tests
{
    [TestFixture]
    public class AssemblyInfoTests
    {
        private const string AssemblyInfoPrefix = "assemblyInfo";

        [Test]
        [TestCase(AssemblyInfoPrefix, "Title", @".\Sample.TestLib.dll", "System.Reflection.AssemblyTitleAttribute")]
        public void TestAssemblyInfoFunction(string prefix, string function, string assemblyPath, string attributeFullName)
        {
            string[] args = { assemblyPath, attributeFullName };
            string fullPath = @"C:\Sample.TestLib.dll";
            string assemblyTitle = "Sample.TestLib";
            object assemblyInfo = new
            {
                Title = assemblyTitle
            };

            var systemReflectionWrapper = Substitute.For<ISystemReflectionWrapper>();
            var systemPathWrapper = Substitute.For<IPath>();
            var systemFileWrapper = Substitute.For<IFile>();
            var pathHelper = Substitute.For<PathHelper>(systemPathWrapper, systemFileWrapper);
            var reflectionHelper = Substitute.ForPartsOf<ReflectionHelper>(systemPathWrapper);
            var preprocessorExtension = new AssemblyInfoPreprocessorExtension(pathHelper, reflectionHelper, systemReflectionWrapper);

            reflectionHelper.GetAssemblyAttributeInfo(fullPath, attributeFullName).Returns(assemblyInfo);
            systemFileWrapper.Exists(assemblyPath).Returns(true);
            systemPathWrapper.GetFullPath(assemblyPath).Returns(fullPath);

            var result = preprocessorExtension.EvaluateFunction(prefix, function, args);

            Assert.AreEqual(result, assemblyTitle, "The attribute value has been changed during the execution!");
        }
    }
}