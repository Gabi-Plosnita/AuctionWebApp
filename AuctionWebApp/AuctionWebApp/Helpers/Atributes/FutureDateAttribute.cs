using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.Helpers;

public class FutureDateAttribute : ValidationAttribute
{
	public FutureDateAttribute()
		: base("{0} must be in the future.") { }

	public override bool IsValid(object? value)
	{
		if (value is null) return true;

		if (value is DateTime dt)
			return dt.ToUniversalTime() > DateTime.UtcNow;

		return false;
	}
}
