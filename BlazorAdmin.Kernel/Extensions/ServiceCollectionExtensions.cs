using BlazorAdmin.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;
using System.Text.RegularExpressions;

namespace BlazorAdmin.Kernel.Extensions;

public static class ServiceCollectionExtensions
{
    
    private static ServiceBuilder builder;

    public static IServiceBuilder AddKernel(this IServiceCollection services)
    { 
        builder = new ServiceBuilder(services); 
        
        return builder;
    }

    internal class StartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return x =>
            {
                builder.AppBuilder(x);
                next(x);
            };
        }
    }
    public static IServiceCollection AddShareBlazorWasmServerClient(this IServiceCollection services)
    {
        DirectoryInfo vDirectoryInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory); 
        string pattern = @$"{Assembly.GetEntryAssembly()?.GetName().Name}.+.dll"; 
        var fileInfos = vDirectoryInfo.EnumerateFiles()
            .Where(w => Regex.IsMatch(w.Name, pattern)).ToList();
        foreach (var fileInfo in fileInfos)
        {
            var assembly = Assembly.LoadFile(fileInfo.FullName);

            if (assembly.FullName == typeof(GlobalApp).Assembly.FullName)
            {
                continue;
            }
            

            if (assembly.GetType($@"{assembly.GetName().Name}._Imports") != null)
            {
                if (!GlobalApp.AdditionalAssemblies.Exists(e => e.FullName == assembly.FullName))
                {
                    GlobalApp.AdditionalAssemblies.Add(assembly);
                }
            }

            var type=  AssemblyHelper.Load(fileInfo, $@"{assembly.GetName().Name}.ServiceCollectionExtensions");
             
            var method = type?.GetMethod("AddBlazorWasmServerClient");
            method?.Invoke(null, [services]);
        }

        return services;
    }
}
