using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Core.Entities;

namespace App.Identity
{
	/// <summary>
	/// Represents a user for the platform administration app/website.
	/// </summary>
	public class AdminUserEntity : UserEntity, IUser<int>
	{
		/// <summary>
		/// Gets or sets how long, in minutes, the screen will be automatically locked.
		/// </summary>
		public int ScreenAutoLockMinutes { get; set; }

		/// <summary>
		/// Creates a new <see cref="ClaimsIdentity"/> from this instance.
		/// </summary>
		/// <param name="manager">The current <see cref="AdminUserManager"/> instance.</param>
		/// <returns>A new <see cref="ClaimsIdentity"/> instance.</returns>
		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(AdminUserManager manager, bool isPersistentState, bool screenLockedState)
		{
			var identity = await manager.CreateIdentityAsync(
				this,
				DefaultAuthenticationTypes.ApplicationCookie
			);

			var claims = AdminPrincipal.GetAdminClaims(
				adminUserEntity: this,
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
