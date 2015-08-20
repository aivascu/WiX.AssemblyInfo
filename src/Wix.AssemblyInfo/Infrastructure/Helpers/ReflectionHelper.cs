using System;
using System.Linq;
using System.Reflection;

namespace Wix.AssemblyInfoExtension.Infrastructure
{
    public class ReflectionHelper : IReflectionHelper
    {
        private readonly IPath systemPathWrapper;

        public ReflectionHelper()
            : this(new SystemPathWrapper())
        {
        }

        public ReflectionHelper(SystemPathWrapper systemPathWrapper)
        {
            this.systemPathWrapper = systemPathWrapper;
        }

        public object GetPropertyValueByName(object queriedObject, string propertyName)
        {
            var propertyInfo = queriedObject.GetType().GetProperty(propertyName);

            if (propertyInfo == null)
            {
                throw new InvalidOperationException($"Unable to find property {propertyName} in {queriedObject.GetType().FullName}");
            }

            return propertyInfo.GetValue(queriedObject, null);
        }

        public object GetAssemblyAttributeInfo(string assemblyPath, string attributeTypeName)
        {
            var assembly = Assembly.LoadFrom(assemblyPath);
            var referencedAssemblies = assembly.GetReferencedAssemblies()
                .Select(a => Assembly.Load(a.FullName));

            var attributeType = referencedAssemblies
                    .Select(a => a.GetTypes()
                        .FirstOrDefault(t => t.FullName.Equals(attributeTypeName)))
                    .FirstOrDefault(t => t != null);

            if (attributeType == null || !attributeType.IsSubclassOf(typeof(Attribute)))
            {
                throw new InvalidOperationException($"{attributeTypeName} is not a valid assembly Attribute!");
            }

            var attribute = assembly.GetCustomAttributes(attributeType, false).FirstOrDefault();

            if (attribute == null)
            {
                throw new InvalidOperationException($"The specified Assembly does not contain an attribute of type {attributeTypeName}!");
            }

            return attribute;
        }
    }
}