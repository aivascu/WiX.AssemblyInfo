using System.Diagnostics;

namespace Wix.AssemblyInfoExtension.Utility
{
    public class FileVersionInfoWrapper
    {
        public FileVersionInfoWrapper()
        {
        }

        public FileVersionInfoWrapper(FileVersionInfo fileVersionInfo)
        {
            Comments = fileVersionInfo.Comments;
            CompanyName = fileVersionInfo.CompanyName;
            FileBuildPart = fileVersionInfo.FileBuildPart;
            FileDescription = fileVersionInfo.FileDescription;
            FileMajorPart = fileVersionInfo.FileMajorPart;
            FileMinorPart = fileVersionInfo.FileMinorPart;
            FileName = fileVersionInfo.FileName;
            FilePrivatePart = fileVersionInfo.FilePrivatePart;
            FileVersion = fileVersionInfo.FileVersion;
            InternalName = fileVersionInfo.InternalName;
            IsDebug = fileVersionInfo.IsDebug;
            IsPatched = fileVersionInfo.IsPatched;
            IsPrivateBuild = fileVersionInfo.IsPrivateBuild;
            IsPreRelease = fileVersionInfo.IsPreRelease;
            IsSpecialBuild = fileVersionInfo.IsSpecialBuild;
            Language = fileVersionInfo.Language;
            LegalCopyright = fileVersionInfo.LegalCopyright;
            LegalTrademarks = fileVersionInfo.LegalTrademarks;
            OriginalFilename = fileVersionInfo.OriginalFilename;
            PrivateBuild = fileVersionInfo.PrivateBuild;
            ProductBuildPart = fileVersionInfo.ProductBuildPart;
            ProductMajorPart = fileVersionInfo.ProductMajorPart;
            ProductMinorPart = fileVersionInfo.ProductMinorPart;
            ProductName = fileVersionInfo.ProductName;
            ProductPrivatePart = fileVersionInfo.ProductPrivatePart;
            ProductVersion = fileVersionInfo.ProductVersion;
            SpecialBuild = fileVersionInfo.SpecialBuild;
        }

        public static explicit operator FileVersionInfoWrapper(FileVersionInfo fileVersionInfo)
        {
            var fileInfo = new FileVersionInfoWrapper(fileVersionInfo);
            return fileInfo;
        }

        public string Comments { get; }
        public string CompanyName { get; }
        public int FileBuildPart { get; }
        public string FileDescription { get; }
        public int FileMajorPart { get; }
        public int FileMinorPart { get; }
        public string FileName { get; }
        public int FilePrivatePart { get; }
        public string FileVersion { get; }
        public string InternalName { get; }
        public bool IsDebug { get; }
        public bool IsPatched { get; }
        public bool IsPrivateBuild { get; }
        public bool IsPreRelease { get; }
        public bool IsSpecialBuild { get; }
        public string Language { get; }
        public string LegalCopyright { get; }
        public string LegalTrademarks { get; }
        public string OriginalFilename { get; }
        public string PrivateBuild { get; }
        public int ProductBuildPart { get; }
        public int ProductMajorPart { get; }
        public int ProductMinorPart { get; }
        public string ProductName { get; }
        public int ProductPrivatePart { get; }
        public string ProductVersion { get; }
        public string SpecialBuild { get; }
    }
}