using ImageResizer.Configuration;
using ImageResizer.Plugins;
using Sarto.Infrastructure;
using System;
using System.Collections.Specialized;

namespace App.UI.Mvc5.Infrastructure
{
	public class ImageResizerThumbPlugin : IPlugin, IVirtualImageProvider
	{
		private IBlobStorageService _blobService = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		public ImageResizerThumbPlugin(IBlobStorageService blobService)
		{
			_blobService = blobService ?? throw new ArgumentNullException(nameof(blobService), nameof(ImageResizerThumbPlugin));
		}

		public bool FileExists(string virtualPath, NameValueCollection queryString)
		{
			virtualPath = virtualPath ?? string.Empty;

			return virtualPath.Contains(Sanitize(ImageResizerInfra.VirtualFileSystemPrefix)) && _blobService.Exists(RemovePrefix(virtualPath));
		}

		public IVirtualFile GetFile(string virtualPath, NameValueCollection queryString)
		{
			var blobName = RemovePrefix(virtualPath);
			var blobStream = _blobService.ReadStream(blobName);

			return new ImageResizerThumbVirtualFile(virtualPath, blobStream);
		}

		public IPlugin Install(Config config)
		{
			config.Plugins.add_plugin(this);
			return this;
		}

		public bool Uninstall(Config config)
		{
			config.Plugins.remove_plugin(this);
			return true;
		}

		private string Sanitize(string path) => path?.Trim('\\', '/', '~') ?? string.Empty;

		private string RemovePrefix(string virtualPath) => Sanitize(
			virtualPath?.Replace(
				Sanitize(ImageResizerInfra.VirtualFileSystemPrefix ?? string.Empty), 
				string.Empty
			)
		);
	}
}
