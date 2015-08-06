using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Wix.AssemblyInfo.Utility;

namespace Wix.AssemblyInfo.Tests
{
    [TestFixture]
    public class IsNullOrEmptyExtensionTests
    {
        private IEnumerable<string> nullGenericEnumerable;
        private IEnumerable<string> emptyGenericEnumerable;
        private IEnumerable<string> singleElementGenericEnumerable;
        private IEnumerable<string> multipleElementGenericEnumerable;

        private IEnumerable nullEnumerable;
        private IEnumerable emptyEnumerable;
        private IEnumerable singleElementEnumerable;
        private IEnumerable multipleElementEnumerable;

        [TestFixtureSetUp]
        public void Setup()
        {
            nullGenericEnumerable = null;
            emptyGenericEnumerable = new List<string>();
            singleElementGenericEnumerable = new List<string> { "Test string" };
            multipleElementGenericEnumerable = new List<string>
            {
                "Test String One",
                "Test String Two",
                "Test String Three"
            };

            nullEnumerable = null;
            emptyEnumerable = new ArrayList();
            singleElementEnumerable = new ArrayList { "Test string" };
            multipleElementEnumerable = new ArrayList
            {
                "Test String One",
                "Test String Two",
                "Test String Three"
            };
        }

        [Test]
        public void EmptyList_GenericExtensionMethodCall_NoException()
        {
            var result = emptyGenericEnumerable.IsNullOrEmpty();

            Assert.IsTrue(result, "Enumerable not detected as null or empty!");
        }

        [Test]
        public void NullList_GenericExtensionMethodCall_NoException()
        {
            var result = nullGenericEnumerable.IsNullOrEmpty();

            Assert.IsTrue(result, "Enumerable not detected as null or empty!");
        }

        [Test]
        public void SingleElementList_GenericExtensionMethodCall_NoException()
        {
            var result = singleElementGenericEnumerable.IsNullOrEmpty();

            Assert.IsFalse(result, "Enumerable not detected as not null or not empty!");
        }

        [Test]
        public void MultipleElementList_GenericExtensionMethodCall_NoException()
        {
            var result = multipleElementGenericEnumerable.IsNullOrEmpty();

            Assert.IsFalse(result, "Enumerable not detected as not null or not empty!");
        }

        [Test]
        public void EmptyList_GenericClassMethodCall_NoException()
        {
            var result = IsNullOrEmptyExtension.IsNullOrEmpty<string>(emptyGenericEnumerable);

            Assert.IsTrue(result, "Enumerable not detected as null or empty!");
        }

        [Test]
        public void NullList_GenericClassMethodCall_NoException()
        {
            var result = IsNullOrEmptyExtension.IsNullOrEmpty<string>(nullGenericEnumerable);

            Assert.IsTrue(result, "Enumerable not detected as null or empty!");
        }

        [Test]
        public void SingleElementList_GenericClassMethodCall_NoException()
        {
            var result = IsNullOrEmptyExtension.IsNullOrEmpty<string>(singleElementGenericEnumerable);

            Assert.IsFalse(result, "Enumerable not detected as not null or not empty!");
        }

        [Test]
        public void MultipleElementList_GenericClassMethodCall_NoException()
        {
            var result = IsNullOrEmptyExtension.IsNullOrEmpty<string>(multipleElementGenericEnumerable);

            Assert.IsFalse(result, "Enumerable not detected as not null or not empty!");
        }

        [Test]
        public void EmptyList_ExtensionMethodCall_NoException()
        {
            var result = emptyEnumerable.IsNullOrEmpty();

            Assert.IsTrue(result, "Enumerable not detected as null or empty!");
        }

        [Test]
        public void NullList_ExtensionMethodCall_NoException()
        {
            var result = nullEnumerable.IsNullOrEmpty();

            Assert.IsTrue(result, "Enumerable not detected as null or empty!");
        }

        [Test]
        public void SingleElementList_ExtensionMethodCall_NoException()
        {
            var result = singleElementEnumerable.IsNullOrEmpty();

            Assert.IsFalse(result, "Enumerable not detected as not null or not empty!");
        }

        [Test]
        public void MultipleElementList_ExtensionMethodCall_NoException()
        {
            var result = multipleElementEnumerable.IsNullOrEmpty();

            Assert.IsFalse(result, "Enumerable not detected as not null or not empty!");
        }

        [Test]
        public void EmptyList_ClassMethodCall_NoException()
        {
            var result = IsNullOrEmptyExtension.IsNullOrEmpty(emptyEnumerable);

            Assert.IsTrue(result, "Enumerable not detected as null or empty!");
        }

        [Test]
        public void NullList_ClassMethodCall_NoException()
        {
            var result = IsNullOrEmptyExtension.IsNullOrEmpty(nullEnumerable);

            Assert.IsTrue(result, "Enumerable not detected as null or empty!");
        }

        [Test]
        public void SingleElementList_ClassMethodCall_NoException()
        {
            var result = IsNullOrEmptyExtension.IsNullOrEmpty(singleElementEnumerable);

            Assert.IsFalse(result, "Enumerable not detected as not null or not empty!");
        }

        [Test]
        public void MultipleElementList_ClassMethodCall_NoException()
        {
            var result = IsNullOrEmptyExtension.IsNullOrEmpty(multipleElementEnumerable);

            Assert.IsFalse(result, "Enumerable not detected as not null or not empty!");
        }
    }
}