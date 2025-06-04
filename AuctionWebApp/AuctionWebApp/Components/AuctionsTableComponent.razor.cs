using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AuctionWebApp.Components;

public partial class AuctionsTableComponent(IAuctionService AuctionsService, 
											NavigationManager NavigationManager, 
											ISnackbar Snackbar) : ComponentBase
{
	[Parameter]
	public AuctionFilterViewModel Filter { get; set; } = new AuctionFilterViewModel();

	[Parameter]
	public int[] RowsPerPageOptions { get; set; } = new[] { 5, 10, 20 };

	[Parameter]
	public string? NavigateUrl { get; set; }

	[Parameter]
	public string? NavigationButtonName { get; set; }


	private MudTable<PreviewAuctionViewModel> table = default!;

	public async Task Reload()
	{
		await table.ReloadServerData();
	}

	private async Task<TableData<PreviewAuctionViewModel>> LoadAuctions(TableState state, CancellationToken cancellationToken)
	{
		Filter.Page = state.Page + 1;
		Filter.PageSize = state.PageSize;

		var result = await AuctionsService.GetByFilterAsync(Filter);

		if (!result.HasErrors)
		{
			var auctions = result.Data?.Items?.ToList() ?? new List<PreviewAuctionViewModel>();
			var totalItems = result.Data?.TotalCount ?? 0;

			return new TableData<PreviewAuctionViewModel>
			{
				Items = auctions,
				TotalItems = totalItems,
			};
		}
		else
		{
			Snackbar.ShowErrors(result.Errors);
			return new TableData<PreviewAuctionViewModel>
			{
				Items = new List<PreviewAuctionViewModel>(),
				TotalItems = 0
			};
		}
	}
	private void NavigateToUrl(int? id)
	{
		if (NavigateUrl != null)
		{
			if(NavigateUrl.Contains("{id}") && id != null)
			{
				NavigateUrl = NavigateUrl.Replace("{id}", id.Value.ToString());
			}
			NavigationManager.NavigateTo(NavigateUrl);
		}
	}
}
