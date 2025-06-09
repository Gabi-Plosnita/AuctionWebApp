namespace AuctionWebApp.ViewModels;

public class CategoryViewModel
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string? ImageUrl { get; set; }

	public CategoryViewModel() { }

	public CategoryViewModel(CategoryViewModel source)
	{
		Id = source.Id;
		Name = source.Name;
		ImageUrl = source.ImageUrl;
	}
}
