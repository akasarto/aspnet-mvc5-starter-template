using App.UI.Mvc5.Infrastructure;
using App.UI.Mvc5.Models;
using Shared.Infrastructure;
using Shared.Extensions;
using System;
using System.Net;
using System.Net.Mime;
using System.Web.Mvc;

namespace App.UI.Mvc5.Controllers
{
	[RoutePrefix("blobs")]
	public partial class BlobsController : __BaseController
	{
		private IBlobService _blobService = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		public BlobsController(IBlobService blobService)
		{
			_blobService = blobService ?? throw new ArgumentNullException(nameof(blobService), nameof(BlobsController));
		}

		[Route("download/{*blobName}", Name = "BlobsDownloadGet")]
		public ActionResult Download(string blobName, string donloadFileLabel)
		{
			var blobInfo = BlobInfo.FromName(blobName);

			var blobStream = _blobService.Download(blobInfo);

			if (blobStream.IsNull())
			{
				return ErrorResult(HttpStatusCode.NotFound);
			}

			var contentDisposition = new ContentDisposition
			{
				FileName = blobInfo.BuildDownloadFileName(donloadFileLabel),
				Inline = false
			};

			Response.AppendHeader("Content-Disposition", contentDisposition.ToString());

			return File(blobStream, blobInfo.ContentType);
		}

		[HttpPost]
		[Route("upload/any", Name = "BlobsUploadAnyPost")]
		public ActionResult UploadAny(UploadAnyViewModel model)
		{
			if (ModelState.IsValid)
			{
				var sourceName = model.Blob.FileName;
				var sourceStream = model.Blob.InputStream;

				var blobInfo = _blobService.Upload(
					sourceName, 
					sourceStream, 
					User.Id
				).GetInfo();

				return Json(new
				{
					blob = blobInfo,
					thumbnail = _blobService.GetThumbnailEndpoint(blobInfo.Name, "preview", model.PreviewThumbWidth, model.PreviewThumbHeight)
				});
			}

			return JsonError(ModelState);
		}

		[HttpPost]
		[Route("upload/picture", Name = "BlobsUploadPicturePost")]
		public ActionResult UploadPicture(UploadPictureViewModel model)
		{
			if (ModelState.IsValid)
			{
				var sourceName = model.Picture.FileName;
				var sourceStream = model.Picture.InputStream;

				var blobInfo = _blobService.Upload(
					sourceName, 
					sourceStream, 
					User.Id
				).GetInfo();

				return Json(new
				{
					blob = blobInfo,
					thumbnail = _blobService.GetThumbnailEndpoint(blobInfo.Name, "preview", model.PreviewThumbWidth, model.PreviewThumbHeight)
				});
			}

			return JsonError(ModelState);
		}
	}
}
