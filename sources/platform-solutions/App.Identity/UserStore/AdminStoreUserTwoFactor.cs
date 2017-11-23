using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace App.Identity
{
	public partial class AdminStore : IUserTwoFactorStore<AdminUserEntity, int>
	{
		public Task<bool> GetTwoFactorEnabledAsync(AdminUserEntity user)
		{
			return Task.FromResult(user.TwoFactorEnabled);
		}

		public Task SetTwoFactorEnabledAsync(AdminUserEntity user, bool enabled)
		{
			user.TwoFactorEnabled = enabled;

			return Task.CompletedTask;
		}
	}
}
