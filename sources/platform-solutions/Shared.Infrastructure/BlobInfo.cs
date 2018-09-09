using Shared.Extensions;
using System;
using System.IO;

namespace Shared.Infrastructure
{
	public sealed class BlobInfo
	{
		private readonly string _folder;
		private string _cleanExtension;
		private Guid _id;

		public BlobInfo(string sourceFileName) : this(Guid.NewGuid(), BuildFolder(), GetCleanExtension(sourceFileName))
		{
		}

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

		public string ContentType => MimeTypeMap.GetMimeType(extension: GetCleanExtension(Name));
		public string Extension => _cleanExtension;
		public string Folder => _folder;
		public Guid Id => _id;
		public string Name => string.Concat("/", Sanitize($@"{Folder}/{Id}.{Extension}"));

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

		public string BuildDownloadFileName(string blobLabel)
		{
			var label = Sanitize(blobLabel.ToSlug());
			var placeholder = $"BLOB-{Id.ToString().ToUpper()}";

			return $"{label.WhenNullOrWhiteSpace(placeholder)}.{Extension}";
		}

		public string GetThumbnailName(string defaultExtension = "png")
		{
			if (ContentType.Contains("image"))
			{
				return Name;
			}

			return Name.Replace(Extension, Sanitize(defaultExtension));
		}

		public override string ToString() => Name;

		private static string BuildFolder()
		{
			var utcCreateDate = DateTime.UtcNow;

			var y = utcCreateDate.ToString("yyyy");
			var m = utcCreateDate.ToString("MM");
			var d = utcCreateDate.ToString("dd");

			return $"{y}/{m}/{d}";
		}

		private static string GetCleanExtension(string blobName)
		{
			blobName = Sanitize(blobName);

			if (!Path.HasExtension(blobName))
			{
				throw new ArgumentException($"Extensionless blob: {blobName}", $"{nameof(blobName)} => {nameof(GetCleanExtension)} => {nameof(blobName)}");
			}

			return Sanitize(Path.GetExtension(blobName));
		}

		private static string GetContainer(string blobName)
		{
			return Path.GetDirectoryName(Sanitize(blobName)).Replace("\\", "/");
		}

		private static string GetRawId(string blobName)
		{
			return Path.GetFileName(path: Path.GetFileNameWithoutExtension(Sanitize(blobName)));
		}

		private static string Sanitize(string input)
		{
			return (input ?? string.Empty).Trim('~', '.', '/', '\\');
		}
	}
}
