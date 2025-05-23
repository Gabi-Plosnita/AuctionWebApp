using AuctionWebApp.HttpClients;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace AuctionWebApp.Services;

public class JwtAuthStateProvider(IAuthHttpClient _authClient) : AuthenticationStateProvider
{
	public override async Task<AuthenticationState> GetAuthenticationStateAsync()
	{
		var result = await _authClient.GetAuthenticatedUserResponseAsync();
		if (result.HasErrors || result.Data is null)
			return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

		var user = result.Data;
		var claims = new[]
		{
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new Claim(ClaimTypes.Email, user.Email),
			new Claim(ClaimTypes.Role, user.Role)
		};
		var identity = new ClaimsIdentity(claims, "apiauth");
		return new AuthenticationState(new ClaimsPrincipal(identity));
	}

	public void NotifyAuthChanged()
		=> NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
}
