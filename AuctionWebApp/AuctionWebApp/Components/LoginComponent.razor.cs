using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace AuctionWebApp.Components;

public partial class LoginComponent : ComponentBase
{
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
		_editContext.OnFieldChanged += (_, __) =>
		{
			showSummary = false;
			StateHasChanged();
		};
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
