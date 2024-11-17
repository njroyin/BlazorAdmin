using BlazorAdmin.Shared;
using Microsoft.AspNetCore.Builder;  

namespace BlazorAdmin.Kernel.Extensions;
 
public class ServiceBuilder : IServiceBuilder
{
    public IServiceCollection Services { get; }

    public Action<IApplicationBuilder> AppBuilder { get; set; }

    public ServiceBuilder(IServiceCollection services)
    {
        Services = services;

        //Services.AddSingleton<IMemoryCache, MemoryCache>();
        // GlobalInjection.Injection(Services); 
        GlobalApp.ServiceProvider = Services.BuildServiceProvider();
        GlobalApp.Configuration = GlobalApp.GetRequiredService<IConfiguration>();
         
    }
}