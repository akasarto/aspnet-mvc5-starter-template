using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace App.Identity.UserStore
{
	public partial class AppUserStore : IUserEmailStore<AppUserEntity, int>
	{
		public Task<AppUserEntity> FindByEmailAsync(string email)
		{
			var user = _identityRepository.GetByEmail(email);

			return Task.FromResult(user);
		}

		public Task<string> GetEmailAsync(AppUserEntity user)
		{
			return Task.FromResult(user.Email);
		}

		public Task<bool> GetEmailConfirmedAsync(AppUserEntity user)
		{
			return Task.FromResult(user.EmailConfirmed);
		}

		public Task SetEmailAsync(AppUserEntity user, string email)
		{
			user.Email = email;

			return Task.CompletedTask;
		}

		public Task SetEmailConfirmedAsync(AppUserEntity user, bool confirmed)
		{
			user.EmailConfirmed = confirmed;

			return Task.CompletedTask;
		}
	}
}
