using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace App.Identity.UserStore
{
	public partial class AppUserStore : IUserPasswordStore<AppUserEntity, int>
	{
		public Task<string> GetPasswordHashAsync(AppUserEntity user)
		{
			return Task.FromResult(user.PasswordHash);
		}

		public Task<bool> HasPasswordAsync(AppUserEntity user)
		{
			var hasPassword = !string.IsNullOrWhiteSpace(user.PasswordHash);

			return Task.FromResult(hasPassword);
		}

		public Task SetPasswordHashAsync(AppUserEntity user, string passwordHash)
		{
			user.PasswordHash = passwordHash;

			return Task.CompletedTask;
		}
	}
}
