using App.UI.Mvc5.Infrastructure;
using App.UI.Mvc5.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace App.UI.Mvc5.Areas.Users.Models
{
	public class ProfileEditViewModel : BaseViewModel
	{
		[LocalizedDisplayName("CultureId", ResourceType = typeof(AreaResources))]
		public string CultureId { get; set; }

		public SelectList Cultures { get; set; }

		[LocalizedDisplayName("Email", ResourceType = typeof(AreaResources))]
		public string Email { get; set; }

		[LocalizedDisplayName("FullName", ResourceType = typeof(AreaResources))]
		public string FullName { get; set; }

		public int Id { get; set; }

		[LocalizedDisplayName("PictureBlobId", ResourceType = typeof(AreaResources))]
		public Guid? PictureBlobId { get; set; }

		public string PictureBlobName { get; set; }

		public long PictureUploadMaxLengthInBytes { get; set; }
		public List<string> Roles { get; set; }

		[LocalizedDisplayName("TimeZoneId", ResourceType = typeof(AreaResources))]
		public string TimeZoneId { get; set; }

		public SelectList TimeZones { get; set; }

		[LocalizedDisplayName("UICultureId", ResourceType = typeof(AreaResources))]
		public string UICultureId { get; set; }

		public SelectList UICultures { get; set; }

		[LocalizedDisplayName("UserName", ResourceType = typeof(AreaResources))]
		public string UserName { get; set; }
	}
}
