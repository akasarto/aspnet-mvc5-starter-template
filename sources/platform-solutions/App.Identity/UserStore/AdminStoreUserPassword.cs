using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace App.Identity
{
	public partial class AdminStore : IUserPasswordStore<AdminUserEntity, int>
	{
		public Task<string> GetPasswordHashAsync(AdminUserEntity user)
		{
			return Task.FromResult(user.PasswordHash);
		}

		public Task<bool> HasPasswordAsync(AdminUserEntity user)
		{
			var hasPassword = !string.IsNullOrWhiteSpace(user.PasswordHash);

			return Task.FromResult(hasPassword);
		}

		public Task SetPasswordHashAsync(AdminUserEntity user, string passwordHash)
		{
			user.PasswordHash = passwordHash;

			return Task.CompletedTask;
		}
	}
}
