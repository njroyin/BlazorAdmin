using Microsoft.AspNetCore.Builder;

namespace BlazorAdmin.Kernel.Extensions;

public interface IServiceBuilder
{ 
        IServiceCollection Services { get; }

        Action<IApplicationBuilder> AppBuilder { get; set; }
 
}