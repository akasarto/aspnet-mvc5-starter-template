using FluentValidation;
using System;
using System.Web;

namespace App.UI.Mvc5.Areas.Features.Models
{
	public class GlobalizationViewModelValidator : AbstractValidator<GlobalizationViewModel>
	{
		public GlobalizationViewModelValidator()
		{
			//
			RuleFor(model => model.Date).Must(BeValidDate).WithMessage("Invalid value for {PropertyName}");

			//
			When(model => model.UploadValidate == "image", () =>
			{
				RuleFor(model => model.StandadUpload).Must(BeImageType).WithMessage(model => $"The file '{model.StandadUpload.FileName}' is not an image type.");
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
