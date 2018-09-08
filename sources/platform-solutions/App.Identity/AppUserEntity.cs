using App.Identity.Managers;
using Domain.Core.Entities;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App.Identity
{
	public class AppUserEntity : UserEntity, IUser<int>
	{
		public int ScreenAutoLockMinutes { get; set; }

		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(AppUserManager appUserManager, bool isPersistentState, bool screenLockedState)
		{
			var identity = await appUserManager.CreateIdentityAsync(
				this,
				DefaultAuthenticationTypes.ApplicationCookie
			);

			var claims = AppPrincipal.GetAdminClaims(
				appUserEntity: this,
				isPersistent: isPersistentState,
				screenLocked: screenLockedState
			);

			foreach (var claim in claims)
			{
				if (!identity.HasClaim(claim.Type, claim.Value))
				{
					identity.AddClaim(claim);
				}
			}

			return identity;
		}
	}
}
