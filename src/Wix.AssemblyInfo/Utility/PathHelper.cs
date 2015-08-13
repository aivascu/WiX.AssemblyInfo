using System;
using System.IO;

namespace Wix.AssemblyInfoExtension.Utility
{
    public class PathHelper : IPathHelper
    {
        private readonly ISystemPathWrapper systemPathWrapper;

        public PathHelper()
            : this(new SystemPathWrapper())
        {
        }

        public PathHelper(ISystemPathWrapper systemPathWrapper)
        {
            this.systemPathWrapper = systemPathWrapper;
        }

        public bool TryPath(string filePath, out string absolutePath)
        {
            if (filePath.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(filePath), "File name not specified");
            }

            if (!systemPathWrapper.FileExists(filePath))
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

    public interface IPathHelper
    {
        bool TryPath(string filePath, out string absolutePath);
    }
}