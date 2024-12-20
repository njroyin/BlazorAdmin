
using BlazorAdmin.Models.DbFlags;
using BlazorAdmin.Shared;
using BlazorAdmin.Shared.Authentication;
using FreeSql;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorAdmin.Platform;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBlazorWasmServerClient(this IServiceCollection services)
    {
        var connectionString = GlobalApp.Configuration.GetConnectionString("main");
        Console.WriteLine($"连接地址=======>{connectionString}");
        //添加数据库访问
        services.AddSingleton<IFreeSql<DbMain>>(provider =>
        { 
            var freeSql = new FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.SqlServer, connectionString)
                .UseMonitorCommand(cmd => Console.WriteLine($@"Sql：CommandText=>{cmd.CommandText}\r\nParameters=>{cmd.Parameters}"))
                .UseAutoSyncStructure(true) //自动同步实体结构到数据库，只有CRUD时才会生成表
                .Build<DbMain>();
            
            return freeSql; 
        });
        
        
      
        return services;
    }
}
