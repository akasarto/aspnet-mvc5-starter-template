using App.UI.Mvc5.Infrastructure;
using App.UI.Mvc5.Models;

namespace App.UI.Mvc5.Areas.Users.Models
{
	public class AccountLogInViewModel : BaseViewModel
	{
		[LocalizedDisplayName("EmailOrUsername")]
		public string EmailOrUsername { get; set; }

		[LocalizedDisplayName("Password")]
		public string Password { get; set; }

		[LocalizedDisplayName("RememberMe")]
		public bool RememberMe { get; set; }
	}
}
