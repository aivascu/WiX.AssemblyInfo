using NUnit.Framework;
using Wix.AssemblyInfo.Utility;

namespace Wix.AssemblyInfo.Tests
{
    [TestFixture]
    public class IsNullOrWhitespaceExtensionTests
    {
        [Test]
        public void EmptyString_NoException()
        {
            string testString = "     ";

            var result = testString.IsNullOrWhiteSpace();

            Assert.IsTrue(result);
            Assert.AreEqual(testString, "     ", "The test string has changed!");
        }

        [Test]
        public void NullString_NoException()
        {
            string testString = null;

            var result = testString.IsNullOrWhiteSpace();

            Assert.IsTrue(result);
            Assert.Null(testString, "The test string has changed!");
        }

        [Test]
        public void NotEmptyString_NoException()
        {
            string testString = "  not empty   ";

            var result = testString.IsNullOrWhiteSpace();

            Assert.IsFalse(result);
            Assert.AreEqual(testString, "  not empty   ", "The test string has changed!");
        }
    }
}