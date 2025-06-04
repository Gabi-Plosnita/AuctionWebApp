using AuctionWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AuctionWebApp.Components;

public partial class CategorySlider : ComponentBase
{
	[Parameter]
	public List<CategoryViewModel> CategoryViewModels { get; set; } = new List<CategoryViewModel>();

	[Inject]
	protected IJSRuntime JSRuntime { get; set; } = default!;

	protected ElementReference scrollableDiv;
	protected bool disableLeftScroll = true;
	protected bool disableRightScroll = false;

	private const int ItemWidthWithMargin = 128;
	private const int ScrollAmount = ItemWidthWithMargin * 2;

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender && CategoryViewModels != null && CategoryViewModels.Count > 0)
		{
			await Task.Delay(50);
			await UpdateScrollButtonStatesAsync();
		}
		else if (firstRender)
		{
			disableLeftScroll = true;
			disableRightScroll = true;
			StateHasChanged();
		}
	}

	protected async Task ScrollLeft()
	{
		await JSRuntime.InvokeVoidAsync("categorySliderFunctions.scrollElementHorizontal", scrollableDiv, -ScrollAmount);
		await Task.Delay(350); 
		await UpdateScrollButtonStatesAsync();
	}

	protected async Task ScrollRight()
	{
		await JSRuntime.InvokeVoidAsync("categorySliderFunctions.scrollElementHorizontal", scrollableDiv, ScrollAmount);
		await Task.Delay(350);
		await UpdateScrollButtonStatesAsync();
	}

	protected async Task HandleScrollAsync()
	{
		await UpdateScrollButtonStatesAsync();
	}

	private async Task UpdateScrollButtonStatesAsync()
	{
		if (scrollableDiv.Context == null) 
		{
			return;
		}

		var scrollProps = await JSRuntime.InvokeAsync<ScrollProperties>("categorySliderFunctions.getElementScrollProperties", scrollableDiv);

		if (scrollProps == null) return;

		if (scrollProps.ScrollWidth <= scrollProps.ClientWidth)
		{
			disableLeftScroll = true;
			disableRightScroll = true;
		}
		else
		{
			disableLeftScroll = scrollProps.ScrollLeft <= 0;
			disableRightScroll = scrollProps.ScrollLeft + scrollProps.ClientWidth >= scrollProps.ScrollWidth - 1;
		}
		StateHasChanged(); 
	}

	private class ScrollProperties
	{
		public double ScrollLeft { get; set; }
		public double ScrollWidth { get; set; }
		public double ClientWidth { get; set; }
	}
}
