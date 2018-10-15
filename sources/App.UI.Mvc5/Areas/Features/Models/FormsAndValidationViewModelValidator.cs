using System;
using FluentValidation;
using Shared.Extensions;

namespace App.UI.Mvc5.Areas.Features.Models
{
	public class FormsAndValidationViewModelValidator : AbstractValidator<FormsAndValidationViewModel>
	{
		// Dependency injection is supported in validation constructors.

		public FormsAndValidationViewModelValidator()
		{
			RuleFor(model => model.FirstName).NotEmpty();
			RuleFor(model => model.LastName).NotEmpty();

			RuleFor(model => model.Username).NotEmpty().Must(BeDifferentFromInvalid).WithMessage("Username '{PropertyValue}' sample server error.");

			RuleFor(model => model.Email).Matches(_RegularExpressions.SimpleEmailPattern);

			RuleFor(model => model.AddressLine1).NotEmpty();

			RuleFor(model => model.CountryId).NotEmpty();
			RuleFor(model => model.StateId).NotEmpty();
			RuleFor(model => model.Zip).NotEmpty();

			RuleFor(model => model.CardName).NotEmpty();
			RuleFor(model => model.CardNumber).NotEmpty();
			RuleFor(model => model.CardExpiration).NotEmpty();
			RuleFor(model => model.CardCVV).NotEmpty();

			RuleFor(model => model.PromoCode).Must(BeValidPromoCode).WithMessage("Invalid promo code.");
		}

		private bool BeValidPromoCode(string input)
		{
			// Some validation can be applied here prior to actually validate the promo code.

			return true;
		}

		private bool BeDifferentFromInvalid(string input)
		{
			return
				!input
				.EnsureNotNull()
				.ToLower()
				.Equals("invalid");
		}
	}
}
