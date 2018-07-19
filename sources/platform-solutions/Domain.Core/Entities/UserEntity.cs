using Shared.Extensions;
using Shared.Infrastructure;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Domain.Core.Entities
{
	/// <summary>
	/// Represents an user entity.
	/// </summary>
	public class UserEntity
	{
		/// <summary>
		/// Gets or sets the id.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the blocked state.
		/// </summary>
		public bool IsBlocked { get; set; }

		/// <summary>
		/// Gets or sets the UTC creation date.
		/// </summary>
		public DateTime UTCCreation { get; set; }

		/// <summary>
		/// Gets or sets the mobile phone number.
		/// </summary>
		public string MobilePhone { get; set; }

		/// <summary>
		/// Gets or sets the too many login failed attempts lockout state.
		/// </summary>
		public bool LockoutEnabled { get; set; }

		/// <summary>
		/// Gets or sets the two factor authentication state.
		/// </summary>
		public bool TwoFactorEnabled { get; set; }

		/// <summary>
		/// Gets or sets the mobile phone confirmed state.
		/// </summary>
		public bool MobilePhoneConfirmed { get; set; }

		/// <summary>
		/// Gets or sets the date that the lockout state will be suspended.
		/// <see cref="LockoutEnabled"/>
		/// </summary>
		public DateTimeOffset? LockoutEndDateUtc { get; set; }

		/// <summary>
		/// Gets or sets the login failed attempts count.
		/// </summary>
		public int AccessFailedCount { get; set; }

		/// <summary>
		/// Gets or sets the email confirmed state.
		/// </summary>
		public bool EmailConfirmed { get; set; }

		/// <summary>
		/// Gets or sets the hash for the provided password.
		/// </summary>
		public string PasswordHash { get; set; }

		/// <summary>
		/// Gets or sets the stamp that changes every time some identity information has changed.
		/// </summary>
		public string SecurityStamp { get; set; }

		/// <summary>
		/// Gets or sets the username.
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// Gets or sets the full name.
		/// </summary>
		public string FullName { get; set; }

		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the culture id (for date, number and currency formats).
		/// </summary>
		public string CultureId { get; set; }

		/// <summary>
		/// Gets or sets the UI culture id (for language translations).
		/// </summary>
		public string UICultureId { get; set; }

		/// <summary>
		/// Gets or sets the time zone id (for proper converting dates and time).
		/// </summary>
		public string TimeZoneId { get; set; }

		/// <summary>
		/// Gets or sets the picture blob id.
		/// </summary>
		public Guid? PictureBlobId { get; set; }

		/// <summary>
		/// Gets or sets the user picture blob info.
		/// </summary>
		public BlobInfo PictureInfo { get; set; }

		/// <summary>
		/// Gets or sets the user claims collection.
		/// </summary>
		public List<Claim> Claims { get; set; } = new List<Claim>();

		/// <summary>
		/// Gets or sets the realms that the user has access to.
		/// </summary>
		public List<Realm> Realms { get; set; } = new List<Realm>();

		/// <summary>
		/// Gets or sets the roles associated with the user (this are converted to claims of type 'Role' during login).
		/// </summary>
		public List<Role> Roles { get; set; } = new List<Role>();

		/// <summary>
		/// Builds the initial username based on the email when creating a new user.
		/// </summary>
		public virtual void SetDefaultUserName()
		{
			var input = Email;

			if (!string.IsNullOrWhiteSpace(UserName))
			{
				return;
			}

			UserName = input.FilterSpecialChars();
		}

		/// <summary>
		/// Builds the default password when creating a new user.
		/// </summary>
		public virtual string GetInitialPassword()
		{
			var guid = Guid.NewGuid().ToString().FilterSpecialChars();

			var initialPassword = guid.Substring(0, _Constants.UsersPasswordMinLength);

			initialPassword = $"{initialPassword}{(_Constants.UsersPasswordRequiresDigit ? "9" : "")}";
			initialPassword = $"{initialPassword}{(_Constants.UsersPasswordRequiresLowercase ? "x" : "")}";
			initialPassword = $"{initialPassword}{(_Constants.UsersPasswordRequiresNonLetterOrDigit ? "_" : "")}";
			initialPassword = $"{initialPassword}{(_Constants.UsersPasswordRequiresUppercase ? "Z" : "")}";

			return initialPassword;
		}
	}
}
