using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App.Identity.UserStore
{
	public partial class AppUserStore : IUserClaimStore<AppUserEntity, int>
	{
		public Task AddClaimAsync(AppUserEntity user, Claim claim)
		{
			user.Claims.Add(claim);

			return Task.CompletedTask;
		}

		public Task<IList<Claim>> GetClaimsAsync(AppUserEntity user)
		{
			return Task.FromResult<IList<Claim>>(user.Claims);
		}

		public Task RemoveClaimAsync(AppUserEntity user, Claim claim)
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
