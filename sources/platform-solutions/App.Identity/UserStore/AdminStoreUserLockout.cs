using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace App.Identity
{
	public partial class AdminStore : IUserLockoutStore<AdminUserEntity, int>
	{
		public Task<bool> GetLockoutEnabledAsync(AdminUserEntity user)
		{
			return Task.FromResult(user.LockoutEnabled);
		}

		public Task<int> GetAccessFailedCountAsync(AdminUserEntity user)
		{
			return Task.FromResult(user.AccessFailedCount);
		}

		public Task<DateTimeOffset> GetLockoutEndDateAsync(AdminUserEntity user)
		{
			user.LockoutEndDateUtc = user.LockoutEndDateUtc ?? DateTimeOffset.MinValue;

			return Task.FromResult(user.LockoutEndDateUtc.Value);
		}

		public Task<int> IncrementAccessFailedCountAsync(AdminUserEntity user)
		{
			user.AccessFailedCount = user.AccessFailedCount + 1;

			return Task.FromResult(user.AccessFailedCount);
		}

		public Task ResetAccessFailedCountAsync(AdminUserEntity user)
		{
			user.AccessFailedCount = 0;

			return Task.CompletedTask;
		}

		public Task SetLockoutEnabledAsync(AdminUserEntity user, bool enabled)
		{
			user.LockoutEnabled = enabled;

			return Task.CompletedTask;
		}

		public Task SetLockoutEndDateAsync(AdminUserEntity user, DateTimeOffset lockoutEnd)
		{
			user.LockoutEndDateUtc = lockoutEnd.UtcDateTime;

			return Task.CompletedTask;
		}
	}
}
