using NSubstitute;
using NUnit.Framework;
using Wix.AssemblyInfoExtension.Utility;

namespace Wix.AssemblyInfoExtension.Tests
{
    [TestFixture]
    public class AssemblyInfoTests
    {
        private const string AssemblyInfoPrefix = "assemblyInfo";

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