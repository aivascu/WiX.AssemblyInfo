using System;
using System.Linq.Expressions;

namespace Wix.AssemblyInfo.Utility
{
    public static class MemberInfoHelper
    {
        public static string GetMemberName<T>(Expression<Func<T>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            if (expression.Body is MemberExpression)
            {
                // Reference type property or field
                var memberExpression = (MemberExpression)expression.Body;
                return memberExpression.Member.Name;
            }

            throw new ArgumentException("Invalid expression");
        }
    }
}