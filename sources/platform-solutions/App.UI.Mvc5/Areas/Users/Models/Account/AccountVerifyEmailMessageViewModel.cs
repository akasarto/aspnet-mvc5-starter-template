using App.UI.Mvc5.Models;

namespace App.UI.Mvc5.Areas.Users.Models
{
	public class AccountVerifyEmailMessageViewModel : BaseViewModel
	{
		public string ConfirmationLink { get; set; }
		public string Name { get; set; }
	}
}