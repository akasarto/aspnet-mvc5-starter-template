using App.UI.Mvc5.Infrastructure;
using App.UI.Mvc5.Models;

namespace App.UI.Mvc5.Areas.Users.Models
{
	public class AccountPasswordChangeViewModel : BaseViewModel
	{
		[LocalizedDisplayName("NewPassword")]
		public string NewPassword { get; set; }

		[LocalizedDisplayName("NewPassword2")]
		public string NewPassword2 { get; set; }

		[LocalizedDisplayName("CurrentPassword")]
		public string Password { get; set; }
	}
}