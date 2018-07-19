using System;
using ImageResizer;
using Shared.Infrastructure;

namespace App.UI.Mvc5.Infrastructure
{
	public class ImageResizerThumbService : IImageResizerThumbService
	{
		public Uri GetThumbEndpoint(string blobName, string label, int? width = null, int? height = null)
		{
			var blobInfo = BlobInfo.FromName(blobName);

			if (blobInfo == null)
			{
				return null;
			}

			width = width ?? 0;
			height = height ?? 0;

			var instructions = new Instructions();

			if (width > 0 && height > 0)
			{
				instructions.Width = width;
				instructions.Height = height;
			}
			else if (width > 0 || height > 0)
			{
				if (width > 0)
				{
					instructions.Width = width;
				}

				if (height > 0)
				{
					instructions.Height = height;
				}
			}
			else
			{
				instructions.Cache = ServerCacheMode.Always;
			}

			instructions.Mode = FitMode.Crop;

			return GetThumbEndpoint(blobName, label, instructions);
		}

		public Uri GetThumbEndpoint(string blobName, string label, Instructions instructions = null)
		{
			var blobInfo = BlobInfo.FromName(blobName);

			if (blobInfo == null || instructions == null)
			{
				return null;
			}

			var uri = $"/{ImageResizerInfra.VirtualFileSystemPrefix}/{Sanitize(blobInfo.GetThumbnailName())}{instructions.ToQueryString()}";

			return new Uri(uri, UriKind.Relative);
		}

		private string Sanitize(string path) => path?.Trim('\\', '/', '~') ?? string.Empty;
	}
}