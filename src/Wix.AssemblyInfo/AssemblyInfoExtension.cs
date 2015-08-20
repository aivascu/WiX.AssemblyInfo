using Microsoft.Tools.WindowsInstallerXml;

namespace Wix.AssemblyInfoExtension
{
    public class AssemblyInfoExtension : WixExtension
    {
        private readonly AssemblyInfoPreprocessorExtension assemblyInfoPreprocessorExtension;

        public AssemblyInfoExtension()
        {
            assemblyInfoPreprocessorExtension = new AssemblyInfoPreprocessorExtension();
        }

        public AssemblyInfoExtension(AssemblyInfoPreprocessorExtension assemblyInfoPreprocessorExtension)
        {
            this.assemblyInfoPreprocessorExtension = assemblyInfoPreprocessorExtension;
        }

        public override PreprocessorExtension PreprocessorExtension => assemblyInfoPreprocessorExtension ?? new AssemblyInfoPreprocessorExtension();
    }
}