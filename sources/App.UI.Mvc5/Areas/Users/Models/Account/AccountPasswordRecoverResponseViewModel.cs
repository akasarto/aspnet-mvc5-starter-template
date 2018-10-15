using App.UI.Mvc5.Infrastructure;
using App.UI.Mvc5.Models;

namespace App.UI.Mvc5.Areas.Users.Models
{
	public class AccountPasswordRecoverResponseViewModel : BaseViewModel
	{
		[LocalizedDisplayName("Email", ResourceType = typeof(AreaResources))]
		public string Email { get; set; }

		[LocalizedDisplayName("NewPassword", ResourceType = typeof(AreaResources))]
		public string NewPassword { get; set; }

		[LocalizedDisplayName("NewPassword2", ResourceType = typeof(AreaResources))]
		public string NewPassword2 { get; set; }

		public string ResetToken { get; set; }
	}
}
