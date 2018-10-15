using App.Identity.Repositories;
using App.Identity.UserStore;
using Domain.Core;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Threading.Tasks;

namespace App.Identity.Managers
{
	public class AppUserManager : UserManager<AppUserEntity, int>
	{
		private IIdentityRepository _identityRepository = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		public AppUserManager(IIdentityRepository identityRepository, IDataProtectionProvider dataProtectionProvider, AppUserStore store) : base(store)
		{
			_identityRepository = identityRepository;

			UserLockoutEnabledByDefault = _Constants.UsersLockoutEnabledByDefault;
			DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(_Constants.UsersLockoutIntervalInMinutes);
			MaxFailedAccessAttemptsBeforeLockout = _Constants.UsersLockoutMaxFailedAccessAttempts;

			UserValidator = new UserValidator<AppUserEntity, int>(this)
			{
				AllowOnlyAlphanumericUserNames = false,
				RequireUniqueEmail = true
			};

			PasswordValidator = new PasswordValidator
			{
				RequiredLength = _Constants.UsersPasswordMinLength,
				RequireDigit = _Constants.UsersPasswordRequiresDigit,
				RequireLowercase = _Constants.UsersPasswordRequiresLowercase,
				RequireUppercase = _Constants.UsersPasswordRequiresUppercase,
				RequireNonLetterOrDigit = _Constants.UsersPasswordRequiresNonLetterOrDigit
			};

			if (dataProtectionProvider != null)
			{
				UserTokenProvider = new DataProtectorTokenProvider<AppUserEntity, int>(dataProtectionProvider.Create("Skeleton [Admin Identity]"));
			}
		}

		public Task UpdateProfileAsync(AppUserEntity adminUser)
		{
			_identityRepository.UpdateProfile(adminUser);

			UpdateSecurityStampAsync(adminUser.Id);

			return Task.CompletedTask;
		}

		[Obsolete("This method is not used. Please use the current IEmailDispatcherService instance instead.", error: true)]
		public new Task SendEmailAsync(int userId, string subject, string body)
		{
			return base.SendEmailAsync(userId, subject, body);
		}

		[Obsolete("This method is currently not used.", error: true)]
		public new Task SendSmsAsync(int userId, string message)
		{
			return base.SendSmsAsync(userId, message);
		}
	}
}
