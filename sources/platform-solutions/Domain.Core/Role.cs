using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Core
{
	/// <summary>
	/// Represent a set of roles that user can be part of.
	/// </summary>
	[Flags]
	public enum Role
	{
		/// <summary>
		/// None #0
		/// </summary>
		[Display(Name = "RoleNone", ResourceType = typeof(SharedResources))]
		None = 0,

		/// <summary>
		/// Basic #1
		/// </summary>
		[Display(Name = "RoleBasic", ResourceType = typeof(SharedResources))]
		Basic = 1,

		/// <summary>
		/// SuperUser #2
		/// </summary>
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
