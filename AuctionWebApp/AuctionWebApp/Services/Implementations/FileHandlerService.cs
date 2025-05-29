using AuctionWebApp.Helpers;
using Microsoft.AspNetCore.Components.Forms;

namespace AuctionWebApp.Services;

public class FileHandlerService
{
	public Result ValidateFile(IBrowserFile file, string[] allowedExtensions, int maxSizeInMB)
	{
		var result = new Result();

		var extension = Path.GetExtension(file.Name).ToLowerInvariant();
		if (!allowedExtensions.Contains(extension))
		{
			result.Errors.Add($"File type not allowed. Only: {string.Join(", ", allowedExtensions)}");
		}

		if (file.Size > maxSizeInMB * 1024 * 1024)
		{
			result.Errors.Add($"File size must be {maxSizeInMB} MB or smaller.");
		}

		return result;
	}

	public async Task<Result<string>> GetBase64ImagePreviewAsync(IBrowserFile file, int maxSizeInMB)
	{
		var result = new Result<string>();

		try
		{
			using var stream = file.OpenReadStream(maxAllowedSize: maxSizeInMB * 1024 * 1024);
			var buffer = new byte[file.Size];
			await stream.ReadAsync(buffer);
			var base64 = Convert.ToBase64String(buffer);
			result.Data = $"data:{file.ContentType};base64,{base64}";
		}
		catch (IOException)
		{
			result.Errors.Add("Failed to process the image. Please ensure it's a valid file and not larger than the maximum allowed size.");
		}
		catch (Exception ex)
		{
			result.Errors.Add($"Unexpected error: {ex.Message}");
		}

		return result;
	}
}
