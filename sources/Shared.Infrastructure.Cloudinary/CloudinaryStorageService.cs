using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.IO;
using System.Net;

namespace Shared.Infrastructure.Cloudinary
{
	public class CloudinaryStorageService : ICloudinaryStorageService
	{
		private CloudinaryDotNet.Cloudinary _client = null;

		public CloudinaryStorageService(string cloudName, string cloudApiKey, string cloudApiSecret)
		{
			cloudName = cloudName ?? throw new ArgumentNullException(nameof(cloudName), nameof(CloudinaryStorageService));
			cloudApiKey = cloudApiKey ?? throw new ArgumentNullException(nameof(cloudApiKey), nameof(CloudinaryStorageService));
			cloudApiSecret = cloudApiSecret ?? throw new ArgumentNullException(nameof(cloudApiSecret), nameof(CloudinaryStorageService));

			_client = new CloudinaryDotNet.Cloudinary(
				new Account(
					cloud: cloudName,
					apiKey: cloudApiKey,
					apiSecret: cloudApiSecret
				)
			);
		}

		public virtual void Delete(string blobName)
		{
			var deletion = new DeletionParams(GetPublicId(blobName));
			var result = _client.Destroy(deletion);
		}

		public virtual bool Exists(string blobName)
		{
			var resource = _client.GetResource(GetPublicId(blobName));
			return resource.StatusCode == System.Net.HttpStatusCode.OK;
		}

		public virtual Uri GetEndpoint(string blobName)
		{
			var blobInfo = BlobInfo.FromName(blobName);

			if (blobInfo == null)
			{
				return null;
			}

			var contentType = blobInfo.ContentType;

			if (contentType.Contains("image"))
			{
				return new Uri(_client.Api.UrlImgUp.CSubDomain(true).UseRootPath(true).BuildUrl(blobName), UriKind.Absolute);
			}

			if (contentType.Contains("video"))
			{
				return new Uri(_client.Api.UrlVideoUp.CSubDomain(true).UseRootPath(true).BuildUrl(blobName), UriKind.Absolute);
			}

			return new Uri(_client.Api.Url.CSubDomain(true).BuildUrl(blobName), UriKind.Absolute);
		}

		public virtual Stream ReadStream(string blobName)
		{
			var blobInfo = BlobInfo.FromName(blobName);

			if (blobInfo == null)
			{
				return Stream.Null;
			}

			using (var webclient = new WebClient())
			{
				return webclient.OpenRead(GetEndpoint(blobName));
			}
		}

		public virtual void WriteStream(string blobName, Stream blobStream)
		{
			blobName = blobName ?? throw new ArgumentNullException(nameof(blobName), nameof(CloudinaryStorageService));
			blobStream = blobStream ?? throw new ArgumentNullException(nameof(blobStream), nameof(CloudinaryStorageService));

			var blobInfo = BlobInfo.FromName(blobName);

			if (blobInfo == null)
			{
				throw new ArgumentException(nameof(CloudinaryStorageService), nameof(blobName));
			}

			var rawUploadParams = new RawUploadParams();

			rawUploadParams.File = new FileDescription(blobName, blobStream);

			rawUploadParams.PublicId = GetPublicId(blobName);

			var result = _client.Upload(rawUploadParams);
		}

		protected virtual string GetPublicId(string blobName) => blobName?.Replace(Path.GetExtension(blobName), string.Empty).Trim('/') ?? string.Empty;
	}
}
