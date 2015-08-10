﻿using Microsoft.Tools.WindowsInstallerXml;

namespace Wix.AssemblyInfoExtension
{
    public class WixAssemblyInfoExtension : WixExtension
    {
        private readonly WixAssemblyInfoPreprocessorExtension versionPreprocessorExtension;

        public WixAssemblyInfoExtension()
        {
            versionPreprocessorExtension = new WixAssemblyInfoPreprocessorExtension();
        }

        public WixAssemblyInfoExtension(WixAssemblyInfoPreprocessorExtension versionPreprocessorExtension)
        {
            this.versionPreprocessorExtension = versionPreprocessorExtension;
        }

        public override PreprocessorExtension PreprocessorExtension => versionPreprocessorExtension ?? new WixAssemblyInfoPreprocessorExtension();
    }
}