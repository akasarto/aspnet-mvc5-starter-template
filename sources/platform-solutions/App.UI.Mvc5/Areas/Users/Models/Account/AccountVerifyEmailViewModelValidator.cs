using FluentValidation;

namespace App.UI.Mvc5.Areas.Users.Models
{
	public class AccountVerifyEmailViewModelValidator : AbstractValidator<AccountVerifyEmailViewModel>
	{
		/// <summary>
		/// Constructor method.
		/// </summary>
		public AccountVerifyEmailViewModelValidator()
		{
		}
	}
}