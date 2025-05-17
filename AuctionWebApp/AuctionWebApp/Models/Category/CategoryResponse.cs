﻿namespace AuctionWebApp.Models;

public record CategoryResponse
{
	public int Id { get; init; }
	public string Name { get; init; } = string.Empty;
}
