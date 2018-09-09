using App.Identity.Repositories;
using App.UI.Mvc5.Infrastructure;
using Domain.Core;
using FluentValidation;

namespace App.UI.Mvc5.Areas.Users.Models
{
	public class AccountSignUpViewModelValidator : AbstractValidator<AccountSignUpViewModel>
	{
		private IIdentityRepository _identityRepository = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		public AccountSignUpViewModelValidator(IIdentityRepository identityRepository)
		{
			_identityRepository = identityRepository;

			// Name
			RuleFor(model => model.Name).NotEmpty();
			RuleFor(model => model.Name).Length(0, _Constants.UsersFullNameMaxLength).WithMessage(GlobalizationManager.GetLocalizedString("_Validation_MaxLength"));

			// Email
			RuleFor(model => model.Email).NotEmpty();
			RuleFor(model => model.Email).Matches(_RegularExpressions.SimpleEmailPattern);
			RuleFor(model => model.Email).Length(0, _Constants.UsersEmailMaxLength).WithMessage(GlobalizationManager.GetLocalizedString("_Validation_MaxLength"));
			RuleFor(model => model.Email).Must(BeUniqueEmail).WithMessage(GlobalizationManager.GetLocalizedString("_Validation_EmailTakenError"));

			// Password
			RuleFor(model => model.Password).NotEmpty();
			RuleFor(model => model.Password).Length(_Constants.UsersPasswordMinLength, int.MaxValue).WithMessage(GlobalizationManager.GetLocalizedString("_Validation_MinLength"));

			// Confirm Password
			RuleFor(model => model.Password2).NotEmpty();
			RuleFor(model => model.Password2).Equal(model => model.Password).WithMessage(GlobalizationManager.GetLocalizedString("_Validation_PasswordsDoNotMatchError"));

			// Terms Of Use Acceptance
			RuleFor(model => model.TermsAcceptance).SetValidator(new RequiredCheckboxValidator()).WithMessage(GlobalizationManager.GetLocalizedString("_Validation_TermsAcceptanceError"));
		}

		private bool BeUniqueEmail(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
			{
				return true;
			}

			var user = _identityRepository.GetByEmail(email);

			return user == null;
		}
	}
}