using Domain.Core.Entities;
using Shared.Infrastructure;

namespace App.UI.Mvc5.Infrastructure
{
	public class BlobUploadResult
	{
		public BlobUploadResult(BlobEntity blobEntity, BlobInfo blobInfo)
		{
			Entity = blobEntity;
			Info = blobInfo;
		}

		public BlobEntity Entity { get; private set; }
		public BlobInfo Info { get; private set; }
	}
}
