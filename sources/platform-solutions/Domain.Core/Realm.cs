using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Core
{
	/// <summary>
	/// Represent a recognized system that can be integrated with the platform.
	/// </summary>
	[Flags]
	public enum Realm : byte
	{
		/// <summary>
		/// None #0
		/// </summary>
		[Display(Name = "RealmNone", ResourceType = typeof(SharedResources))]
		None = 0,

		/// <summary>
		/// AdminWebsite #1
		/// </summary>
		[Display(Name = "RealmAdminWebsite", ResourceType = typeof(SharedResources))]
		AdminWebsite = 1,

		/// <summary>
		/// WebApi #2
		/// </summary>
		[Display(Name = "RealmWebApi", ResourceType = typeof(SharedResources))]
		WebApi = 2

		// Use powers of two for new realms.
		// This is REQUIRED as the system will perform bitwise operations in this enum.
		//Sample = 4,
		//Sample = 8,
		//Sample = 16,
		//Sample = 32,
		//Sample = 64,
		//Sample...
	}
}
