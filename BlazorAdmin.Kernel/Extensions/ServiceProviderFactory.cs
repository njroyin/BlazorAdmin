using BlazorAdmin.Shared;

namespace BlazorAdmin.Kernel.Extensions;

public class ServiceProviderFactory : IServiceProviderFactory<IServiceProvider>
{
    public IServiceProvider CreateBuilder(IServiceCollection services)
    {
        GlobalApp.ServiceProvider = services.BuildServiceProvider();
        GlobalApp.Configuration = GlobalApp.GetRequiredService<IConfiguration>();
        return GlobalApp.ServiceProvider;
    }

    public IServiceProvider CreateServiceProvider(IServiceProvider containerBuilder)
    {
        return containerBuilder;
    }
}