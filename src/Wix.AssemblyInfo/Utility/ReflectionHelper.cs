using System;

namespace Wix.AssemblyInfo.Utility
{
    internal static class ReflectionHelper
    {
        public static object GetPropertyValueByName(object queriedObject, string propertyName)
        {
            var propertyInfo = queriedObject.GetType().GetProperty(propertyName);

            if (propertyInfo == null)
            {
                throw new InvalidOperationException($"Unable to find property {propertyName} in {queriedObject.GetType().FullName}");
            }

            return propertyInfo.GetValue(queriedObject, null);
        }
    }
}