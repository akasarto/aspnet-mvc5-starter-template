using System;
using System.IO;

namespace Sarto.Infrastructure
{
	/// <summary>
	/// Blob storage service provider interface.
	/// </summary>
	public interface IBlobStorageService
	{
		/// <summary>
		/// Delete a blob.
		/// </summary>
		/// <param name="blobName">Virtual blob name.</param>
		void Delete(string blobName);

		/// <summary>
		/// Checks if the blob exists.
		/// </summary>
		/// <param name="blobName">Virtual blob name.</param>
		/// <returns><c>True</c> or <c>false</c>.</returns>
		bool Exists(string blobName);

		/// <summary>
		/// Get the endpoint to the blob within the storage service.
		/// </summary>
		/// <param name="blobName">Virtual blob name.</param>
		/// <returns>The <see cref="Uri"/> pointing to the bob file.</returns>
		Uri GetEndpoint(string blobName);

		/// <summary>
		/// Get an open stream for the blob contents.
		/// </summary>
		/// <param name="blobName">Virtual blob name.</param>
		/// <returns>The open <see cref="Stream"/> instance.</returns>
		Stream ReadStream(string blobName);

		/// <summary>
		/// Writes a stream to the blob.
		/// </summary>
		/// <param name="blobName">Virtual blob name.</param>
		/// <param name="blobStream">The <see cref="Stream"/> with data to be writen to the blob.</param>
		void WriteStream(string blobName, Stream blobStream);
	}
}
