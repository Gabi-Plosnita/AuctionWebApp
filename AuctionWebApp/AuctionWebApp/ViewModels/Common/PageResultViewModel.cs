namespace AuctionWebApp.ViewModels;

public class PagedResultViewModel<T>
{
	public IReadOnlyList<T> Items { get; set; }
	public int TotalCount { get; set; }
	public int Page { get; set; }
	public int PageSize { get; set; }

	public int TotalPages
		=> (int)Math.Ceiling(TotalCount / (double)PageSize);

	public PagedResultViewModel(
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
