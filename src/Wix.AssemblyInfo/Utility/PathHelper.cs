using System;
using System.IO;

namespace Wix.AssemblyInfoExtension.Utility
{
    internal static class PathHelper
    {
        public static bool TryPath(string filePath, out string absolutePath)
        {
            if (filePath.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(filePath), "File name not specified");
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The specified file does not exist!", filePath);
            }

            absolutePath = !Path.IsPathRooted(filePath) ? Path.GetFullPath(filePath) : filePath;
            return true;
        }
    }
}