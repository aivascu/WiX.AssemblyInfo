using System.IO;

namespace Wix.AssemblyInfoExtension.Infrastructure
{
    public class SystemPathWrapper : IPath
    {
        public bool IsPathRooted(string filePath)
        {
            return Path.IsPathRooted(filePath);
        }

        public string GetFullPath(string filePath)
        {
            return Path.GetFullPath(filePath);
        }
    }
}