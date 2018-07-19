using System;
using System.IO;

namespace Shared.Infrastructure.FileSystem
{
	/// <summary>
	/// FileSystem storage service.
	/// </summary>
	public class FileSystemStorageService : IFileSystemStorageService
	{
		private DirectoryInfo _storageDirectory = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		/// <param name="storageFolder">The base storage folder where the blobs will be stored.</param>
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

		/// <summary>
		/// Delete a blob.
		/// </summary>
		/// <param name="blobName">Virtual blob name.</param>
		public virtual void Delete(string blobName)
		{
			blobName = blobName ?? string.Empty;
			var info = GetFileInfo(blobName);
			info.Delete();
		}

		/// <summary>
		/// Checks if the blob exists.
		/// </summary>
		/// <param name="blobName">Virtual blob name.</param>
		/// <returns><c>True</c> or <c>false</c>.</returns>
		public virtual bool Exists(string blobName)
		{
			blobName = blobName ?? string.Empty;
			var info = GetFileInfo(blobName);
			return info.Exists;
		}

		/// <summary>
		/// Get the endpoint to the blob within the storage service.
		/// </summary>
		/// <param name="blobName">Virtual blob name.</param>
		/// <returns>The <see cref="Uri"/> pointing to the bob file.</returns>
		public virtual Uri GetEndpoint(string blobName)
		{
			blobName = blobName ?? string.Empty;
			var fileInfo = GetFileInfo(blobName);
			var fileEndpoint = $"file://{fileInfo.FullName}";
			return new Uri(fileEndpoint, UriKind.Absolute);
		}

		/// <summary>
		/// Get an open stream for the blob contents.
		/// </summary>
		/// <param name="blobName">Virtual blob name.</param>
		/// <returns>The open <see cref="Stream"/> instance.</returns>
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

		/// <summary>
		/// Writes a stream to the blob.
		/// </summary>
		/// <param name="blobName">Virtual blob name.</param>
		/// <param name="blobStream">The <see cref="Stream"/> with data to be writen to the blob.</param>
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

		/// <summary>
		/// Gets the file system file info instance.
		/// </summary>
		/// <param name="blobName">The blob name to get a reference in the file system.</param>
		/// <returns>A <see cref="FileInfo"/> instance for the specified blob name.</returns>
		protected virtual FileInfo GetFileInfo(string blobName)
		{
			blobName = blobName ?? string.Empty;
			var baseFolder = _storageDirectory.FullName;
			var blobCleanName = blobName.Replace("/", "\\").Trim('\\');

			return new FileInfo(Path.Combine(baseFolder, blobCleanName));
		}
	}
}
