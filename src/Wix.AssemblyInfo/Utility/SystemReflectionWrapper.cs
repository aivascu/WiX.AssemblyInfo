using System.Diagnostics;

namespace Wix.AssemblyInfoExtension.Utility
{
    public class SystemReflectionWrapper : ISystemReflectionWrapper
    {
        public FileVersionInfoWrapper GetFileVersionInfo(string assemblyPath)
        {
            return (FileVersionInfoWrapper)FileVersionInfo.GetVersionInfo(assemblyPath);
        }
    }

    public interface ISystemReflectionWrapper
    {
        FileVersionInfoWrapper GetFileVersionInfo(string assemblyPath);
    }
}