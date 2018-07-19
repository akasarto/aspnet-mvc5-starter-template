using App.UI.Mvc5.Infrastructure;
using FluentValidation;
using Domain.Core;

namespace App.UI.Mvc5.Areas.Users.Models
{
	public class AccountPasswordChangeViewModelValidator : AbstractValidator<AccountPasswordChangeViewModel>
	{
		/// <summary>
		/// Constructor method.
		/// </summary>
		public AccountPasswordChangeViewModelValidator()
		{
			// Password
			RuleFor(model => model.Password).NotEmpty();
			RuleFor(model => model.Password).Length(_Constants.UsersPasswordMinLength, int.MaxValue).WithMessage(GlobalizationManager.GetLocalizedString("_Validation_MinLength"));

			// New Password
			RuleFor(model => model.NewPassword).NotEmpty();
			RuleFor(model => model.NewPassword).Length(_Constants.UsersPasswordMinLength, int.MaxValue).WithMessage(GlobalizationManager.GetLocalizedString("_Validation_MinLength"));

			// New Password Confirmation
			RuleFor(model => model.NewPassword2).NotEmpty();
			RuleFor(model => model.NewPassword2).Equal(model => model.NewPassword).WithMessage(GlobalizationManager.GetLocalizedString("_Validation_PasswordsDoNotMatchError"));
		}
	}
}
