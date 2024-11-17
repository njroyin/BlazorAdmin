using Microsoft.Extensions.Hosting;

namespace BlazorAdmin.Kernel.Extensions;

public static class HostBuilderExtensions
{
    public static IHostBuilder AddKernel(this IHostBuilder hostBuilder, Action<IConfigurationBuilder> action = null)
    { 
        return hostBuilder.ConfigureAppConfiguration((hostingContext, config) =>
        {
            action?.Invoke(config);
        }).UseServiceProviderFactory(new ServiceProviderFactory());
    }
}