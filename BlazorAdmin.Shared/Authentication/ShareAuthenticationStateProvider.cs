using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorAdmin.Shared.Authentication;

public abstract class ShareAuthenticationStateProvider:AuthenticationStateProvider
{
    
    private ClaimsPrincipal AnonymousUser => new(new ClaimsIdentity(Array.Empty<Claim>()));

    
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return  Task.FromResult(new AuthenticationState(AnonymousUser));
    }

    public abstract void FakedSignIn(string code, string password);

    public void FakedSignOut() {
        var result = Task.FromResult(new AuthenticationState(AnonymousUser));
        NotifyAuthenticationStateChanged(result);
    }
}
