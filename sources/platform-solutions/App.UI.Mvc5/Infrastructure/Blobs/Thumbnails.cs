using System;
using System.Web.Mvc;

namespace App.UI.Mvc5.Infrastructure
{
	public static partial class BlobExtensions
	{
		public static Uri BlobThumbnail(this UrlHelper @this, string blobName, string label, int? width = null, int? height = null)
		{
			var blobService = DependencyResolver.Current.GetService<IBlobService>();
			return blobService.GetThumbnailEndpoint(blobName, label, width, height);
		}
	}
}
