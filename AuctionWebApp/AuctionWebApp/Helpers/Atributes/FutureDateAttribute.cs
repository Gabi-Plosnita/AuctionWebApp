using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.Helpers;

public class FutureDateAttribute : ValidationAttribute
{
	public override bool IsValid(object value)
	{
		if (value is DateTime dt)
		{
			return dt > DateTime.UtcNow;
		}
		return true;
	}
}
