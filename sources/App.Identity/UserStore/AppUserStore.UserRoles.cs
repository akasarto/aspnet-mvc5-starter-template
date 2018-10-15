using Domain.Core;
using Microsoft.AspNet.Identity;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Identity.UserStore
{
	public partial class AppUserStore : IUserRoleStore<AppUserEntity, int>
	{
		public Task<IList<string>> GetRolesAsync(AppUserEntity user)
		{
			var roles = user.Roles.Select(role => role.ToString()).ToList();

			return Task.FromResult<IList<string>>(roles);
		}

		public Task AddToRoleAsync(AppUserEntity user, string roleId)
		{
			var role = roleId.ChangeType(Role.None);

			if (role == Role.None)
			{
				throw new Exception($"Invalid role at {nameof(AppUserStore)} => {nameof(AddToRoleAsync)}.");
			}

			user.Roles.Add(role);

			return Task.CompletedTask;
		}

		public Task<bool> IsInRoleAsync(AppUserEntity user, string roleId)
		{
			var isInRole = user.Roles.Any(role =>
				role.Equals(
					roleId.ChangeType<Role>()
				)
			);

			return Task.FromResult(isInRole);
		}

		public Task RemoveFromRoleAsync(AppUserEntity user, string roleId)
		{
			var role = roleId.ChangeType<Role>();

			if (role == Role.None)
			{
				throw new Exception($"Invalid role at {nameof(AppUserStore)} => {nameof(RemoveFromRoleAsync)}.");
			}

			user.Roles.Remove(role);

			return Task.CompletedTask;
		}
	}
}
