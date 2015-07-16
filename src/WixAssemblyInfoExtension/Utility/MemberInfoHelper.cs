using System;
using System.Linq.Expressions;

namespace WixAssemblyInfoExtension.Utility
{
    public static class MemberInfoHelper
    {
        public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
        {
            var expressionBody = (MemberExpression)memberExpression.Body;
            return expressionBody.Member.Name;
        }
    }
}