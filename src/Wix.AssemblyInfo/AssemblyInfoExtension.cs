using Microsoft.Tools.WindowsInstallerXml;

namespace Wix.AssemblyInfoExtension
{
    public class AssemblyInfoExtension : WixExtension
    {
        private readonly AssemblyInfoPreprocessorExtension versionPreprocessorExtension;

        public AssemblyInfoExtension()
        {
            versionPreprocessorExtension = new AssemblyInfoPreprocessorExtension();
        }

        public AssemblyInfoExtension(AssemblyInfoPreprocessorExtension versionPreprocessorExtension)
        {
            this.versionPreprocessorExtension = versionPreprocessorExtension;
        }

        public override PreprocessorExtension PreprocessorExtension => versionPreprocessorExtension ?? new AssemblyInfoPreprocessorExtension();
    }
}