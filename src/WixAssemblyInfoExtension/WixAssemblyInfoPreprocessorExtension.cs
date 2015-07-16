using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Tools.WindowsInstallerXml;
using WixAssemblyInfoExtension.Utility;

namespace WixAssemblyInfoExtension
{
    public class WixAssemblyInfoPreprocessorExtension : PreprocessorExtension
    {
        private static readonly string[] ExtPrefixes = { "fileVersion", "assemblyInfo" };

        public override string[] Prefixes
        {
            get { return ExtPrefixes; }
        }

        public override string EvaluateFunction(string prefix, string function, string[] args)
        {
            if (string.Compare(prefix, "fileVersion", StringComparison.OrdinalIgnoreCase) == 0)
            {
                string filePath;
                if (!args.IsNullOrEmpty() && FileExists(args[0], out filePath))
                {
                    var fileVersionInfo = FileVersionInfo.GetVersionInfo(filePath);
                    return GetPropertyValueByName(fileVersionInfo, function).ToString();
                }
            }

            if (string.Compare(prefix, "assemblyInfo", StringComparison.OrdinalIgnoreCase) == 0)
            {
                string filePath;
                // Abort if parameters are invalid
                if (args.IsNullOrEmpty() || args[1].IsNullOrWhiteSpace() || !FileExists(args[0], out filePath))
                {
                    throw new ArgumentNullException(MemberInfoHelper.GetMemberName(() => args), "AssemblyInfo arguments not specified!");
                }

                var attributeType = Type.GetType(args[1]);
                if (attributeType == null || !attributeType.IsSubclassOf(typeof(Attribute)))
                {
                    throw new InvalidOperationException(string.Format("{0} is not a valid assembly Attribute!", args[1]));
                }

                var assembly = Assembly.LoadFile(filePath);
                var attribute = assembly.GetCustomAttributes(attributeType, false).FirstOrDefault();
                if (attribute == null)
                {
                    throw new InvalidOperationException(string.Format("The specified Assembly does not contain an attribute of type {0}", args[1]));
                }
                return GetPropertyValueByName(attribute, function).ToString();
            }

            return null;
        }

        private static bool FileExists(string filePath, out string absolutePath)
        {
            if (filePath.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(MemberInfoHelper.GetMemberName(() => filePath), "File name not specified");
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(string.Format("File {0} does not exist", Path.GetFileName(filePath)), filePath);
            }

            absolutePath = !Path.IsPathRooted(filePath) ? Path.GetFullPath(filePath) : filePath;
            return true;
        }

        private static object GetPropertyValueByName(object queriedObject, string propertyName)
        {
            var propertyInfo = queriedObject.GetType().GetProperty(propertyName);

            if (propertyInfo == null)
            {
                throw new InvalidOperationException(string.Format("Unable to find property {0} in {1}", propertyName, queriedObject.GetType().FullName));
            }

            return propertyInfo.GetValue(queriedObject, null);
        }
    }
}