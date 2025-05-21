using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace AuctionWebApp.Components;

public partial class LoginComponent : ComponentBase
{
	[Parameter]
	public string Title { get; set; } = "Login";

	[Parameter]
	public LoginViewModel Model { get; set; } = new LoginViewModel();

	[Parameter]
	public EventCallback<LoginViewModel> OnValidSubmit { get; set; }

	private EditContext _editContext;
	private bool showSummary;
	protected bool ShowPassword { get; set; }

	protected override void OnInitialized()
	{
		_editContext = new EditContext(Model);
	}

	protected async Task HandleValidSubmit()
	{
		showSummary = false;
		await OnValidSubmit.InvokeAsync(Model);
	}

	protected void HandleInvalidSubmit()
	{
		showSummary = true;
	}
}
