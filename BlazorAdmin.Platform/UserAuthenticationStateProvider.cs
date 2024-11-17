using BlazorAdmin.Shared;
using BlazorAdmin.Platform.Models;
using BlazorAdmin.Shared.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorAdmin.Platform;
 
/// <summary>
/// 权限认证
/// </summary>
public class UserAuthenticationStateProvider : ShareAuthenticationStateProvider
{
    /// <summary>
    /// 定义访问指定数据连接
    /// </summary>
    private IFreeSql<DbMainFlag> freeSql = GlobalApp.GetService<IFreeSql<DbMainFlag>>();
     
 

   // private ClaimsPrincipal FakedUser {
    //     get {
    //         var claims = new[] {
    //             new Claim(ClaimTypes.Name, "john"),
    //             new Claim(ClaimTypes.Role, "user"), 
    //             
    //         };
    //         var identity = new ClaimsIdentity(claims, "faked");
    //         return new ClaimsPrincipal(identity);
    //     }
    // }

    private ClaimsPrincipal BuildUser(User user)
    {
        var claims = new[] {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, "user"), 
            new Claim(ClaimTypes.Sid, $"{user.Id}"), 
        };
        var identity = new ClaimsIdentity(claims, "faked");
        return new ClaimsPrincipal(identity);
    }

    public override void FakedSignIn(string code,string password)
    {  
       var user = freeSql.Select<User>().Where(w => w.Code == code).ToOne();

       if (user == null)
       {
           throw new Exception("未找到用户");
       }

       if (user.Password != password)
       {
           throw new Exception("密码错误");
       }

       var result = Task.FromResult(new AuthenticationState(BuildUser(user)));
        NotifyAuthenticationStateChanged(result);
    }
    
   
  
 
}
