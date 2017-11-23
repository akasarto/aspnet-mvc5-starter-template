using System;

namespace Sarto.Infrastructure
{
	/// <summary>
	/// Blob thumbnail service interface.
	/// </summary>
	public interface IBlobThumbService
	{
		/// <summary>
		/// Get a endpoint to access the thumbnail picture.
		/// </summary>
		/// <param name="blobName">Virtual blob name.</param>
		/// <param name="label">A readable label to associate with the thumbnail.</param>
		/// <param name="width">The expected width or null for the full size.</param>
		/// <param name="height">The expected height or null for the full size.</param>
		/// <returns>A <see cref="Uri"/> pointing to the thumbnail picture.</returns>
		Uri GetThumbEndpoint(string blobName, string label, int? width = null, int? height = null);
	}
}