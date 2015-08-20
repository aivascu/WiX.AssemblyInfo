using System.IO;

namespace Wix.AssemblyInfoExtension.Infrastructure
{
    public class SystemFileWrapper : IFile
    {
        public bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}