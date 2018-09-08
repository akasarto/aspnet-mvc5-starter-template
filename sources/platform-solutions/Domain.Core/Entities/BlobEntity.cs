using Shared.Infrastructure;
using System;

namespace Domain.Core.Entities
{
	public class BlobEntity
	{
		public long ContentLength { get; set; }
		public string ContentType { get; set; }
		public string Extension { get; set; }
		public string Folder { get; set; }
		public Guid Id { get; set; }
		public bool IsDeleted { get; set; }
		public bool IsPurged { get; set; }
		public string SourceFileName { get; set; }
		public DateTime UTCCreated { get; set; }

		public BlobInfo GetInfo() => new BlobInfo(Id, Folder, Extension);
	}
}
