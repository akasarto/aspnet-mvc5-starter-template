using System;
using System.IO;

namespace Shared.Infrastructure.FileSystem
{
	public class FileSystemStorageService : IFileSystemStorageService
	{
		private DirectoryInfo _storageDirectory = null;

		public FileSystemStorageService(string storageFolder)
		{
			storageFolder = storageFolder ?? throw new ArgumentNullException(nameof(storageFolder), nameof(FileSystemStorageService));

			storageFolder = storageFolder.Trim('\\', '/');

			if (!Path.IsPathRooted(storageFolder))
			{
				storageFolder = Path.Combine(
					AppDomain.CurrentDomain.BaseDirectory,
					storageFolder.Trim('~').Trim('\\', '/').Replace("/", "\\")
				);
			}

			_storageDirectory = Directory.CreateDirectory(storageFolder);

			if (!_storageDirectory.Exists)
			{
				_storageDirectory.Create();
			}
		}

		public virtual void Delete(string blobName)
		{
			blobName = blobName ?? string.Empty;
			var info = GetFileInfo(blobName);
			info.Delete();
		}

		public virtual bool Exists(string blobName)
		{
			blobName = blobName ?? string.Empty;
			var info = GetFileInfo(blobName);
			return info.Exists;
		}

		public virtual Uri GetEndpoint(string blobName)
		{
			blobName = blobName ?? string.Empty;
			var fileInfo = GetFileInfo(blobName);
			var fileEndpoint = $"file://{fileInfo.FullName}";
			return new Uri(fileEndpoint, UriKind.Absolute);
		}

		public virtual Stream ReadStream(string blobName)
		{
			blobName = blobName ?? string.Empty;

			var fileInfo = GetFileInfo(blobName);

			if (!fileInfo.Exists)
			{
				return Stream.Null;
			}

			return fileInfo.OpenRead();
		}

		public virtual void WriteStream(string blobName, Stream blobStream)
		{
			blobName = blobName ?? throw new ArgumentNullException(nameof(blobName), nameof(FileSystemStorageService));
			blobStream = blobStream ?? throw new ArgumentNullException(nameof(blobStream), nameof(FileSystemStorageService));

			var fileInfo = GetFileInfo(blobName);

			if (!fileInfo.Directory.Exists)
			{
				fileInfo.Directory.Create();
			}

			using (blobStream)
			{
				using (var fStream = fileInfo.OpenWrite())
				{
					blobStream.CopyTo(fStream);
				}
			}
		}

		protected virtual FileInfo GetFileInfo(string blobName)
		{
			blobName = blobName ?? string.Empty;
			var baseFolder = _storageDirectory.FullName;
			var blobCleanName = blobName.Replace("/", "\\").Trim('\\');

			return new FileInfo(Path.Combine(baseFolder, blobCleanName));
		}
	}
}
