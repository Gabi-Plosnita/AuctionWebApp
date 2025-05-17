using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace AuctionWebApp.Components;

public partial class LoginComponent : ComponentBase
{
	[Parameter]
	public LoginViewModel Model { get; set; } = new LoginViewModel();

	[Parameter]
	public EventCallback<LoginViewModel> OnValidSubmit { get; set; }

	protected bool ShowPassword { get; set; }

	protected async Task HandleValidSubmit()
	{
		await OnValidSubmit.InvokeAsync(Model);
	}
}
