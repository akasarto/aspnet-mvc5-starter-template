using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace App.Identity
{
	public partial class AdminStore : IUserEmailStore<AdminUserEntity, int>
	{
		public Task<AdminUserEntity> FindByEmailAsync(string email)
		{
			var user = _identityRepository.GetByEmail(email);

			return Task.FromResult(user);
		}

		public Task<string> GetEmailAsync(AdminUserEntity user)
		{
			return Task.FromResult(user.Email);
		}

		public Task<bool> GetEmailConfirmedAsync(AdminUserEntity user)
		{
			return Task.FromResult(user.EmailConfirmed);
		}

		public Task SetEmailAsync(AdminUserEntity user, string email)
		{
			user.Email = email;

			return Task.CompletedTask;
		}

		public Task SetEmailConfirmedAsync(AdminUserEntity user, bool confirmed)
		{
			user.EmailConfirmed = confirmed;

			return Task.CompletedTask;
		}
	}
}
