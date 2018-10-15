using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace App.Identity.UserStore
{
	public partial class AppUserStore : IUserLockoutStore<AppUserEntity, int>
	{
		public Task<bool> GetLockoutEnabledAsync(AppUserEntity user)
		{
			return Task.FromResult(user.LockoutEnabled);
		}

		public Task<int> GetAccessFailedCountAsync(AppUserEntity user)
		{
			return Task.FromResult(user.AccessFailedCount);
		}

		public Task<DateTimeOffset> GetLockoutEndDateAsync(AppUserEntity user)
		{
			user.LockoutEndDateUtc = user.LockoutEndDateUtc ?? DateTimeOffset.MinValue;

			return Task.FromResult(user.LockoutEndDateUtc.Value);
		}

		public Task<int> IncrementAccessFailedCountAsync(AppUserEntity user)
		{
			user.AccessFailedCount = user.AccessFailedCount + 1;

			return Task.FromResult(user.AccessFailedCount);
		}

		public Task ResetAccessFailedCountAsync(AppUserEntity user)
		{
			user.AccessFailedCount = 0;

			return Task.CompletedTask;
		}

		public Task SetLockoutEnabledAsync(AppUserEntity user, bool enabled)
		{
			user.LockoutEnabled = enabled;

			return Task.CompletedTask;
		}

		public Task SetLockoutEndDateAsync(AppUserEntity user, DateTimeOffset lockoutEnd)
		{
			user.LockoutEndDateUtc = lockoutEnd.UtcDateTime;

			return Task.CompletedTask;
		}
	}
}
