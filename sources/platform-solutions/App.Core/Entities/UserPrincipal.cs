using Sarto.Extensions;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace App.Core.Entities
{
	/// <summary>
	/// Common principal for the system.
	/// </summary>
	public class UserPrincipal : IPrincipal
	{
		private IPrincipal _principal = null;
		private IIdentity _identity = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		/// <param name="principal">The base principal isntance.</param>
		public UserPrincipal(IPrincipal principal)
		{
			_principal = principal;
			_identity = principal?.Identity;
		}

		/// <summary>
		/// Gets the principal identity.
		/// </summary>
		public IIdentity Identity => _identity;

		/// <summary>
		/// Checks if the principal has the specified role.
		/// </summary>
		/// <param name="role">The role to check.</param>
		/// <returns><c>True</c> or <c>false</c>.</returns>
		public bool IsInRole(string role) => IsInRole(role.ChangeType<Role>());

		/// <summary>
		/// Checks if the principal has the specified role.
		/// </summary>
		/// <param name="role">The role to check.</param>
		/// <returns><c>True</c> or <c>false</c>.</returns>
		public bool IsInRole(Role role) => _principal?.IsInRole(role.ToString()) ?? false;

		/// <summary>
		/// Checks if the principal has all the specified roles.
		/// </summary>
		/// <param name="roles">The roles to check.</param>
		/// <returns><c>True</c> or <c>false</c>.</returns>
		public bool IsInRoleAll(params Role[] roles)
		{
			var roleNames = roles.Select(role => role.ToString());

			foreach (var name in roleNames)
			{
				var isInRole = _principal?.IsInRole(name) ?? false;

				if (!isInRole)
				{
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Checks if the principal has any of the specified roles.
		/// </summary>
		/// <param name="roles">The roles to check.</param>
		/// <returns><c>True</c> or <c>false</c>.</returns>
		public bool IsInRoleAny(params Role[] roles)
		{
			var roleNames = roles.Select(role => role.ToString());

			foreach (var name in roleNames)
			{
				var isInRole = _principal?.IsInRole(name) ?? false;

				if (isInRole)
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Get a value from the principal identity roles.
		/// </summary>
		/// <typeparam name="T">The expected return type.</typeparam>
		/// <param name="claimType">The claim to get the value.</param>
		/// <param name="defaultValue">The expected default value in case the role is no presents.</param>
		/// <returns>The claim value as <typeparamref name="T"/>.</returns>
		public T GetClaimValue<T>(string claimType, T defaultValue = default(T))
		{
			var ci = Identity as ClaimsIdentity;

			var claim = ci?.FindFirst(c => c.Type.Equals(claimType, StringComparison.InvariantCultureIgnoreCase)) ?? null;

			if (claim == null || claim.Value == null)
			{
				return defaultValue;
			}

			return claim.Value.ChangeType(defaultValue);
		}

		/// <summary>
		/// Set a new value for the specified claim type.
		/// </summary>
		/// <typeparam name="T">The original value type.</typeparam>
		/// <param name="claimType">The claim type to be set.</param>
		/// <param name="value">The value to associate with the claim.</param>
		public void SetClaim<T>(string claimType, T value)
		{
			var ci = Identity as ClaimsIdentity;

			if (ci != null)
			{
				var claims = ci.FindAll(claimType).ToList();

				claims.ForEach(claim => ci.RemoveClaim(claim));

				ci.AddClaim(
					new Claim(
						claimType,
						value?.ToString() ?? string.Empty
					)
				);
			}
		}
	}
}
