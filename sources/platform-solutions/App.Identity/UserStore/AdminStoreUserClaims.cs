using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App.Identity
{
	public partial class AdminStore : IUserClaimStore<AdminUserEntity, int>
	{
		public Task AddClaimAsync(AdminUserEntity user, Claim claim)
		{
			user.Claims.Add(claim);

			return Task.CompletedTask;
		}

		public Task<IList<Claim>> GetClaimsAsync(AdminUserEntity user)
		{
			return Task.FromResult<IList<Claim>>(user.Claims);
		}

		public Task RemoveClaimAsync(AdminUserEntity user, Claim claim)
		{
			claim = user.Claims.SingleOrDefault(uc => uc.Type == claim.Type);

			if (claim != null)
			{
				user.Claims.Remove(claim);
			}

			return Task.CompletedTask;
		}
	}
}
