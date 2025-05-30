using AuctionWebApp.Helpers;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AuctionWebApp.Components;

public partial class AuctionsTableComponent : ComponentBase
{
	[Inject] 
	private IAuctionService AuctionsService { get; set; } = default!;

	[Inject]
	private ISnackbar Snackbar { get; set; } = default!;

	[Parameter]
	public AuctionFilterViewModel Filter { get; set; } = new AuctionFilterViewModel();

	private async Task<TableData<PreviewAuctionViewModel>> LoadAuctions(TableState state)
	{
		Filter.Page = state.Page + 1;
		Filter.PageSize = state.PageSize;

		var result = await AuctionsService.GetByFilterAsync(Filter);

		if (!result.HasErrors)
		{
			var auctions = new List<PreviewAuctionViewModel>();
			var totalItems = 0;
			if (result.Data != null)
			{
				auctions = result.Data.Items.ToList();
				totalItems = result.Data.TotalCount;
			}
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
}
