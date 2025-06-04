namespace AuctionWebApp.Layout;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using MudBlazor;

public partial class MainLayout(NavigationManager Navigation, ISnackbar Snackbar) : LayoutComponentBase, IDisposable
{
	protected override void OnInitialized()
	{
		Navigation.LocationChanged += OnLocationChanged;
	}

	private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
	{
		Snackbar.Clear();
	}

	public void Dispose()
	{
		Navigation.LocationChanged -= OnLocationChanged;
	}
}

