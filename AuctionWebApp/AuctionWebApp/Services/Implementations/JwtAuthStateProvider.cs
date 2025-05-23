using AuctionWebApp.HttpClients;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using System.Security.Claims;

namespace AuctionWebApp.Services;

public class JwtAuthStateProvider : AuthenticationStateProvider, IDisposable
{
	private readonly IAuthHttpClient _authClient;
	private readonly NavigationManager _navManager;

	public JwtAuthStateProvider(IAuthHttpClient authClient, NavigationManager navManager)
	{
		_authClient = authClient;
		_navManager = navManager;
		_navManager.LocationChanged += HandleLocationChanged;
	}

	private void HandleLocationChanged(object? sender, LocationChangedEventArgs e)
	{
		NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
	}

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

	public void Dispose()
	{
		_navManager.LocationChanged -= HandleLocationChanged;
	}
}
