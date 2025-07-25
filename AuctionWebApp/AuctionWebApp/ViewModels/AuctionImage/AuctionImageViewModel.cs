﻿namespace AuctionWebApp.ViewModels;

public class AuctionImageViewModel
{
	public int Id { get; init; }

	public int AuctionId { get; init; }

	public string ImageUrl { get; init; } = string.Empty;

	public AuctionImageViewModel() { }

	public AuctionImageViewModel(AuctionImageViewModel source)
	{
		Id = source.Id;
		AuctionId = source.AuctionId;
		ImageUrl = source.ImageUrl;
	}
}
