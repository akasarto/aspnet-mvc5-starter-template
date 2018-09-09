using FluentValidation;

namespace App.UI.Mvc5.Areas.Users.Models
{
	public class AccountLogInViewModelValidator : AbstractValidator<AccountLogInViewModel>
	{
		public AccountLogInViewModelValidator()
		{
			// Email or Username
			RuleFor(model => model.EmailOrUsername).NotEmpty();

			// Password
			RuleFor(model => model.Password).NotEmpty();
		}
	}
}