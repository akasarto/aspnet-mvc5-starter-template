using CloudinaryDotNet;
using System;

namespace Shared.Infrastructure.Cloudinary
{
	public interface ICloudinaryThumbService : IBlobThumbService
	{
		Uri GetThumbEndpoint(string blobName, string label, bool cSubdomain = true, Transformation transformation = null);
	}
}
