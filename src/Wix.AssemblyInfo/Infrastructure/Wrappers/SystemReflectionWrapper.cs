using System.Diagnostics;

namespace Wix.AssemblyInfoExtension.Infrastructure
{
    public class SystemReflectionWrapper : ISystemReflectionWrapper
    {
        public FileVersionInfoWrapper GetFileVersionInfo(string assemblyPath)
        {
            return (FileVersionInfoWrapper)FileVersionInfo.GetVersionInfo(assemblyPath);
        }
    }
}