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

            //TODO Check if there are any referenced assemblies
            var dependencies = assembly.GetReferencedAssemblies();
            var nonSystemDependencies = (from dependency in dependencies
                                         where dependency.Name != "<In Memory Module>"
                                         && !dependency.FullName.StartsWith("System")
                                         && !dependency.FullName.StartsWith("Microsoft")
                                         && !dependency.FullName.Contains("CppCodeProvider")
                                         && !dependency.FullName.Contains("WebMatrix")
                                         && !dependency.FullName.Contains("SMDiagnostics")
                                         && !dependency.CodeBase.IsNullOrWhiteSpace()
                                         select dependency).ToList();

            foreach (var dependency in nonSystemDependencies)
            {
                //TODO load dependencies to the app domain
                Assembly.Load(systemPathWrapper.GetFullPath($"{dependency.CodeBase}{dependency.Name}.dll"));
            }

            Type attributeType;
            if (attributeTypeName.StartsWith("System") || attributeTypeName.StartsWith("Microsoft"))
            {
                attributeType = Type.GetType(attributeTypeName);
            }
            else
            {
                attributeType = assembly.GetType(attributeTypeName);
            }

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