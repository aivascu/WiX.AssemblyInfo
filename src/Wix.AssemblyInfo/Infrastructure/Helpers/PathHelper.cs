using System;
using System.IO;

namespace Wix.AssemblyInfoExtension.Infrastructure
{
    public class PathHelper : IPathHelper
    {
        private readonly IPath systemPathWrapper;
        private readonly IFile systemFileWrapper;

        public PathHelper()
            : this(new SystemPathWrapper(), new SystemFileWrapper())
        {
        }

        public PathHelper(IPath systemPathWrapper, IFile systemFileWrapper)
        {
            this.systemPathWrapper = systemPathWrapper;
            this.systemFileWrapper = systemFileWrapper;
        }

        public bool TryPath(string filePath, out string absolutePath)
        {
            if (filePath.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(filePath), "File name not specified");
            }

            if (!systemFileWrapper.Exists(filePath))
            {
                throw new FileNotFoundException("The specified file does not exist!", filePath);
            }

            if (systemPathWrapper.IsPathRooted(filePath))
            {
                absolutePath = filePath;
            }
            else
            {
                absolutePath = systemPathWrapper.GetFullPath(filePath);
            }
            return true;
        }
    }
}