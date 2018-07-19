using Microsoft.AspNet.Identity;
using Shared.Extensions;
using Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Identity
{
	public partial class AdminStore : IUserRoleStore<AdminUserEntity, int>
	{
		public Task<IList<string>> GetRolesAsync(AdminUserEntity user)
		{
			var roles = user.Roles.Select(role => role.ToString()).ToList();

			return Task.FromResult<IList<string>>(roles);
		}

		public Task AddToRoleAsync(AdminUserEntity user, string roleId)
		{
			var role = roleId.ChangeType(Role.None);

			if (role == Role.None)
			{
				throw new Exception($"Invalid role at {nameof(AdminStore)} => {nameof(AddToRoleAsync)}.");
			}

			user.Roles.Add(role);

			return Task.CompletedTask;
		}

		public Task<bool> IsInRoleAsync(AdminUserEntity user, string roleId)
		{
			var isInRole = user.Roles.Any(role =>
				role.Equals(
					roleId.ChangeType<Role>()
				)
			);

			return Task.FromResult(isInRole);
		}

		public Task RemoveFromRoleAsync(AdminUserEntity user, string roleId)
		{
			var role = roleId.ChangeType<Role>();

			if (role == Role.None)
			{
				throw new Exception($"Invalid role at {nameof(AdminStore)} => {nameof(RemoveFromRoleAsync)}.");
			}

			user.Roles.Remove(role);

			return Task.CompletedTask;
		}
	}
}
