using App.Identity.Repositories;
using App.UI.Mvc5.Infrastructure;
using Domain.Core;
using Domain.Core.Sessions;
using FluentValidation;
using System;

namespace App.UI.Mvc5.Areas.Users.Models
{
	public class ProfileEditViewModelValidator : AbstractValidator<ProfileEditViewModel>
	{
		private IIdentityRepository _identityRepository = null;
		private ISessionContext _sessionContext = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		public ProfileEditViewModelValidator(ISessionContext sessionContext, IIdentityRepository identityRepository)
		{
			_sessionContext = sessionContext;
			_identityRepository = identityRepository;

			// Name
			RuleFor(model => model.FullName).NotEmpty();
			RuleFor(model => model.FullName).Length(0, _Constants.UsersFullNameMaxLength).WithMessage(GlobalizationManager.GetLocalizedString<AreaResources>("_Validation_MaxLength"));

			// Email
			RuleFor(model => model.Email).NotEmpty();
			RuleFor(model => model.Email).Matches(_RegularExpressions.SimpleEmailPattern);
			RuleFor(model => model.Email).Length(0, _Constants.UsersEmailMaxLength).WithMessage(GlobalizationManager.GetLocalizedString<AreaResources>("_Validation_MaxLength"));
			RuleFor(model => model.Email).Must(BeUniqueOrCurrentEmail).WithMessage(GlobalizationManager.GetLocalizedString<AreaResources>("_Validation_EmailTakenError"));

			// Username
			RuleFor(model => model.UserName).NotEmpty();
			RuleFor(model => model.UserName).Matches(_RegularExpressions.UserNamePattern);
			RuleFor(model => model.UserName).Length(0, _Constants.UsersUserNameMaxLength).WithMessage(GlobalizationManager.GetLocalizedString<AreaResources>("_Validation_MaxLength"));
			RuleFor(model => model.UserName).Must(BeUniqueOrCurrentUsername).WithMessage(GlobalizationManager.GetLocalizedString<AreaResources>("_Validation_UserNameTakenError"));

			// Picture Blob Id
			RuleFor(model => model.PictureBlobId).Must(BeExistingPictureBlobId).WithMessage(GlobalizationManager.GetLocalizedString<AreaResources>("_Validation_BlobIdInvalidError"));

			// Globalization
			RuleFor(model => model.CultureId).NotEmpty();
			RuleFor(model => model.UICultureId).NotEmpty();
			RuleFor(model => model.TimeZoneId).NotEmpty();
		}

		private bool BeExistingPictureBlobId(Guid? pictureBlobId)
		{
			if (!pictureBlobId.HasValue)
			{
				return true;
			}

			return pictureBlobId.Value != Guid.Empty;
		}

		private bool BeUniqueOrCurrentEmail(string email)
		{
			if (string.IsNullOrWhiteSpace(email) || _sessionContext == null)
			{
				return false;
			}

			var user = _identityRepository.GetByEmail(email);

			if (user == null)
			{
				return true;
			}

			return user.Id.Equals(_sessionContext.UserId);
		}

		private bool BeUniqueOrCurrentUsername(string userName)
		{
			if (string.IsNullOrWhiteSpace(userName) || _sessionContext == null)
			{
				return false;
			}

			var user = _identityRepository.GetByUserName(userName);

			if (user == null)
			{
				return true;
			}

			return user.Id.Equals(_sessionContext.UserId);
		}
	}
}
