using System;
using System.Linq.Expressions;
using NUnit.Framework;
using Wix.AssemblyInfo.Utility;

namespace Wix.AssemblyInfo.Tests
{
    [TestFixture]
    public class MemberInfoHelperTests
    {
        private class TestClass
        {
            public string TestMember { get; set; }

            public void TestMethod() { }
        }

        [Test]
        public void TestAttributeName()
        {
            var testObject = new TestClass();

            var attributeName = MemberInfoHelper.GetMemberName(() => testObject.TestMember);

            Assert.AreEqual(attributeName, "TestMember", "This is not the name of the field!");
        }

        [Test]
        public void TestVariableName()
        {
            var testVariable = "test value";

            var variableName = MemberInfoHelper.GetMemberName(() => testVariable);

            Assert.AreEqual(variableName, "testVariable", "This is not the name of the variable!");
        }

        [Test]
        public void TestObjectName()
        {
            var testObject = new TestClass();

            var attributeName = MemberInfoHelper.GetMemberName(() => testObject);

            Assert.AreEqual(attributeName, "testObject", "Something went wrong");
        }

        [Test]
        public void TestNullObject()
        {
            TestClass testObject = null;

            var attributeName = MemberInfoHelper.GetMemberName(() => testObject);

            Assert.AreEqual(attributeName, "testObject", "Something went wrong");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullExpression_ArgumentNullException()
        {
            Expression<Func<TestClass>> expression = null;

            var attributeName = MemberInfoHelper.GetMemberName(expression);

            Assert.AreNotEqual(attributeName, "expression", "Something went wrong");
        }
    }
}