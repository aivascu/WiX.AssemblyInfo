using System;
using Microsoft.Tools.WindowsInstallerXml;
using Wix.AssemblyInfoExtension.Infrastructure;
using Wix.AssemblyInfoExtension.Resources;

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
                throw new ArgumentNullException(nameof(filePath), "The file path has not been specified!");
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
                var attribute = reflectionHelper.GetAssemblyAttributeInfo(absoluteFilePath, attributeTypeName);

                result = reflectionHelper.GetPropertyValueByName(attribute, function).ToString();
            }

            return result;
        }
    }
}