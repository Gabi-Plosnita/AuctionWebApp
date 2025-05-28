using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace AuctionWebApp.Atributes;

[AttributeUsage(AttributeTargets.Property)]
public class AllowedBrowserExtensionsAttribute : ValidationAttribute
{
	private readonly HashSet<string> _exts;
	public AllowedBrowserExtensionsAttribute(string[] exts)
	{
		_exts = new HashSet<string>(
			exts.Select(e => e.StartsWith('.') ? e.ToLowerInvariant() : "." + e.ToLowerInvariant())
		);
	}

	protected override ValidationResult? IsValid(object? value, ValidationContext context)
	{
		var files = value as IEnumerable<IBrowserFile>;
		if (files != null)
		{
			foreach (var file in files)
			{
				var ext = Path.GetExtension(file.Name).ToLowerInvariant();
				if (!_exts.Contains(ext))
				{
					var allowed = string.Join(", ", _exts);
					var msg = ErrorMessage
						  ?? $"File extension '{ext}' is not allowed. Allowed: {allowed}.";
					return new ValidationResult(msg, new[] { context.MemberName! });
				}
			}
		}

		return ValidationResult.Success;
	}
}
