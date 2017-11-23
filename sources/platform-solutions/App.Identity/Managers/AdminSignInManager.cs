using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App.Identity
{
	/// <summary>
	/// Asp.Net identity sign in manager for the admin website.
	/// </summary>
	public class AdminSignInManager : SignInManager<AdminUserEntity, int>
	{
		private AdminUserManager _adminUserManager = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		/// <param name="adminUserManager">The current user manager.</param>
		/// <param name="authenticationManager">The current authentication manager.</param>
		public AdminSignInManager(AdminUserManager adminUserManager, IAuthenticationManager authenticationManager) : base(adminUserManager, authenticationManager)
		{
			_adminUserManager = adminUserManager;
		}

		/// <summary>
		/// Allow the manager to consider the initial state chosen by the user (remeber me) or imposed by the app (signup) when creating a new identity.
		/// </summary>
		/// <remarks>This information will be associated to current principal and will be used when refreshing the identity.</remarks>
		public bool InitialPersistenceState { get; set; }

		/// <summary>
		/// Create a nex <see cref="ClaimsIdentity"/> with the current user information.
		/// </summary>
		/// <param name="user">The signed in user.</param>
		/// <returns>A <see cref="ClaimsIdentity"/> <see cref="Task"/>.</returns>
		public override Task<ClaimsIdentity> CreateUserIdentityAsync(AdminUserEntity user)
		{
			return user.GenerateUserIdentityAsync(
				_adminUserManager,
				isPersistentState: InitialPersistenceState,
				screenLockedState: false
			);
		}

		/// <summary>
		/// Recreates the user cookie with updated claims information.
		/// </summary>
		/// <param name="principal">Current principal instance.</param>
		/// <param name="reloadClaims">Flags if claims should be read from database again.</param>
		/// <returns>A <see cref="Task"/>.</returns>
		public async Task RefreshIdentity(AdminPrincipal principal, bool reloadClaims = false)
		{
			ClaimsIdentity identity = null;

			var isPersistent = principal.IsPersistent;

			if (reloadClaims)
			{
				var adminUser = await _adminUserManager.FindByIdAsync(principal.Id);

				if (adminUser != null)
				{
					identity = await adminUser.GenerateUserIdentityAsync(
						_adminUserManager,
						isPersistentState: isPersistent,
						screenLockedState: false
					);
				}
			}
			else
			{
				identity = principal.Identity as ClaimsIdentity;
			}

			if (identity == null)
			{
				throw new Exception("The provided principal has an invalid identity.", new Exception(nameof(RefreshIdentity)));
			}

			AuthenticationManager.AuthenticationResponseGrant = new AuthenticationResponseGrant(
				identity,
				new AuthenticationProperties { IsPersistent = isPersistent }
			);
		}
	}
}
