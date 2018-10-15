using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace App.Identity.UserStore
{
	public partial class AppUserStore : IUserTwoFactorStore<AppUserEntity, int>
	{
		public Task<bool> GetTwoFactorEnabledAsync(AppUserEntity user)
		{
			return Task.FromResult(user.TwoFactorEnabled);
		}

		public Task SetTwoFactorEnabledAsync(AppUserEntity user, bool enabled)
		{
			user.TwoFactorEnabled = enabled;

			return Task.CompletedTask;
		}
	}
}
