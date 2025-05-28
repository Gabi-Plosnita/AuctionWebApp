using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.Atributes;

[AttributeUsage(AttributeTargets.Property)]
public class MaxBrowserFileCountAttribute : ValidationAttribute
{
	private readonly int _maxCount;
	public MaxBrowserFileCountAttribute(int maxCount) => _maxCount = maxCount;

	protected override ValidationResult? IsValid(object? value, ValidationContext context)
	{
		var files = value as IEnumerable<IBrowserFile>;
		if (files != null && files.Count() > _maxCount)
		{
			var msg = ErrorMessage
				  ?? $"You cannot upload more than {_maxCount} files.";
			return new ValidationResult(msg, new[] { context.MemberName! });
		}

		return ValidationResult.Success;
	}
}
