namespace Wix.AssemblyInfoExtension
{
    public interface IFileVersionInfo
    {
        string Comments { get; set; }
        string CompanyName { get; set; }
        int FileBuildPart { get; set; }
        string FileDescription { get; set; }
        int FileMajorPart { get; set; }
        int FileMinorPart { get; set; }
        string FileName { get; set; }
        int FilePrivatePart { get; set; }
        string FileVersion { get; set; }
        string InternalName { get; set; }
        bool IsDebug { get; set; }
        bool IsPatched { get; set; }
        bool IsPrivateBuild { get; set; }
        bool IsPreRelease { get; set; }
        bool IsSpecialBuild { get; set; }
        string Language { get; set; }
        string LegalCopyright { get; set; }
        string LegalTrademarks { get; set; }
        string OriginalFilename { get; set; }
        string PrivateBuild { get; set; }
        int ProductBuildPart { get; set; }
        int ProductMajorPart { get; set; }
        int ProductMinorPart { get; set; }
        string ProductName { get; set; }
        int ProductPrivatePart { get; set; }
        string ProductVersion { get; set; }
        string SpecialBuild { get; set; }
    }
}