using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace App.Identity
{
	public partial class AdminStore : IUserStore<AdminUserEntity, int>
	{
		public Task CreateAsync(AdminUserEntity user)
		{
			user.Id = _identityRepository.Create(user).Id;

			if (user.Id <= 0)
			{
				throw new Exception($"Invalid user id at {nameof(AdminStore)} => {nameof(CreateAsync)}.");
			}

			return Task.CompletedTask;
		}

		public Task<AdminUserEntity> FindByIdAsync(int userId)
		{
			var user = _identityRepository.GetById(userId);

			return Task.FromResult(user);
		}

		public Task<AdminUserEntity> FindByNameAsync(string userName)
		{
			var user = _identityRepository.GetByUserName(userName);

			return Task.FromResult(user);
		}

		public Task UpdateAsync(AdminUserEntity user)
		{
			_identityRepository.UpdateIdentity(user);

			return Task.CompletedTask;
		}

		public Task DeleteAsync(AdminUserEntity user)
		{
			return Task.CompletedTask;
		}
	}
}
