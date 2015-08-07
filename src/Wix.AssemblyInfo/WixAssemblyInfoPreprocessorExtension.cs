using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.Tools.WindowsInstallerXml;
using Wix.AssemblyInfo.Utility;

namespace Wix.AssemblyInfo
{
    public class WixAssemblyInfoPreprocessorExtension : PreprocessorExtension
    {
        private static readonly string[] ExtPrefixes = { ExtensionPrefixes.FileVersionPrefix, ExtensionPrefixes.AssemblyInfoPrefix };

        public override string[] Prefixes => ExtPrefixes;

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

        private static string EvaluateFileVersionInfo(string function, string filePath)
        {
            if (filePath.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(filePath), "The attribute name has not been specified!");
            }

            string absoluteFilePath;
            if (PathHelper.TryPath(filePath, out absoluteFilePath))
            {
                var fileVersionInfo = FileVersionInfo.GetVersionInfo(absoluteFilePath);
                return ReflectionHelper.GetPropertyValueByName(fileVersionInfo, function).ToString();
            }

            return string.Empty;
        }

        private static string EvaluateAssemblyInfo(string function, string filePath, string attributeTypeName)
        {
            if (attributeTypeName.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(attributeTypeName), "The attribute name has not been specified!");
            }

            string absoluteFilePath;
            if (PathHelper.TryPath(filePath, out absoluteFilePath))
            {
                var attributeType = Type.GetType(attributeTypeName);
                if (attributeType == null || !attributeType.IsSubclassOf(typeof(Attribute)))
                {
                    throw new InvalidOperationException($"{attributeTypeName} is not a valid assembly Attribute!");
                }

                var assembly = Assembly.ReflectionOnlyLoadFrom(absoluteFilePath);
                var attribute = assembly.GetCustomAttributes(attributeType, false).FirstOrDefault();
                if (attribute == null)
                {
                    throw new InvalidOperationException($"The specified Assembly does not contain an attribute of type {attributeTypeName}!");
                }
                return ReflectionHelper.GetPropertyValueByName(attribute, function).ToString();
            }

            return string.Empty;
        }
    }
}