namespace AuctionWebApp.Helpers;

public class Result
{
	public List<string> Errors { get; set; } = new List<string>();

	public bool HasErrors => Errors.Any();

	public override string ToString()
	{
		var errorMessage = string.Join(Environment.NewLine, Errors);
		return HasErrors ? errorMessage : "Result is successful with no errors.";
	}
}

public class Result<T> : Result
{
	public T? Data { get; set; }
}
