using CloudinaryDotNet;
using System;

namespace Shared.Infrastructure.Cloudinary
{
	public class CloudinaryThumbService : ICloudinaryThumbService
	{
		private CloudinaryDotNet.Cloudinary _client = null;

		public CloudinaryThumbService(string cloudName)
		{
			cloudName = cloudName ?? throw new ArgumentNullException(nameof(cloudName), nameof(CloudinaryThumbService));

			_client = new CloudinaryDotNet.Cloudinary(
				new Account(
					cloudName
				)
			);
		}

		public Uri GetThumbEndpoint(string blobName, string label, int? width = null, int? height = null)
		{
			var blobInfo = BlobInfo.FromName(blobName);

			if (blobInfo == null)
			{
				return null;
			}

			width = width ?? 0;
			height = height ?? 0;

			var transformation = new Transformation();

			if (width > 0 && height > 0)
			{
				transformation = transformation.Width(width);
				transformation = transformation.Height(height);
			}
			else if (width > 0 || height > 0)
			{
				if (width > 0)
				{
					transformation = transformation.Width(width);
				}

				if (height > 0)
				{
					transformation = transformation.Height(height);
				}
			}

			transformation = transformation.Crop("fill");
			transformation = transformation.Gravity("faces:center");

			return GetThumbEndpoint(blobName, label, true, transformation);
		}

		public Uri GetThumbEndpoint(string blobName, string label, bool cSubdomain = true, Transformation transformation = null)
		{
			var blobInfo = BlobInfo.FromName(blobName);

			if (blobInfo == null || transformation == null)
			{
				return null;
			}

			if (blobInfo.ContentType.Contains("video"))
			{
				return new Uri(_client.Api.UrlVideoUp.CSubDomain(cSubdomain).Transform(transformation).BuildUrl(blobInfo.GetThumbnailName()), UriKind.Absolute);
			}

			return new Uri(_client.Api.UrlImgUp.CSubDomain(cSubdomain).Transform(transformation).BuildUrl(blobInfo.GetThumbnailName()), UriKind.Absolute);
		}
	}
}
