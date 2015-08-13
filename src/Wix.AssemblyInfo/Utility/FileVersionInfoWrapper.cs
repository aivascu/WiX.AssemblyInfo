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

        public string Comments { get; set; }
        public string CompanyName { get; set; }
        public int FileBuildPart { get; set; }
        public string FileDescription { get; set; }
        public int FileMajorPart { get; set; }
        public int FileMinorPart { get; set; }
        public string FileName { get; set; }
        public int FilePrivatePart { get; set; }
        public string FileVersion { get; set; }
        public string InternalName { get; set; }
        public bool IsDebug { get; set; }
        public bool IsPatched { get; set; }
        public bool IsPrivateBuild { get; set; }
        public bool IsPreRelease { get; set; }
        public bool IsSpecialBuild { get; set; }
        public string Language { get; set; }
        public string LegalCopyright { get; set; }
        public string LegalTrademarks { get; set; }
        public string OriginalFilename { get; set; }
        public string PrivateBuild { get; set; }
        public int ProductBuildPart { get; set; }
        public int ProductMajorPart { get; set; }
        public int ProductMinorPart { get; set; }
        public string ProductName { get; set; }
        public int ProductPrivatePart { get; set; }
        public string ProductVersion { get; set; }
        public string SpecialBuild { get; set; }
    }
}