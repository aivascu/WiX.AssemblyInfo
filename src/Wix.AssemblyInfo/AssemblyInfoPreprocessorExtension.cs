using System;
using System.Linq;
using System.Reflection;
using Microsoft.Tools.WindowsInstallerXml;
using Wix.AssemblyInfoExtension.Utility;

namespace Wix.AssemblyInfoExtension
{
    public class AssemblyInfoPreprocessorExtension : PreprocessorExtension
    {
        private static readonly string[] ExtPrefixes = { ExtensionPrefixes.FileVersionPrefix, ExtensionPrefixes.AssemblyInfoPrefix };
        private readonly IPathHelper pathHelper;
        private readonly IReflectionHelper reflectionHelper;
        private readonly ISystemReflectionWrapper systemReflectionWrapper;

        public override string[] Prefixes => ExtPrefixes;

        public AssemblyInfoPreprocessorExtension()
            : this(new PathHelper(), new ReflectionHelper(), new SystemReflectionWrapper())
        {
        }

        public AssemblyInfoPreprocessorExtension(IPathHelper pathHelper, IReflectionHelper reflectionHelper, ISystemReflectionWrapper systemReflectionWrapper)
        {
            this.systemReflectionWrapper = systemReflectionWrapper;
            this.pathHelper = pathHelper;
            this.reflectionHelper = reflectionHelper;
        }

        public override string EvaluateFunction(string prefix, string function, string[] args)
        {
            if (string.Compare(prefix, ExtensionPrefixes.FileVersionPrefix, StringComparison.OrdinalIgnoreCase) == 0)
            {
                return EvaluateFileVersionInfo(function, args[0]);
            }

            if (string.Compare(prefix, ExtensionPrefixes.AssemblyInfoPrefix, StringComparison.OrdinalIgnoreCase) == 0)
            {
                return EvaluateAssemblyInfo(function, args[0], args[1]);
            }

            return null;
        }

        private string EvaluateFileVersionInfo(string function, string filePath)
        {
            if (filePath.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(filePath), "The attribute name has not been specified!");
            }

            if (function.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(function), "The attribute name has not been specified!");
            }

            string absoluteFilePath;
            string result = string.Empty;
            if (pathHelper.TryPath(filePath, out absoluteFilePath))
            {
                var fileVersionInfo = systemReflectionWrapper.GetFileVersionInfo(absoluteFilePath);
                result = reflectionHelper.GetPropertyValueByName(fileVersionInfo, function).ToString();
            }

            return result;
        }

        private string EvaluateAssemblyInfo(string function, string filePath, string attributeTypeName)
        {
            if (attributeTypeName.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(attributeTypeName), "The attribute name has not been specified!");
            }

            string absoluteFilePath;
            string result = string.Empty;
            if (pathHelper.TryPath(filePath, out absoluteFilePath))
            {
                var assembly = Assembly.ReflectionOnlyLoadFrom(absoluteFilePath);

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

                result = reflectionHelper.GetPropertyValueByName(attribute, function).ToString();
            }

            return result;
        }
    }
}