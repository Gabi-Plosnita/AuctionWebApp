namespace AuctionWebApp.Helpers;

public static class DateHelper
{
	public static string GetRelativeTime(DateTime dateTime)
	{
		var span = DateTime.UtcNow - dateTime.ToUniversalTime();
		if (span.TotalMinutes < 60)
			return $"{(int)span.TotalMinutes} min ago";
		if (span.TotalHours < 24)
			return $"{(int)span.TotalHours} h ago";
		if (span.TotalDays < 7)
			return $"{(int)span.TotalDays} day{(span.TotalDays >= 2 ? "s" : "")} ago";
		return dateTime.ToString("d MMM yyyy");
	}
}
