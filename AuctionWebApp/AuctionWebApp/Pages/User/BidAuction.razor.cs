using AuctionWebApp.Helpers;
using AuctionWebApp.Models;
using AuctionWebApp.Services;
using AuctionWebApp.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;

namespace AuctionWebApp.Pages;

public partial class BidAuction(IAuctionService AuctionService,
								IAuthService AuthService,
								ISnackbar Snackbar,
								IMapper Mapper,
								HubConnection Hub) : ComponentBase
{
	[Parameter]
	public int AuctionId { get; set; }

	private DetailedAuctionViewModel auction;

	private CreateBidViewModel bid = new();

	private bool isLoading = true;

	private bool showErrorComponent = false;

	protected override async Task OnInitializedAsync()
	{
		await InitializeAuctionAsync();
		if (showErrorComponent || !isLoading)
			return;

		await AuthenticateUserAsync();
		if (showErrorComponent || !isLoading)
			return;

		bid = new CreateBidViewModel { AuctionId = auction.Id };

		Hub.On<BidResponse>("ReceiveBid", bidResp =>
		{
			if (bidResp.AuctionId != AuctionId)
				return;

			var createdBid = Mapper.Map<BidViewModel>(bidResp);
			UpdateAuctionAfterSuccessfulBid(createdBid);

			StateHasChanged();

			/*_ = InvokeAsync(() =>
			{
				var createdBid = Mapper.Map<BidViewModel>(bidResp);
				UpdateAuctionAfterSuccessfulBid(createdBid);

				StateHasChanged(); // delete this and see if still works
			});*/
		});

		await Hub.StartAsync();
		await Hub.SendAsync("JoinAuctionGroup", AuctionId);

		isLoading = false;
	}

	private async Task SubmitBid()
	{
		var result = await AuctionService.CreateBidAsync(bid);

		if (result.HasErrors)
		{
			Snackbar.ShowErrors(result.Errors);
			return;
		}

		if (result.Data is null)
		{
			Snackbar.ShowError("Bid creation failed.");
			return;
		}

		Snackbar.ShowSuccess("Bid placed successfully!");

		UpdateAuctionAfterSuccessfulBid(result.Data);
	}

	private async Task AuthenticateUserAsync()
	{
		var authenticatedUserResult = await AuthService.GetAuthenticatedUserAsync();
		if (authenticatedUserResult.HasErrors
			|| authenticatedUserResult.Data is null
			|| authenticatedUserResult.Data.Id == auction.Seller.Id)
		{
			showErrorComponent = true;
			isLoading = false;
			return;
		}
	}

	private async Task InitializeAuctionAsync()
	{
		var auctionResult = await AuctionService.GetDetailedByIdAsync(AuctionId);
		if (auctionResult.HasErrors || auctionResult.Data is null)
		{
			showErrorComponent = true;
			isLoading = false;
			return;
		}
		auction = auctionResult.Data;
	}

	public async ValueTask DisposeAsync()
	{
		if (Hub is not null)
		{
			await Hub.SendAsync("LeaveAuctionGroup", AuctionId);
			await Hub.StopAsync();
			await Hub.DisposeAsync();
		}
	}

	private void UpdateAuctionAfterSuccessfulBid(BidViewModel createdBid)
	{
		bid = new CreateBidViewModel { AuctionId = auction.Id };
		DetailedAuctionViewModel newAuction = new DetailedAuctionViewModel(auction);
		newAuction.Bids.Add(createdBid);
		newAuction.CurrentPrice = createdBid.Amount;
		auction = newAuction;	
	}
}
