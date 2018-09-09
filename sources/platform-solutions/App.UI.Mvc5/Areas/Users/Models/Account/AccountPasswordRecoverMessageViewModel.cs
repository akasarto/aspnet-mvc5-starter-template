using App.UI.Mvc5.Models;

namespace App.UI.Mvc5.Areas.Users.Models
{
	public class AccountPasswordRecoverMessageViewModel : BaseViewModel
	{
		public string Name { get; set; }

		public string ResetLink { get; set; }
	}
}