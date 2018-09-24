using App.UI.Mvc5.Infrastructure;
using App.UI.Mvc5.Models;

namespace App.UI.Mvc5.Areas.Users.Models
{
	public class AccountPasswordChangeViewModel : BaseViewModel
	{
		[LocalizedDisplayName("NewPassword", ResourceType = typeof(AreaResources))]
		public string NewPassword { get; set; }

		[LocalizedDisplayName("NewPassword2", ResourceType = typeof(AreaResources))]
		public string NewPassword2 { get; set; }

		[LocalizedDisplayName("CurrentPassword", ResourceType = typeof(AreaResources))]
		public string Password { get; set; }
	}
}
