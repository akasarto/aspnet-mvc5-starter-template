using App.UI.Mvc5.Infrastructure;
using App.UI.Mvc5.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace App.UI.Mvc5.Areas.Users.Models
{
	public class ProfileEditViewModel : BaseViewModel
	{
		[LocalizedDisplayName("CultureId")]
		public string CultureId { get; set; }

		public SelectList Cultures { get; set; }

		[LocalizedDisplayName("Email")]
		public string Email { get; set; }

		[LocalizedDisplayName("FullName")]
		public string FullName { get; set; }

		public int Id { get; set; }

		[LocalizedDisplayName("PictureBlobId")]
		public Guid? PictureBlobId { get; set; }

		public string PictureBlobName { get; set; }

		public long PictureUploadMaxLengthInBytes { get; set; }
		public List<string> Roles { get; set; }

		[LocalizedDisplayName("TimeZoneId")]
		public string TimeZoneId { get; set; }

		public SelectList TimeZones { get; set; }

		[LocalizedDisplayName("UICultureId")]
		public string UICultureId { get; set; }

		public SelectList UICultures { get; set; }

		[LocalizedDisplayName("UserName")]
		public string UserName { get; set; }
	}
}