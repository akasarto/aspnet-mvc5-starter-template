using App.UI.Mvc5.Infrastructure;
using Domain.Core;
using FluentValidation;

namespace App.UI.Mvc5.Areas.Users.Models
{
	public class AccountPasswordRecoverResponseViewModelValidator : AbstractValidator<AccountPasswordRecoverResponseViewModel>
	{
		public AccountPasswordRecoverResponseViewModelValidator()
		{
			// Email
			RuleFor(model => model.Email).NotEmpty();
			RuleFor(model => model.Email).Matches(_RegularExpressions.SimpleEmailPattern);
			RuleFor(model => model.Email).Length(0, _Constants.UsersEmailMaxLength).WithMessage(GlobalizationManager.GetLocalizedString<AreaResources>("_Validation_MaxLength"));

			// Password
			RuleFor(model => model.NewPassword).NotEmpty();
			RuleFor(model => model.NewPassword).Length(_Constants.UsersPasswordMinLength, int.MaxValue).WithMessage(GlobalizationManager.GetLocalizedString<AreaResources>("_Validation_MinLength"));

			// Confirm Password
			RuleFor(model => model.NewPassword2).NotEmpty();
			RuleFor(model => model.NewPassword2).Equal(model => model.NewPassword).WithMessage(GlobalizationManager.GetLocalizedString<AreaResources>("_Validation_PasswordsDoNotMatchError"));
		}
	}
}
