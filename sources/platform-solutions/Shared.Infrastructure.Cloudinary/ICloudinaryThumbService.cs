using CloudinaryDotNet;
using System;

namespace Shared.Infrastructure.Cloudinary
{
	/// <summary>
	/// Specialized interface for cloudinary thumb services.
	/// </summary>
	public interface ICloudinaryThumbService : IBlobThumbService
	{
		/// <summary>
		/// Get a <see cref="Uri"/> for the specified blob name thumb.
		/// </summary>
		/// <param name="blobName">The blob to get the thumb.</param>
		/// <param name="label">The friendly label for SEO and presentation related functionaliy.</param>
		/// <param name="cSubdomain">Indicates whether the cloudinary should cycle through different subdomains for performance.</param>
		/// <param name="transformation">The transformations to apply in the resulting thumb.</param>
		/// <returns>A <see cref="Uri"/> instance for the thumb.</returns>
		Uri GetThumbEndpoint(string blobName, string label, bool cSubdomain = true, Transformation transformation = null);
	}
}