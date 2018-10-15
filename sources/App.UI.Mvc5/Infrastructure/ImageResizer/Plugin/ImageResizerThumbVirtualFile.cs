using ImageResizer.Plugins;
using System;
using System.IO;

namespace App.UI.Mvc5.Infrastructure
{
	public class ImageResizerThumbVirtualFile : IVirtualFile
	{
		private readonly Stream _stream = null;
		private readonly string _virtualPath = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		public ImageResizerThumbVirtualFile(string virtualPath, Stream stream)
		{
			_virtualPath = virtualPath ?? throw new ArgumentNullException(nameof(virtualPath), nameof(ImageResizerThumbVirtualFile));
			_stream = stream ?? throw new ArgumentNullException(nameof(stream), nameof(ImageResizerThumbVirtualFile));
		}

		public string VirtualPath => _virtualPath;

		public Stream Open() => _stream;
	}
}
