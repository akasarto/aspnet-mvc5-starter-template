using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App.Identity.Managers
{
	public class AppSignInManager : SignInManager<AppUserEntity, int>
	{
		private AppUserManager _userManager = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		public AppSignInManager(AppUserManager userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
		{
			_userManager = userManager;
		}

		public bool InitialPersistenceState { get; set; }

		public override Task<ClaimsIdentity> CreateUserIdentityAsync(AppUserEntity user)
		{
			return user.GenerateUserIdentityAsync(
				_userManager,
				isPersistentState: InitialPersistenceState,
				screenLockedState: false
			);
		}

		public async Task RefreshIdentity(AppPrincipal principal, bool reloadClaims = false)
		{
			ClaimsIdentity identity = null;

			var isPersistent = principal.IsPersistent;

			if (reloadClaims)
			{
				var adminUser = await _userManager.FindByIdAsync(principal.Id);

				if (adminUser != null)
				{
					identity = await adminUser.GenerateUserIdentityAsync(
						_userManager,
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
