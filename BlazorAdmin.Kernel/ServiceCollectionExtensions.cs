using Microsoft.Extensions.DependencyInjection;

namespace BlazorAdmin.Kernel;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBlazorWasmServerClient(this IServiceCollection services)
    {
        return services;
    }
}
