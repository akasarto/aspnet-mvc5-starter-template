using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace App.Identity.UserStore
{
	public partial class AppUserStore : IUserSecurityStampStore<AppUserEntity, int>
	{
		public Task<string> GetSecurityStampAsync(AppUserEntity user)
		{
			return Task.FromResult(user.SecurityStamp);
		}

		public Task SetSecurityStampAsync(AppUserEntity user, string stamp)
		{
			user.SecurityStamp = stamp;

			return Task.CompletedTask;
		}
	}
}
