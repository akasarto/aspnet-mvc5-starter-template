using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;

namespace Sarto.Infrastructure.Azure
{
	/// <summary>
	/// Azure storage service provider.
	/// </summary>
	public class AzureStorageService : IAzureStorageService
	{
		private string _connectionString = null;
		private string _container = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		/// <param name="connectionString">The connection string for the azure storage.</param>
		/// <param name="container">The base storage container(folder) where the blobs will be stored.</param>
		public AzureStorageService(string connectionString, string container)
		{
			_connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString), nameof(CloudStorageAccount));
			_container = container ?? throw new ArgumentNullException(nameof(container), nameof(CloudStorageAccount));
		}

		/// <summary>
		/// Delete a blob.
		/// </summary>
		/// <param name="blobName">Virtual blob name.</param>
		public virtual void Delete(string blobName)
		{
			var blockBlob = GetBlobReference(blobName);
			blockBlob.DeleteIfExists();
		}

		/// <summary>
		/// Checks if the blob exists.
		/// </summary>
		/// <param name="blobName">Virtual blob name.</param>
		/// <returns><c>True</c> or <c>false</c>.</returns>
		public virtual bool Exists(string blobName)
		{
			var blockBlob = GetBlobReference(blobName);
			return blockBlob.Exists();
		}

		/// <summary>
		/// Get the endpoint to the blob within the storage service.
		/// </summary>
		/// <param name="blobName">Virtual blob name.</param>
		/// <returns>The <see cref="Uri"/> pointing to the bob file.</returns>
		public virtual Uri GetEndpoint(string blobName)
		{
			var blobInfo = BlobInfo.FromName(blobName);

			if (blobInfo == null)
			{
				return null;
			}

			var blockBlob = GetBlobReference(blobName);

			return blockBlob.Uri;
		}

		/// <summary>
		/// Get an open stream for the blob contents.
		/// </summary>
		/// <param name="blobName">Virtual blob name.</param>
		/// <returns>The open <see cref="Stream"/> instance.</returns>
		public virtual Stream ReadStream(string blobName)
		{
			var blobInfo = BlobInfo.FromName(blobName);

			if (blobInfo == null)
			{
				return Stream.Null;
			}

			var blockBlob = GetBlobReference(blobName);

			return blockBlob.OpenRead();
		}

		/// <summary>
		/// Writes a stream to the blob.
		/// </summary>
		/// <param name="blobName">Virtual blob name.</param>
		/// <param name="blobStream">The <see cref="Stream"/> with data to be writen to the blob.</param>
		public virtual void WriteStream(string blobName, Stream blobStream)
		{
			blobName = blobName ?? throw new ArgumentNullException(nameof(blobName), nameof(AzureStorageService));
			blobStream = blobStream ?? throw new ArgumentNullException(nameof(blobStream), nameof(AzureStorageService));

			var blobInfo = BlobInfo.FromName(blobName);

			if (blobInfo == null)
			{
				throw new ArgumentException(nameof(AzureStorageService), nameof(blobName));
			}

			var blockBlob = GetBlobReference(blobName);

			blockBlob.UploadFromStream(blobStream);
		}

		/// <summary>
		/// Clean the virtual blob name.
		/// </summary>
		/// <param name="path">The blob/file path to be sanitized.</param>
		/// <returns></returns>
		protected virtual string Sanitize(string path) => path?.Trim('\\', '/', '~') ?? string.Empty;

		/// <summary>
		/// Get a reference to the blob within the azure storage infrastructure.
		/// </summary>
		/// <param name="blobName">The blob name to get a reference.</param>
		/// <returns>A <see cref="CloudBlockBlob"/> instance for the specified blob name.</returns>
		protected virtual CloudBlockBlob GetBlobReference(string blobName)
		{
			blobName = Sanitize(blobName) ?? throw new ArgumentNullException(nameof(blobName), nameof(GetBlobReference));

			var cloudStorageAccount = CloudStorageAccount.Parse(_connectionString);
			var blobClient = cloudStorageAccount.CreateCloudBlobClient();
			var container = blobClient.GetContainerReference(_container);

			if (!container.Exists())
			{
				container.Create();
				var permissions = container.GetPermissions();
				permissions.PublicAccess = BlobContainerPublicAccessType.Blob;
				container.SetPermissions(permissions);
			}

			return container.GetBlockBlobReference(blobName);
		}
	}
}
