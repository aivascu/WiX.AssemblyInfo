namespace Wix.AssemblyInfoExtension
{
    public interface IPathHelper
    {
        bool TryPath(string filePath, out string absolutePath);
    }
}