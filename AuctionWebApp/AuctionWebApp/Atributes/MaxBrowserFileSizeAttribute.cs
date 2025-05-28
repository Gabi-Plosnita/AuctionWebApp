using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.Atributes;

[AttributeUsage(AttributeTargets.Property)]
public class MaxBrowserFileSizeAttribute : ValidationAttribute
{
	private readonly long _maxBytes;
	public MaxBrowserFileSizeAttribute(long maxBytes) => _maxBytes = maxBytes;

	protected override ValidationResult? IsValid(object? value, ValidationContext context)
	{
		var files = value as IEnumerable<IBrowserFile>;
		if (files != null)
		{
			var tooBig = files.FirstOrDefault(f => f.Size > _maxBytes);
			if (tooBig != null)
			{
				var maxMb = _maxBytes / 1024 / 1024;
				var msg = ErrorMessage
					  ?? $"Each file must be {maxMb} MB or smaller.";
				return new ValidationResult(msg, new[] { context.MemberName! });
			}
		}

		return ValidationResult.Success;
	}
}

