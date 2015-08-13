using System.IO;

namespace Wix.AssemblyInfoExtension.Utility
{
    public class SystemPathWrapper : ISystemPathWrapper
    {
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

    public interface ISystemPathWrapper
    {
        bool FileExists(string filePath);

        bool IsPathRooted(string filePath);

        string GetFullPath(string filePath);
    }
}