using Shared.Extensions;
using Shared.Infrastructure;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Domain.Core.Entities
{
	public class UserEntity
	{
		public int AccessFailedCount { get; set; }
		public List<Claim> Claims { get; set; } = new List<Claim>();
		public string CultureId { get; set; }
		public string Email { get; set; }
		public bool EmailConfirmed { get; set; }
		public string FullName { get; set; }
		public int Id { get; set; }
		public bool IsBlocked { get; set; }
		public bool LockoutEnabled { get; set; }
		public DateTimeOffset? LockoutEndDateUtc { get; set; }
		public string MobilePhone { get; set; }
		public bool MobilePhoneConfirmed { get; set; }
		public string PasswordHash { get; set; }
		public Guid? PictureBlobId { get; set; }
		public BlobInfo PictureInfo { get; set; }
		public List<Realm> Realms { get; set; } = new List<Realm>();
		public List<Role> Roles { get; set; } = new List<Role>();
		public string SecurityStamp { get; set; }
		public string TimeZoneId { get; set; }
		public bool TwoFactorEnabled { get; set; }
		public string UICultureId { get; set; }
		public string UserName { get; set; }
		public DateTime UTCCreation { get; set; }

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

		public virtual void SetDefaultUserName()
		{
			var input = Email;

			if (!string.IsNullOrWhiteSpace(UserName))
			{
				return;
			}

			UserName = input.FilterSpecialChars();
		}
	}
}
