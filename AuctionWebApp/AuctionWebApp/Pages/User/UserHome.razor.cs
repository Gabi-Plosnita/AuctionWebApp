using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AuctionWebApp.Pages;

public partial class UserHome(IUserService UserService,
							  IAuthService AuthService,
							  ISnackbar Snackbar) : ComponentBase
{
	private UserViewModel userViewModel { get; set; } = new();

	bool isLoading = true;

	bool showErrorComponent = false;

	protected override async Task OnInitializedAsync()
	{
		await AuthenticateUserAsync();

		if (showErrorComponent || !isLoading)
			return;

		isLoading = false;
	}

	private async Task AuthenticateUserAsync()
	{
		var authenticatedUserResult = await AuthService.GetAuthenticatedUserAsync();
		if(authenticatedUserResult.HasErrors || authenticatedUserResult.Data == null)
		{
			showErrorComponent = true;
			isLoading = false;
			return;
		}

		var userViewModelResult = await UserService.GetByIdAsync(authenticatedUserResult.Data.Id);
		if(userViewModelResult.HasErrors || userViewModelResult.Data == null)
		{
			showErrorComponent = true;
			isLoading = false; 
			return;
		}

		userViewModel = userViewModelResult.Data;
	}
}
