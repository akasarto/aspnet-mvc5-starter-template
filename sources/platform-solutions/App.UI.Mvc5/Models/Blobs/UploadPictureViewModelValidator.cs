using FluentValidation;
using App.UI.Mvc5.Infrastructure;
using System.Web;

namespace App.UI.Mvc5.Models
{
	public class UploadPictureViewModelValidator : AbstractValidator<UploadPictureViewModel>
	{
		/// <summary>
		/// Constructor method.
		/// </summary>
		public UploadPictureViewModelValidator()
		{
			// Image
			RuleFor(model => model.Picture).NotNull();
			RuleFor(model => model.Picture).Must(BeOfValidContentLength).WithMessage(model =>
				string.Format(GlobalizationManager.GetLocalizedString("_Validation_BlobSizeLimitError"), model?.Picture?.FileName ?? string.Empty)
			);
			RuleFor(model => model.Picture).Must(BeImageMimeType).WithMessage(model =>
				string.Format(GlobalizationManager.GetLocalizedString("_Validation_BlobImageMimeTypeError", model?.Picture?.FileName ?? string.Empty))
			);
		}

		private bool BeOfValidContentLength(HttpPostedFileBase postedFile)
		{
			if (postedFile == null)
			{
				return true;
			}

			if (postedFile.ContentLength <= 0)
			{
				return false;
			}

			return postedFile.ContentLength <= AppSettings.Blobs.FileUploadMaxLengthInBytes;
		}

		private bool BeImageMimeType(HttpPostedFileBase postedFile)
		{
			if (postedFile == null)
			{
				return true;
			}

			return postedFile.ContentType.Contains("image/");
		}
	}
}
