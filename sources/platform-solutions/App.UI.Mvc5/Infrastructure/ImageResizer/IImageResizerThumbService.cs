using ImageResizer;
using Sarto.Infrastructure;
using System;

namespace App.UI.Mvc5.Infrastructure
{
	public interface IImageResizerThumbService : IBlobThumbService
	{
		Uri GetThumbEndpoint(string blobName, string label, Instructions instructions = null);
	}
}