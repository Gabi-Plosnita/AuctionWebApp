﻿namespace AuctionWebApp.Models;

public class PagedResult<T>
{
	public IReadOnlyList<T> Items { get; set; }
	public int TotalCount { get; }
	public int Page { get; }
	public int PageSize { get; }

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
