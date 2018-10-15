using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;

namespace Shared.Infrastructure.Azure
{
	public class AzureStorageService : IAzureStorageService
	{
		private readonly string _connectionString = null;
		private readonly string _container = null;

		public AzureStorageService(string connectionString, string container)
		{
			_connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString), nameof(CloudStorageAccount));
			_container = container ?? throw new ArgumentNullException(nameof(container), nameof(CloudStorageAccount));
		}

		public virtual void Delete(string blobName)
		{
			var blockBlob = GetBlobReference(blobName);
			blockBlob.DeleteIfExists();
		}

		public virtual bool Exists(string blobName)
		{
			var blockBlob = GetBlobReference(blobName);
			return blockBlob.Exists();
		}

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

		protected virtual string Sanitize(string path) => path?.Trim('\\', '/', '~') ?? string.Empty;
	}
}
