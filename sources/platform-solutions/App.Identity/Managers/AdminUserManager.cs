using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using App.Core;
using System;
using System.Threading.Tasks;

namespace App.Identity
{
	/// <summary>
	/// Asp.Net identity user manager for the admin website.
	/// </summary>
	public class AdminUserManager : UserManager<AdminUserEntity, int>
	{
		private IIdentityRepository _identityRepository = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		/// <param name="identityRepository">The identity specialized repository.</param>
		/// <param name="dataProtectionProvider">The current data protection provider.</param>
		/// <param name="userStore">The current user store.</param>
		public AdminUserManager(IIdentityRepository identityRepository, IDataProtectionProvider dataProtectionProvider, AdminStore userStore) : base(userStore)
		{
			_identityRepository = identityRepository;

			UserLockoutEnabledByDefault = _Constants.UsersLockoutEnabledByDefault;
			DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(_Constants.UsersLocloutIntervalInMinutes);
			MaxFailedAccessAttemptsBeforeLockout = _Constants.UsersLockoutMaxFailedAccessAttempts;

			UserValidator = new UserValidator<AdminUserEntity, int>(this)
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
				UserTokenProvider = new DataProtectorTokenProvider<AdminUserEntity, int>(dataProtectionProvider.Create("Skeleton [Admin Identity]"));
			}
		}

		/// <summary>
		/// Update the user profile data keeping identity information intact.
		/// </summary>
		/// <param name="adminUser">The current admin user instance.</param>
		/// <returns>A <see cref="Task"/>.</returns>
		public Task UpdateProfileAsync(AdminUserEntity adminUser)
		{
			_identityRepository.UpdateProfile(adminUser);

			UpdateSecurityStampAsync(adminUser.Id);

			return Task.CompletedTask;
		}

		[Obsolete("This method is not supported. Please use the current IEmailDispatcherService instance instead.", error: true)]
		public new Task SendEmailAsync(int userId, string subject, string body)
		{
			return base.SendEmailAsync(userId, subject, body);
		}

		[Obsolete("This method is currently not supported.", error: true)]
		public new Task SendSmsAsync(int userId, string message)
		{
			return base.SendSmsAsync(userId, message);
		}
	}
}
