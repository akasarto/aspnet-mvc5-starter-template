using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace App.Identity.UserStore
{
	public partial class AppUserStore : IUserStore<AppUserEntity, int>
	{
		public Task CreateAsync(AppUserEntity user)
		{
			user.Id = _identityRepository.Create(user).Id;

			if (user.Id <= 0)
			{
				throw new Exception($"Invalid user id at {nameof(AppUserStore)} => {nameof(CreateAsync)}.");
			}

			return Task.CompletedTask;
		}

		public Task<AppUserEntity> FindByIdAsync(int userId)
		{
			var user = _identityRepository.GetById(userId);

			return Task.FromResult(user);
		}

		public Task<AppUserEntity> FindByNameAsync(string userName)
		{
			var user = _identityRepository.GetByUserName(userName);

			return Task.FromResult(user);
		}

		public Task UpdateAsync(AppUserEntity user)
		{
			_identityRepository.UpdateIdentity(user);

			return Task.CompletedTask;
		}

		public Task DeleteAsync(AppUserEntity user)
		{
			return Task.CompletedTask;
		}
	}
}
