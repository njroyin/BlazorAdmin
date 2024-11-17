using BlazorAdmin.Shared.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorAdmin.Kernel;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBlazorWasmServerClient(this IServiceCollection services)
    {
        //添加权限认证服务
        services.AddScoped<ShareAuthenticationStateProvider>(provider => new UserAuthenticationStateProvider() );
        services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<ShareAuthenticationStateProvider>());
        services.AddAuthorizationCore();
        return services;
    }
}
