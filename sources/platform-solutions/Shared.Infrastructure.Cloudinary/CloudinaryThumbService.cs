using CloudinaryDotNet;
using System;

namespace Shared.Infrastructure.Cloudinary
{
	/// <summary>
	/// Cloudinary thumb service.
	/// </summary>
	public class CloudinaryThumbService : ICloudinaryThumbService
	{
		private CloudinaryDotNet.Cloudinary _client = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		/// <param name="cloudName">The cloudinary account cloud name.</param>
		public CloudinaryThumbService(string cloudName)
		{
			cloudName = cloudName ?? throw new ArgumentNullException(nameof(cloudName), nameof(CloudinaryThumbService));

			_client = new CloudinaryDotNet.Cloudinary(
				new Account(
					cloudName
				)
			);
		}

		/// <summary>
		/// Get a endpoint to access the thumbnail picture.
		/// </summary>
		/// <param name="blobName">Virtual blob name.</param>
		/// <param name="label">A readable label to associate with the thumbnail.</param>
		/// <param name="width">The expected width or null for the full size.</param>
		/// <param name="height">The expected height or null for the full size.</param>
		/// <returns>A <see cref="Uri"/> pointing to the thumbnail picture.</returns>
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

		/// <summary>
		/// Get a <see cref="Uri"/> for the specified blob name thumb.
		/// </summary>
		/// <param name="blobName">The blob to get the thumb.</param>
		/// <param name="label">The friendly label for SEO and presentation related functionaliy.</param>
		/// <param name="cSubdomain">Indicates whether the cloudinary should cycle through different subdomains for performance.</param>
		/// <param name="transformation">The transformations to apply in the resulting thumb.</param>
		/// <returns>A <see cref="Uri"/> instance for the thumb.</returns>
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