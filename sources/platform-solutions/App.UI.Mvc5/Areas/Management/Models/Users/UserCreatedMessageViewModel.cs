using App.UI.Mvc5.Models;
using Domain.Core;
using System;
using System.Collections.Generic;

namespace App.UI.Mvc5.Areas.Management.Models
{
	public class UserCreatedMessageViewModel : BaseViewModel
	{
		public List<AllowedRealmInfo> AllowedRealms { get; set; } = new List<AllowedRealmInfo>();
		public string Email { get; set; }
		public string FullName { get; set; }
		public string InitialPassword { get; set; }

		public class AllowedRealmInfo
		{
			public Realm Realm { get; set; }

			public Uri RealmUri { get; set; }
		}
	}
}