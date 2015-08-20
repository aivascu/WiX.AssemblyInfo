using Wix.AssemblyInfoExtension.Infrastructure;

namespace Wix.AssemblyInfoExtension
{
    public interface ISystemReflectionWrapper
    {
        FileVersionInfoWrapper GetFileVersionInfo(string assemblyPath);
    }
}