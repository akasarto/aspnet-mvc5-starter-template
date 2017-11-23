using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.IO;
using System.Net;

namespace Sarto.Infrastructure.Cloudinary
{
	/// <summary>
	/// Cloudinary storage service.
	/// </summary>
	public class CloudinaryStorageService : ICloudinaryStorageService
	{
		private CloudinaryDotNet.Cloudinary _client = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		/// <param name="cloudName">Cloudinary cloud name.</param>
		/// <param name="cloudApiKey">Cloudinary api key.</param>
		/// <param name="cloudApiSecret">Cloudinary api secret.</param>
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

		/// <summary>
		/// Delete a blob.
		/// </summary>
		/// <param name="blobName">Virtual blob name.</param>
		public virtual void Delete(string blobName)
		{
			var deletion = new DeletionParams(GetPublicId(blobName));
			var result = _client.Destroy(deletion);
		}

		/// <summary>
		/// Checks if the blob exists.
		/// </summary>
		/// <param name="blobName">Virtual blob name.</param>
		/// <returns><c>True</c> or <c>false</c>.</returns>
		public virtual bool Exists(string blobName)
		{
			var resource = _client.GetResource(GetPublicId(blobName));
			return resource.StatusCode == System.Net.HttpStatusCode.OK;
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

			using (var webclient = new WebClient())
			{
				return webclient.OpenRead(GetEndpoint(blobName));
			}
		}

		/// <summary>
		/// Writes a stream to the blob.
		/// </summary>
		/// <param name="blobName">Virtual blob name.</param>
		/// <param name="blobStream">The <see cref="Stream"/> with data to be writen to the blob.</param>
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

		/// <summary>
		/// Gets the cloudinary internal public id from the blob name.
		/// </summary>
		/// <param name="blobName">The blob name to get the public id.</param>
		/// <returns>The blob name as a cloudinary infrastructure internal id.</returns>
		protected virtual string GetPublicId(string blobName) => blobName?.Replace(Path.GetExtension(blobName), string.Empty).Trim('/') ?? string.Empty;
	}
}
