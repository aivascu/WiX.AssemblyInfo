namespace Wix.AssemblyInfoExtension
{
    public interface IReflectionHelper
    {
        object GetPropertyValueByName(object queriedObject, string propertyName);

        object GetAssemblyAttributeInfo(string assemblyPath, string attributeTypeName);
    }
}