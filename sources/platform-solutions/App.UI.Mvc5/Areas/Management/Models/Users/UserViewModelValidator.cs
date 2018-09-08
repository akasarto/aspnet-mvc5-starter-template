using App.UI.Mvc5.Infrastructure;
using FluentValidation;
using Domain.Core;
using App.Identity.Repositories;

namespace App.UI.Mvc5.Areas.Management.Models
{
	public class UserViewModelValidator : AbstractValidator<UserViewModel>
	{
		private IIdentityRepository _identityRepository = null;

		public UserViewModelValidator(IIdentityRepository identityRepository)
		{
			_identityRepository = identityRepository;

			// Validate name and email only when adding new user.
			When(model => model.Id <= 0, () =>
			{
				// Name
				RuleFor(model => model.FullName).NotEmpty();
				RuleFor(model => model.FullName).Length(0, _Constants.UsersFullNameMaxLength).WithMessage(GlobalizationManager.GetLocalizedString("_Validation_MaxLength"));

				// Email
				RuleFor(model => model.Email).NotEmpty();
				RuleFor(model => model.Email).Matches(_RegularExpressions.SimpleEmailPattern);
				RuleFor(model => model.Email).Length(0, _Constants.UsersEmailMaxLength).WithMessage(GlobalizationManager.GetLocalizedString("_Validation_MaxLength"));
				RuleFor(model => model.Email).Must(BeUniqueEmail).WithMessage(GlobalizationManager.GetLocalizedString("_Validation_EmailTakenError"));
			});

			// Realms
			RuleFor(model => model.Realms).NotEmpty();

			// Roles
			RuleFor(model => model.Roles).NotEmpty();
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
