using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace App.Identity
{
	public partial class AdminStore : IUserSecurityStampStore<AdminUserEntity, int>
	{
		public Task<string> GetSecurityStampAsync(AdminUserEntity user)
		{
			return Task.FromResult(user.SecurityStamp);
		}

		public Task SetSecurityStampAsync(AdminUserEntity user, string stamp)
		{
			user.SecurityStamp = stamp;

			return Task.CompletedTask;
		}
	}
}
