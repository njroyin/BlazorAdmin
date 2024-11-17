using System.Reflection;
using System.Runtime.Loader;

namespace BlazorAdmin.Shared;

public static class AssemblyHelper
{
    private static AssemblyLoadContext context;
    private static DirectoryInfo directoryInfo;

    public static Type Load(FileInfo dllFileInfo, string typeName)
    {
        directoryInfo = dllFileInfo.Directory;
        context = new AssemblyLoadContext(dllFileInfo.Name);
        context.Resolving += Context_Resolving; 
  
        using (var stream = File.OpenRead(dllFileInfo.FullName))
        {
            Assembly assembly = context.LoadFromStream(stream);
            Type type = assembly.GetType(typeName); 
            return type;
        }  
    }
 
    private static Assembly Context_Resolving(AssemblyLoadContext context, AssemblyName assemblyName)
    {
        string expectedPath = Path.Combine(directoryInfo.FullName, assemblyName.Name+".dll"); ;
        
        using (var stream = File.OpenRead(expectedPath))
        {
            return context.LoadFromStream(stream);
        }
            
    }
}
