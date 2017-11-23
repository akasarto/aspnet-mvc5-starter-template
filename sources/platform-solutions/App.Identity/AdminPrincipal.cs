using App.Core.Entities;
using Sarto.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace App.Identity
{
	/// <summary>
	/// Represents the current user identity/principal using the system.
	/// </summary>
	public class AdminPrincipal : UserPrincipal
	{
		private const string IdClaim = ClaimTypes.NameIdentifier;
		private const string FullNameClaim = "u.full.name";
		private const string UserNameClaim = ClaimTypes.Name;
		private const string EmailClaim = ClaimTypes.Email;
		private const string EmailConfirmedClaim = "u.eml.confirmed";
		private const string PictureBlobIdClaim = "u.pic.blob.id";
		private const string PictureBlobNameClaim = "u.pic.blob.name";
		private const string CultureIdClaim = "u.culture.id";
		private const string UICultureIdClaim = "u.uiculture.id";
		private const string LocalUserTimeZoneIdClaim = "u.timezone.id";
		private const string ScreenAutoLockMinutesClaim = "adm.scr.lck.mins";
		private const string ScreenLockedClaim = "adm.scr.lck";
		private const string IsPersistentClaim = "adm.is.persistent";

		/// <summary>
		/// Internal constructor method.
		/// </summary>
		/// <remarks>
		/// This constructor is used by mapper tools to construct new instances.
		/// </remarks>
		public AdminPrincipal() : base(new ClaimsPrincipal()) { }

		/// <summary>
		/// Constructor method.
		/// </summary>
		/// <param name="principal"></param>
		public AdminPrincipal(IPrincipal principal) : base(principal)
		{
		}

		/// <summary>
		/// Gets or sets the id from claims collection.
		/// </summary>
		public int Id
		{
			get => GetClaimValue<int>(IdClaim);
			set => SetClaim(IdClaim, value);
		}

		/// <summary>
		/// Get the full name from claims collection.
		/// </summary>
		public string FullName
		{
			get => GetClaimValue<string>(FullNameClaim);
			set => SetClaim(FullNameClaim, value);
		}

		/// <summary>
		/// Get the username from claims collection.
		/// </summary>
		public string UserName
		{
			get => GetClaimValue<string>(UserNameClaim);
			set => SetClaim(UserNameClaim, value);
		}

		/// <summary>
		/// Get the email from claims collection.
		/// </summary>
		public string Email
		{
			get => GetClaimValue<string>(EmailClaim);
			set => SetClaim(EmailClaim, value);
		}

		/// <summary>
		/// Get the email confirmed status from claims collection.
		/// </summary>
		public bool EmailConfirmed
		{
			get => GetClaimValue<bool>(EmailConfirmedClaim);
			set => SetClaim(EmailConfirmedClaim, value);
		}

		/// <summary>
		/// Get the profile picture blob id from claims collection.
		/// </summary>
		public Guid? PictureBlobId
		{
			get => GetClaimValue<Guid?>(PictureBlobIdClaim);
			set => SetClaim(PictureBlobIdClaim, value);
		}

		/// <summary>
		/// Get the profile picture blob name from claims collection.
		/// </summary>
		public string PictureBlobName
		{
			get => GetClaimValue<string>(PictureBlobNameClaim);
			set => SetClaim(PictureBlobNameClaim, value);
		}

		/// <summary>
		/// Get the culture id from claims collection.
		/// </summary>
		public string CultureId
		{
			get => GetClaimValue<string>(CultureIdClaim);
			set => SetClaim(CultureIdClaim, value);
		}

		/// <summary>
		/// Get the UI culture id from claims collection.
		/// </summary>
		public string UICultureId
		{
			get => GetClaimValue<string>(UICultureIdClaim);
			set => SetClaim(UICultureIdClaim, value);
		}

		/// <summary>
		/// Get the timezone id from claims collection.
		/// </summary>
		public string TimeZoneId
		{
			get => GetClaimValue<string>(LocalUserTimeZoneIdClaim, "UTC");
			set => SetClaim(LocalUserTimeZoneIdClaim, value);
		}

		/// <summary>
		/// Get the profile automatic screen lock interval from claims collection.
		/// </summary>
		public int ScreenAutoLockMinutes
		{
			get => GetClaimValue<int>(ScreenAutoLockMinutesClaim);
			set => SetClaim(ScreenAutoLockMinutesClaim, value);
		}

		/// <summary>
		/// Get the current screen locked status from the claims collection.
		/// </summary>
		public bool ScreenLocked
		{
			get => GetClaimValue<bool>(ScreenLockedClaim);
			set => SetClaim(ScreenLockedClaim, value);
		}

		/// <summary>
		/// Get the initial persistence state defined by the user during login (Remember Me).
		/// </summary>
		/// <remarks>This is primarily used for tracking the latest state when the claims have to be updated. (E.g.: Edit profile or screen locks).</remarks>
		public bool IsPersistent
		{
			get => GetClaimValue<bool>(IsPersistentClaim);
			set => SetClaim(IsPersistentClaim, value);
		}

		/// <summary>
		/// Current culture info for the user.
		/// </summary>
		public CultureInfo Culture { get; set; }

		/// <summary>
		/// Current ui culture info for the user.
		/// </summary>
		public CultureInfo UICulture { get; set; }

		/// <summary>
		/// Current time zone info for the user.
		/// </summary>
		public TimeZoneInfo TimeZone { get; set; }

		/// <summary>
		/// Gets a list of claims that are relevant to the admin application.
		/// </summary>
		/// <param name="adminUserEntity">The admin user instance.</param>
		/// <param name="screenLocked">The current screen locked state.</param>
		/// <returns>A list of claims relevant to the admin application.</returns>
		public static List<Claim> GetAdminClaims(AdminUserEntity adminUserEntity, bool isPersistent, bool screenLocked)
		{
			//
			var claims = new List<Claim>()
			{
				new Claim(IdClaim, adminUserEntity.Id.ToString()),
				new Claim(FullNameClaim, adminUserEntity.FullName),
				new Claim(UserNameClaim, adminUserEntity.UserName),
				new Claim(EmailClaim, adminUserEntity.Email),
				new Claim(EmailConfirmedClaim, adminUserEntity.EmailConfirmed.ChangeType<string>()),
				new Claim(PictureBlobIdClaim, adminUserEntity.PictureInfo?.Id.ToString() ?? string.Empty),
				new Claim(PictureBlobNameClaim, adminUserEntity.PictureInfo?.Name ?? string.Empty),
				new Claim(CultureIdClaim, adminUserEntity.CultureId),
				new Claim(UICultureIdClaim, adminUserEntity.UICultureId),
				new Claim(LocalUserTimeZoneIdClaim, adminUserEntity.TimeZoneId),
				new Claim(ScreenAutoLockMinutesClaim, adminUserEntity.ScreenAutoLockMinutes.ChangeType(string.Empty)),
				new Claim(ScreenLockedClaim, screenLocked.ChangeType(string.Empty)),
				new Claim(IsPersistentClaim, isPersistent.ChangeType(string.Empty))
			};

			//
			claims.AddRange(
				adminUserEntity.Claims.Select(claim => new Claim(claim.Type, claim.Value))
			);

			//
			claims.AddRange(
				adminUserEntity.Roles.Select(role => new Claim(ClaimTypes.Role, role.ToString()))
			);

			return claims;
		}
	}
}
