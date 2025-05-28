﻿namespace AuctionWebApp.Models;

public class PagedResult<T>
{
	public IReadOnlyList<T> Items { get; init; }
	public int TotalCount { get; init; }
	public int Page { get; init; }
	public int PageSize { get; init; }

	public int TotalPages
		=> (int)Math.Ceiling(TotalCount / (double)PageSize);

	public PagedResult(
		IReadOnlyList<T> items,
		int totalCount,
		int page,
		int pageSize)
	{
		Items = items;
		TotalCount = totalCount;
		Page = page;
		PageSize = pageSize;
	}
}
