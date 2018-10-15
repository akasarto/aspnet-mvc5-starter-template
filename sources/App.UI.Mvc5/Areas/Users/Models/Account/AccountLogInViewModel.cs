using App.UI.Mvc5.Infrastructure;
using App.UI.Mvc5.Models;

namespace App.UI.Mvc5.Areas.Users.Models
{
	public class AccountLogInViewModel : BaseViewModel
	{
		[LocalizedDisplayName("EmailOrUsername", ResourceType = typeof(AreaResources))]
		public string EmailOrUsername { get; set; }

		[LocalizedDisplayName("Password", ResourceType = typeof(AreaResources))]
		public string Password { get; set; }

		[LocalizedDisplayName("RememberMe", ResourceType = typeof(AreaResources))]
		public bool RememberMe { get; set; }
	}
}
