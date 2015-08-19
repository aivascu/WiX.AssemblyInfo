using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Policy;

namespace Wix.AssemblyInfoExtension
{
    public interface IAssembly
    {
        string CodeBase { get; }
        MethodInfo EntryPoint { get; }
        string EscapedCodeBase { get; }
        Evidence Evidence { get; }
        string FullName { get; }
        bool GlobalAssemblyCache { get; }
        string Location { get; }
        event ModuleResolveEventHandler ModuleResolve;
        object CreateInstance(string typeName);
        object CreateInstance(string typeName, bool ignoreCase);
        object CreateInstance(string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes);
        bool Equals(object other);
        object[] GetCustomAttributes(bool inherit);
        object[] GetCustomAttributes(Type attributeType, bool inherit);
        Type[] GetExportedTypes();
        FileStream GetFile(string name);
        FileStream[] GetFiles();
        FileStream[] GetFiles(bool getResourceModules);
        int GetHashCode();
        Module[] GetLoadedModules();
        Module[] GetLoadedModules(bool getResourceModules);
        ManifestResourceInfo GetManifestResourceInfo(string resourceName);
        string[] GetManifestResourceNames();
        Stream GetManifestResourceStream(string name);
        Stream GetManifestResourceStream(Type type, string name);
        Module GetModule(string name);
        Module[] GetModules();
        Module[] GetModules(bool getResourceModules);
        AssemblyName GetName();
        AssemblyName GetName(bool copiedName);
        void GetObjectData(SerializationInfo info, StreamingContext context);
        AssemblyName[] GetReferencedAssemblies();
        Assembly GetSatelliteAssembly(CultureInfo culture);
        Assembly GetSatelliteAssembly(CultureInfo culture, Version version);
        Type GetType();
        Type GetType(string name);
        Type GetType(string name, bool throwOnError);
        Type GetType(string name, bool throwOnError, bool ignoreCase);
        Type[] GetTypes();
        bool IsDefined(Type attributeType, bool inherit);
        Module LoadModule(string moduleName, byte[] rawModule);
        Module LoadModule(string moduleName, byte[] rawModule, byte[] rawSymbolStore);
        string ToString();
    }
}