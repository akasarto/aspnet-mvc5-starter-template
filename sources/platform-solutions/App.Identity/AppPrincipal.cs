using Domain.Core.Entities;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace App.Identity
{
	[Serializable]
	public class AppPrincipal : UserPrincipal
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

		public AppPrincipal() : base(new ClaimsPrincipal())
		{
		}

		public AppPrincipal(IPrincipal principal) : base(principal)
		{
		}

		public int Id
		{
			get => GetClaimValue<int>(IdClaim);
			set => SetClaim(IdClaim, value);
		}

		public string FullName
		{
			get => GetClaimValue<string>(FullNameClaim);
			set => SetClaim(FullNameClaim, value);
		}

		public string UserName
		{
			get => GetClaimValue<string>(UserNameClaim);
			set => SetClaim(UserNameClaim, value);
		}

		public string Email
		{
			get => GetClaimValue<string>(EmailClaim);
			set => SetClaim(EmailClaim, value);
		}

		public bool EmailConfirmed
		{
			get => GetClaimValue<bool>(EmailConfirmedClaim);
			set => SetClaim(EmailConfirmedClaim, value);
		}

		public Guid? PictureBlobId
		{
			get => GetClaimValue<Guid?>(PictureBlobIdClaim);
			set => SetClaim(PictureBlobIdClaim, value);
		}

		public string PictureBlobName
		{
			get => GetClaimValue<string>(PictureBlobNameClaim);
			set => SetClaim(PictureBlobNameClaim, value);
		}

		public string CultureId
		{
			get => GetClaimValue<string>(CultureIdClaim);
			set => SetClaim(CultureIdClaim, value);
		}

		public string UICultureId
		{
			get => GetClaimValue<string>(UICultureIdClaim);
			set => SetClaim(UICultureIdClaim, value);
		}

		public string TimeZoneId
		{
			get => GetClaimValue<string>(LocalUserTimeZoneIdClaim, "UTC");
			set => SetClaim(LocalUserTimeZoneIdClaim, value);
		}

		public int ScreenAutoLockMinutes
		{
			get => GetClaimValue<int>(ScreenAutoLockMinutesClaim);
			set => SetClaim(ScreenAutoLockMinutesClaim, value);
		}

		public bool ScreenLocked
		{
			get => GetClaimValue<bool>(ScreenLockedClaim);
			set => SetClaim(ScreenLockedClaim, value);
		}

		public bool IsPersistent
		{
			get => GetClaimValue<bool>(IsPersistentClaim);
			set => SetClaim(IsPersistentClaim, value);
		}

		public CultureInfo Culture { get; set; }

		public CultureInfo UICulture { get; set; }

		public TimeZoneInfo TimeZone { get; set; }

		public static List<Claim> GetAdminClaims(AppUserEntity appUserEntity, bool isPersistent, bool screenLocked)
		{
			//
			var claims = new List<Claim>()
			{
				new Claim(IdClaim, appUserEntity.Id.ToString()),
				new Claim(FullNameClaim, appUserEntity.FullName),
				new Claim(UserNameClaim, appUserEntity.UserName),
				new Claim(EmailClaim, appUserEntity.Email),
				new Claim(EmailConfirmedClaim, appUserEntity.EmailConfirmed.ChangeType<string>()),
				new Claim(PictureBlobIdClaim, appUserEntity.PictureInfo?.Id.ToString() ?? string.Empty),
				new Claim(PictureBlobNameClaim, appUserEntity.PictureInfo?.Name ?? string.Empty),
				new Claim(CultureIdClaim, appUserEntity.CultureId),
				new Claim(UICultureIdClaim, appUserEntity.UICultureId),
				new Claim(LocalUserTimeZoneIdClaim, appUserEntity.TimeZoneId),
				new Claim(ScreenAutoLockMinutesClaim, appUserEntity.ScreenAutoLockMinutes.ChangeType(string.Empty)),
				new Claim(ScreenLockedClaim, screenLocked.ChangeType(string.Empty)),
				new Claim(IsPersistentClaim, isPersistent.ChangeType(string.Empty))
			};

			//
			claims.AddRange(
				appUserEntity.Claims.Select(claim => new Claim(claim.Type, claim.Value))
			);

			//
			claims.AddRange(
				appUserEntity.Roles.Select(role => new Claim(ClaimTypes.Role, role.ToString()))
			);

			return claims;
		}
	}
}
