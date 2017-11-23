using FluentValidation;
using App.UI.Mvc5.Infrastructure;
using System.Web;

namespace App.UI.Mvc5.Models
{
	public class UploadAnyViewModelValidator : AbstractValidator<UploadAnyViewModel>
	{
		/// <summary>
		/// Constructor method.
		/// </summary>
		public UploadAnyViewModelValidator()
		{
			// Image
			RuleFor(model => model.Blob).NotNull();
			RuleFor(model => model.Blob).Must(BeOfValidContentLength).WithMessage(model =>
				string.Format(GlobalizationManager.GetLocalizedString("_Validation_BlobSizeLimitError"), model?.Blob?.FileName ?? string.Empty)
			);
			RuleFor(model => model.Blob).Must(BeKnownMimeType).WithMessage(model =>
				string.Format(GlobalizationManager.GetLocalizedString("_Validation_BlobKnownMimeTypeError", model?.Blob?.FileName ?? string.Empty))
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

		private bool BeKnownMimeType(HttpPostedFileBase postedFile)
		{
			if (postedFile == null)
			{
				return true;
			}

			return !string.IsNullOrWhiteSpace(postedFile.ContentType);
		}
	}
}
