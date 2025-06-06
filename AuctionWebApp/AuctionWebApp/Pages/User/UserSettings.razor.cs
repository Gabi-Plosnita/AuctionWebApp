using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AuctionWebApp.Pages;

public partial class UserSettings(IUserService UserService,
							      IAuthService AuthService,
							      ISnackbar Snackbar) : ComponentBase
{
	private MudForm? _editForm;
	private UserViewModel userViewModel { get; set; } = new();
	private UpdateUserViewModel updateUserViewModel { get; set; } = new();

	private bool isLoading = true;

	private bool showErrorComponent = false;

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
		if (authenticatedUserResult.HasErrors || authenticatedUserResult.Data == null)
		{
			showErrorComponent = true;
			isLoading = false;
			return;
		}

		var userViewModelResult = await UserService.GetByIdAsync(authenticatedUserResult.Data.Id);
		if (userViewModelResult.HasErrors || userViewModelResult.Data == null)
		{
			showErrorComponent = true;
			isLoading = false;
			return;
		}

		userViewModel = userViewModelResult.Data;
		SetUpdateUserViewModel();
	}

	private void SetUpdateUserViewModel()
	{
		updateUserViewModel.FirstName = userViewModel.FirstName;
		updateUserViewModel.LastName = userViewModel.LastName;
		updateUserViewModel.Address = userViewModel.Address;
	}

	private async Task UpdateAsync()
	{
		if (_editForm is null)
			return;

		await _editForm.Validate();
		if (_editForm.IsValid)
		{
			userViewModel.FirstName = updateUserViewModel.FirstName;
			userViewModel.LastName = updateUserViewModel.LastName;
			userViewModel.Address = updateUserViewModel.Address;
			Snackbar.ShowSuccess("Profile updated successfully!");
		}
	}
}
