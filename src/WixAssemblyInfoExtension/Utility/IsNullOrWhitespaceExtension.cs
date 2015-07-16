using System;

namespace WixAssemblyInfoExtension.Utility
{
    public static class IsNullOrWhitespaceExtension
    {
        public static bool IsNullOrWhiteSpace(this String value)
        {
            return value == null || string.IsNullOrEmpty(value.Trim());
        }
    }
}