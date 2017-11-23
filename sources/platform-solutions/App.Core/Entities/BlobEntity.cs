using Sarto.Infrastructure;
using System;

namespace App.Core.Entities
{
	/// <summary>
	/// Represents a blob data entity in the system.
	/// </summary>
	public class BlobEntity
	{
		/// <summary>
		/// Gets or sets the id.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the purged state.
		/// </summary>
		/// <remarks>Determines whether the physical file has been deleted as well.</remarks>
		public bool IsPurged { get; set; }

		/// <summary>
		/// Gets or sets the is deleted state.
		/// </summary>
		/// <remarks>This represents a logical deletion and can be used as a flag for purging when cleanin up space.</remarks>
		public bool IsDeleted { get; set; }

		/// <summary>
		/// Gets or sets the UTC creation date.
		/// </summary>
		public DateTime UTCCreated { get; set; }

		/// <summary>
		/// Gets or sets the name from the original file that originated this blob entry.
		/// </summary>
		public string SourceFileName { get; set; }

		/// <summary>
		/// Gets or sets the physical file content length in bytes.
		/// </summary>
		public long ContentLength { get; set; }

		/// <summary>
		/// Ges or sets the blob content type.
		/// </summary>
		public string ContentType { get; set; }

		/// <summary>
		/// Gets or sets the folder that contains the physical file in the storage service.
		/// </summary>
		public string Folder { get; set; }

		/// <summary>
		/// Gets or sets the clean extension (e.g: png).
		/// </summary>
		public string Extension { get; set; }

		/// <summary>
		/// Builds the blob info from the required fields.
		/// </summary>
		/// <returns>An instance of the <see cref="BlobInfo"/> class that represents this blob.</returns>
		public BlobInfo GetInfo() => new BlobInfo(Id, Folder, Extension);
	}
}
