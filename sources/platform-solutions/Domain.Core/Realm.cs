using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Core
{
	[Flags]
	public enum Realm : byte
	{
		[Display(Name = "RealmNone", ResourceType = typeof(SharedResources))]
		None = 0,

		[Display(Name = "RealmAdminWebsite", ResourceType = typeof(SharedResources))]
		AdminWebsite = 1,

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
