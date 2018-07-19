using Shared.Extensions;
using System;
using System.IO;

/// <summary>
/// Shared infrastructure classes.
/// </summary>
namespace Shared.Infrastructure
{
	/// <summary>
	/// Represents a blob definition on the system.
	/// </summary>
	/// <remarks>This class is responsible for centralizing related information like name and content type.</remarks>
	public sealed class BlobInfo
	{
		private Guid _id;
		private string _folder;
		private string _cleanExtension;

		/// <summary>
		/// Constructor method.
		/// </summary>
		/// <param name="sourceFileName">The file name to build the blob from.</param>
		public BlobInfo(string sourceFileName) : this(Guid.NewGuid(), BuildFolder(), GetCleanExtension(sourceFileName))
		{
		}

		/// <summary>
		/// Constructor method.
		/// </summary>
		/// <remarks>Normaly used to build instances from stored data.</remarks>
		/// <param name="id">The blob id.</param>
		/// <param name="folder">The blob folder.</param>
		/// <param name="cleanExtension">The blob extension only.</param>
		public BlobInfo(Guid id, string folder, string cleanExtension)
		{
			_id = id;

			if (_id == Guid.Empty)
			{
				throw new ArgumentException(nameof(BlobInfo), nameof(id));
			}

			_folder = folder ?? throw new ArgumentNullException(nameof(folder), nameof(BlobInfo));
			_cleanExtension = cleanExtension ?? throw new ArgumentNullException(nameof(cleanExtension), nameof(BlobInfo));

			if (_cleanExtension.Contains("."))
			{
				throw new ArgumentException($"Dirty extension detected: {_cleanExtension}", $"{nameof(BlobInfo)} => {nameof(cleanExtension)}");
			}
		}

		/// <summary>
		/// Gets the id.
		/// </summary>
		public Guid Id => _id;

		/// <summary>
		/// Gets the folder.
		/// </summary>
		public string Folder => _folder;

		/// <summary>
		/// Gets the content type.
		/// </summary>
		public string ContentType => MimeTypeMap.GetMimeType(extension: GetCleanExtension(Name));

		/// <summary>
		/// Gets the extension.
		/// </summary>
		public string Extension => _cleanExtension;

		/// <summary>
		/// Gets the virtual file name.
		/// </summary>
		/// <remarks>This is the value used by the application to access the blob.</remarks>
		public string Name => string.Concat("/", Sanitize($@"{Folder}/{Id}.{Extension}"));

		/// <summary>
		/// Convert this instance to a string representation.
		/// </summary>
		/// <returns>The string representation for this instance.</returns>
		public override string ToString() => Name;

		/// <summary>
		/// Build a new <see cref="BlobInfo"/> instance from a virtual blob name.
		/// </summary>
		/// <param name="blobName">The virtual blob name.</param>
		/// <returns>A <see cref="BlobInfo"/> instance or null.</returns>
		public static BlobInfo FromName(string blobName)
		{
			if (string.IsNullOrWhiteSpace(blobName) || Path.IsPathRooted(Sanitize(blobName)))
			{
				return null;
			}

			var id = Guid.Empty;
			var rawId = GetRawId(blobName: blobName);

			if (!Guid.TryParse(rawId, out id))
			{
				return null;
			}

			var name = blobName ?? string.Empty;
			var container = GetContainer(blobName);
			var cleanExtension = GetCleanExtension(blobName);

			return new BlobInfo(id, container, cleanExtension);
		}

		/// <summary>
		/// Gets the thumbnail name for the blob.
		/// </summary>
		/// <remarks>This is required when generating thumbnails for non image content types.</remarks>
		/// <param name="defaultExtension">The default extension expected for non image files.</param>
		/// <returns>The blob thumbnail name.</returns>
		public string GetThumbnailName(string defaultExtension = "png")
		{
			if (ContentType.Contains("image"))
			{
				return Name;
			}

			return Name.Replace(Extension, Sanitize(defaultExtension));
		}

		/// <summary>
		/// Get a download friendly name for this blob based on a specified label.
		/// </summary>
		/// <param name="blobLabel">The label to consider when generating the name.</param>
		/// <returns>The friendly download file name.</returns>
		public string BuildDownloadFileName(string blobLabel)
		{
			var label = Sanitize(blobLabel.ToSlug());
			var placeholder = $"BLOB-{Id.ToString().ToUpper()}";

			return $"{label.WhenNullOrWhiteSpace(placeholder)}.{Extension}";
		}

		/// <summary>
		/// Remove path delimiters from the input string ends.
		/// </summary>
		/// <param name="input">The string to cleanup.</param>
		/// <returns>Sanitized string.</returns>
		private static string Sanitize(string input)
		{
			return (input ?? string.Empty).Trim('~', '.', '/', '\\');
		}

		/// <summary>
		/// Extract the container from the virtual blob name.
		/// </summary>
		/// <param name="blobName">The virtual blob name.</param>
		/// <returns>Extracted blob folder.</returns>
		private static string GetContainer(string blobName)
		{
			return Path.GetDirectoryName(Sanitize(blobName)).Replace("\\", "/");
		}

		/// <summary>
		/// Extract the blob id from the virtual blob name.
		/// </summary>
		/// <param name="blobName">The virtual blob name.</param>
		/// <returns>Extracted blob id.</returns>
		private static string GetRawId(string blobName)
		{
			return Path.GetFileName(path: Path.GetFileNameWithoutExtension(Sanitize(blobName)));
		}

		/// <summary>
		/// Extract the extension from the virtual blob name.
		/// </summary>
		/// <param name="blobName">The virtual blob name.</param>
		/// <returns>Extracted blob extension.</returns>
		private static string GetCleanExtension(string blobName)
		{
			blobName = Sanitize(blobName);

			if (!Path.HasExtension(blobName))
			{
				throw new ArgumentException($"Extensionless blob: {blobName}", $"{nameof(blobName)} => {nameof(GetCleanExtension)} => {nameof(blobName)}");
			}

			return Sanitize(Path.GetExtension(blobName));
		}

		/// <summary>
		/// Build the folder for the new blob info.
		/// </summary>
		/// <returns>Folder structure for the blob.</returns>
		private static string BuildFolder()
		{
			var utcCreateDate = DateTime.UtcNow;

			var y = utcCreateDate.ToString("yyyy");
			var m = utcCreateDate.ToString("MM");
			var d = utcCreateDate.ToString("dd");

			return $"{y}/{m}/{d}";
		}
	}
}
