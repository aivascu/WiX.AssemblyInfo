namespace Wix.AssemblyInfoExtension.Infrastructure
{
    public static class IsNullOrWhitespaceExtension
    {
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return value == null || string.IsNullOrEmpty(value.Trim());
        }
    }
}