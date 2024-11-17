using BlazorAdmin.Kernel.Extensions;
using BlazorAdmin.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddKernel(); 

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddBootstrapBlazor();

builder.Services.AddKernel();
 
builder.Services.AddShareBlazorWasmServerClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}


app.UseStaticFiles(); 
app.UseAntiforgery();


app.MapRazorComponents<BlazorAdmin.Components.App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies([..GlobalApp.AdditionalAssemblies]);


app.Run();