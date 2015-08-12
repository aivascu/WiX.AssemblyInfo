using System;
using System.IO;

namespace Wix.AssemblyInfoExtension.Utility
{
    internal class PathHelper : IPathHelper
    {
        public bool TryPath(string filePath, out string absolutePath)
        {
            if (filePath.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(filePath), "File name not specified");
            }

            if (!FileExists(filePath))
            {
                throw new FileNotFoundException("The specified file does not exist!", filePath);
            }

            absolutePath = !IsPathRooted(filePath) ? GetFullPath(filePath) : filePath;
            return true;
        }

        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public bool IsPathRooted(string filePath)
        {
            return Path.IsPathRooted(filePath);
        }

        public string GetFullPath(string filePath)
        {
            return Path.GetFullPath(filePath);
        }
    }

    public interface IPathHelper
    {
        bool FileExists(string filePath);
        bool IsPathRooted(string filePath);
        bool TryPath(string filePath, out string absolutePath);
        string GetFullPath(string filePath);
    }
}