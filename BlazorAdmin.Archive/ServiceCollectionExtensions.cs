using Microsoft.Extensions.DependencyInjection;

namespace BlazorAdmin.Archive;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBlazorWasmServerClient(this IServiceCollection services)
    {
        return services;
    }
}
