using FluentValidation;

namespace App.UI.Mvc5.Areas.Features.Models
{
	public class FormsAndValidationViewModelValidator : AbstractValidator<FormsAndValidationViewModel>
	{
		public FormsAndValidationViewModelValidator()
		{
			RuleFor(model => model.FirstName).NotEmpty();
			RuleFor(model => model.LastName).NotEmpty();
			RuleFor(model => model.Username).NotEmpty();

			RuleFor(model => model.Email).Matches(_RegularExpressions.SimpleEmailPattern);

			RuleFor(model => model.AddressLine1).NotEmpty();

			RuleFor(model => model.CountryId).NotEmpty();
			RuleFor(model => model.StateId).NotEmpty();
			RuleFor(model => model.Zip).NotEmpty();

			RuleFor(model => model.CardName).NotEmpty();
			RuleFor(model => model.CardNumber).NotEmpty();
			RuleFor(model => model.CardExpiration).NotEmpty();
			RuleFor(model => model.CardCVV).NotEmpty();
		}
	}
}
