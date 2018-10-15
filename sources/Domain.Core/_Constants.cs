namespace Domain.Core
{
	public static class _Constants
	{
		// User
		public const int UsersEmailMaxLength = 256;
		public const int UsersFullNameMaxLength = 256;

		// Access Lockout
		public const bool UsersLockoutEnabledByDefault = true;
		public const int UsersLockoutIntervalInMinutes = 5;
		public const int UsersLockoutMaxFailedAccessAttempts = 3;
		public const int UsersMobilePhoneMaxLength = 30;

		// Passwords
		public const int UsersPasswordMinLength = 6;
		public const bool UsersPasswordRequiresDigit = false;
		public const bool UsersPasswordRequiresLowercase = false;
		public const bool UsersPasswordRequiresNonLetterOrDigit = false;
		public const bool UsersPasswordRequiresUppercase = false;
		public const int UsersUserNameMaxLength = 256;
	}
}
