using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Core
{
	[Flags]
	public enum Role
	{
		[Display(Name = "RoleNone", ResourceType = typeof(SharedResources))]
		None = 0,

		[Display(Name = "RoleBasic", ResourceType = typeof(SharedResources))]
		Basic = 1,

		[Display(Name = "RoleSuperUser", ResourceType = typeof(SharedResources))]
		SuperUser = 2,

		// Use powers of two for new roles.
		// This is REQUIRED as the system will perform bitwise operations with this enum.
		//Sample = 4,
		//Sample = 8,
		//Sample = 16,
		//Sample = 32,
		//Sample = 64,
		//Sample...

	}
}
