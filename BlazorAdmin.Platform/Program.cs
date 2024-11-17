using BlazorAdmin.Platform;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args); 

builder.Services.AddBootstrapBlazor();
builder.Services.AddBlazorWasmServerClient();

await builder.Build().RunAsync();