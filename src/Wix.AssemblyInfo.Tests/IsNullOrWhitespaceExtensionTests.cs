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
        }

        [Test]
        public void NullString_NoException()
        {
            string testString = null;

            var result = testString.IsNullOrWhiteSpace();

            Assert.IsTrue(result);
        }

        [Test]
        public void NotEmptyString_NoException()
        {
            string testString = "  not empty   ";

            var result = testString.IsNullOrWhiteSpace();

            Assert.IsFalse(result);
        }
    }
}