using System;

namespace Sample.TestLib
{
    public class AssemblyTestAttribute : Attribute
    {
        public AssemblyTestAttribute(string test)
        {
            Test = test;
        }

        public string Test { get; }
    }
}