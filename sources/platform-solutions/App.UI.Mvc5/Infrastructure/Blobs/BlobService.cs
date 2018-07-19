using Domain.Core.Entities;
using Domain.Core.Repositories;
using Shared.Infrastructure;
using Shared.Infrastructure.DynamicImage;
using System;
using System.IO;
using System.Linq;

namespace App.UI.Mvc5.Infrastructure
{
	public class BlobService : IBlobService
	{
		private BlobServiceConfigs _configs = null;
		private IBlobsRepository _blobsRepository = null;
		private IBlobStorageService _storageService = null;
		private IBlobThumbService _thumbService = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		public BlobService(BlobServiceConfigs configs, IBlobsRepository blobsRepository, IBlobStorageService storageService, IBlobThumbService thumbService)
		{
			_configs = configs ?? throw new ArgumentNullException(nameof(configs), nameof(BlobService));
			_blobsRepository = blobsRepository ?? throw new ArgumentNullException(nameof(blobsRepository), nameof(BlobService));
			_storageService = storageService ?? throw new ArgumentNullException(nameof(storageService), nameof(BlobService));
			_thumbService = thumbService ?? throw new ArgumentNullException(nameof(thumbService), nameof(BlobService));
		}

		public Stream Download(BlobInfo blobInfo)
		{
			if (blobInfo == null)
			{
				return Stream.Null;
			}

			return _storageService.ReadStream(blobInfo.Name);
		}

		public Uri GetThumbnailEndpoint(string blobName, string label, int? width = null, int? height = null)
		{
			var blobInfo = BlobInfo.FromName(blobName) ?? new BlobInfo("placeholder.png");

			// ToDo: Cache verification.
			if (!_storageService.Exists(blobInfo.Name))
			{
				label = (label?.First().ToString() ?? "?").ToUpper();
				return BuildUriBase64(blobInfo, label, width, height);
			}

			if (!blobInfo.ContentType.Contains("image"))
			{
				var background = _configs.DefaultThumbBackgroundHexColor;
				var foreground = _configs.DefaultThumbForegroundHexColor;

				switch (blobInfo.Extension)
				{
					case "pdf":
						background = "#e81f01";
						foreground = "#FFFFFF";
						break;

					case "zip":
						background = "#FFE693";
						foreground = "#afafaf";
						break;
				}

				var docLabel = $".{blobInfo.Extension.Trim('.')}";

				var thumbInfo = BlobInfo.FromName(blobInfo.GetThumbnailName());

				return BuildUriBase64(thumbInfo, docLabel, width, height, background, foreground);
			}

			return _thumbService.GetThumbEndpoint(blobName, label, width, height);
		}

		public BlobEntity Upload(string sourceFileName, Stream sourceFileStream, int userId)
		{
			var blobInfo = new BlobInfo(sourceFileName);
			var contentLength = sourceFileStream.Length;

			var blobEntity = new BlobEntity()
			{
				Id = blobInfo.Id,
				Folder = blobInfo.Folder,
				Extension = blobInfo.Extension,
				ContentType = blobInfo.ContentType,
				ContentLength = contentLength,
				SourceFileName = sourceFileName,
				UTCCreated = DateTime.UtcNow
			};

			_storageService.WriteStream(blobInfo.Name, sourceFileStream);

			blobEntity =_blobsRepository.Create(blobEntity, userId);

			return blobEntity;
		}

		private Uri BuildUriBase64(BlobInfo blobInfo, string label, int? width = null, int? height = null, string bgColor = null, string fgColor = null)
		{
			var dynamicTextImage = new DynamicTextImage(
				width ?? 500,
				height ?? 500,
				bgColor ?? _configs.DefaultThumbBackgroundHexColor,
				fgColor ?? _configs.DefaultThumbForegroundHexColor
			);

			using (var imageStream = dynamicTextImage.Build(label))
			{
				var encodedData = Convert.ToBase64String(imageStream.ToArray());
				var embeddedUri = $"data:{blobInfo.ContentType};base64,{encodedData}";
				return new Uri(embeddedUri, UriKind.Absolute);
			}
		}
	}
}
