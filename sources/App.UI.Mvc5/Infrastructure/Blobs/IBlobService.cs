using Domain.Core.Entities;
using Shared.Infrastructure;
using System;
using System.IO;

namespace App.UI.Mvc5.Infrastructure
{
	public interface IBlobService
	{
		Stream Download(BlobInfo blobInfo);

		Uri GetThumbnailEndpoint(string blobName, string label, int? width = null, int? height = null);

		BlobEntity Upload(string sourceFileName, Stream sourceFileStream, int userId);
	}
}
