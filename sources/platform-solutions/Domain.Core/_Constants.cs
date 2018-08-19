namespace Domain.Core
{
	/// <summary>
	/// A set of constants available to the system.
	/// </summary>
	public static class _Constants
	{
		// User
		public const int UsersFullNameMaxLength = 256;
		public const int UsersEmailMaxLength = 256;
		public const int UsersUserNameMaxLength = 256;
		public const int UsersMobilePhoneMaxLength = 30;

		// Passwords
		public const int UsersPasswordMinLength = 6;
		public const bool UsersPasswordRequiresDigit = false;
		public const bool UsersPasswordRequiresLowercase = false;
		public const bool UsersPasswordRequiresUppercase = false;
		public const bool UsersPasswordRequiresNonLetterOrDigit = false;

		// Access Lockout
		public const bool UsersLockoutEnabledByDefault = true;
		public const int UsersLockoutIntervalInMinutes = 5;
		public const int UsersLockoutMaxFailedAccessAttempts = 3;

		// Events
		public const int EventNameMaxLength = 175;
		public const int EventBlobLabelMaxLength = 75;
	}
}
