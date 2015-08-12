using System;

namespace Wix.AssemblyInfoExtension.Utility
{
    internal class ReflectionHelper : IReflectionHelper
    {
        public object GetPropertyValueByName(object queriedObject, string propertyName)
        {
            var propertyInfo = queriedObject.GetType().GetProperty(propertyName);

            if (propertyInfo == null)
            {
                throw new InvalidOperationException($"Unable to find property {propertyName} in {queriedObject.GetType().FullName}");
            }

            return propertyInfo.GetValue(queriedObject, null);
        }
    }

    public interface IReflectionHelper
    {
        object GetPropertyValueByName(object queriedObject, string propertyName);
    }
}