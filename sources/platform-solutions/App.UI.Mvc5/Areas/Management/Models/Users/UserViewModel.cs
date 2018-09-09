using App.UI.Mvc5.Infrastructure;
using App.UI.Mvc5.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace App.UI.Mvc5.Areas.Management.Models
{
	public class UserViewModel : BaseViewModel
	{
		[LocalizedDisplayName("Email")]
		public string Email { get; set; }

		[LocalizedDisplayName("FullName")]
		public string FullName { get; set; }

		public int Id { get; set; }

		public MultiSelectList RealmOptions { get; set; }
		public List<string> Realms { get; set; } = new List<string>();
		public MultiSelectList RoleOptions { get; set; }
		public List<string> Roles { get; set; } = new List<string>();

		[LocalizedDisplayName("UserName")]
		public string UserName { get; set; }

		public DateTime UTCCreation { get; set; }
	}
}