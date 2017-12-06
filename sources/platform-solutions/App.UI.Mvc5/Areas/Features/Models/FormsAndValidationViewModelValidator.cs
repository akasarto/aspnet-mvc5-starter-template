using FluentValidation;
using System;
using System.Web;

namespace App.UI.Mvc5.Areas.Features.Models
{
	/// <summary>
	/// This will be automatically applied to the FormsAndValidationViewModel when posted in a form.
	/// </summary>
	public class FormsAndValidationViewModelValidator : AbstractValidator<FormsAndValidationViewModel>
	{
		/// <summary>
		/// Constructor method.
		/// </summary>
		public FormsAndValidationViewModelValidator()
		{
			// Ensures the the field will not have an empty value.
			RuleFor(model => model.RequiredText).NotEmpty();

			// Ensures that the date will be a valid date value.
			RuleFor(model => model.Date).Must(BeValidDate).WithMessage("Invalid value for {PropertyName}");

			// Validate the upload content type depending on the selected value
			When(model => model.UploadValidate == "image", () =>
			{
				RuleFor(model => model.DefaultUpload).Must(BeImageType).WithMessage(model => $"The file '{model.DefaultUpload.FileName}' is not an image type.");
			});

			//
			RuleFor(model => model.SelectedMulti).NotEmpty();
		}

		private bool BeImageType(HttpPostedFileBase postedfile)
		{
			if (postedfile == null)
			{
				return true;
			}

			return postedfile.ContentType.Contains("image");
		}

		private bool BeValidDate(DateTime? date)
		{
			if (!date.HasValue)
			{
				return true;
			}

			return !date.Value.Equals(default(DateTime));
		}
	}
}
