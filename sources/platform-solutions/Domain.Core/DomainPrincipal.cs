using Shared.Extensions;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Domain.Core
{
	public class DomainPrincipal : IPrincipal
	{
		private readonly IPrincipal _principal = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		public DomainPrincipal(IPrincipal principal)
		{
			_principal = principal;
		}

		public IIdentity Identity => _principal?.Identity;

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

		public bool IsInRole(string role) => IsInRole(role.ChangeType<Role>());

		public bool IsInRole(Role role) => _principal?.IsInRole(role.ToString()) ?? false;

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
