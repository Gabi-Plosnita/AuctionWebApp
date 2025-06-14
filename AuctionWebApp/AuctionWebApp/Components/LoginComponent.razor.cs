using AuctionWebApp.Helpers;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace AuctionWebApp.Components;

public partial class LoginComponent(NavigationManager NavigationManager) : ComponentBase
{
	[Parameter]
	public string Title { get; set; } = "Login";

	[Parameter]
	public LoginViewModel Model { get; set; } = new LoginViewModel();

	[Parameter]
	public Func<LoginViewModel, Task<Result>> OnValidSubmit { get; set; }

	[Parameter]
	public string ForgotPasswordUrl { get; set; }

	[Parameter]
	public string RegisterUrl { get; set; }

	private List<string> LoginErrorMessages { get; set; } = new List<string>();

	private EditContext editContext;

	private bool showSummary;
	protected bool showPassword { get; set; }

	protected override void OnInitialized()
	{
		editContext = new EditContext(Model);
	}

	protected async Task HandleValidSubmit()
	{
		showSummary = false;
		LoginErrorMessages.Clear();
		if (OnValidSubmit != null)
		{
			var result = await OnValidSubmit(Model);
			if (result.HasErrors)
			{
				LoginErrorMessages = result.Errors;
				StateHasChanged();
			}
		}
	}

	protected void HandleInvalidSubmit()
	{
		LoginErrorMessages.Clear();
		showSummary = true;
	}

	private void OnPasswordForgotClicked()
	{
		NavigationManager.NavigateTo(ForgotPasswordUrl ?? "/");
	}

	private void OnRegisterClicked()
	{
		NavigationManager.NavigateTo(RegisterUrl ?? "/");
	}
}
