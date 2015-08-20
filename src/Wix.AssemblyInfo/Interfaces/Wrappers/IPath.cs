namespace Wix.AssemblyInfoExtension
{
    public interface IPath
    {
        bool IsPathRooted(string filePath);

        string GetFullPath(string filePath);
    }
}