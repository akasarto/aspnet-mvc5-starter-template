using App.UI.Mvc5.Infrastructure;
using App.UI.Mvc5.Models;

namespace App.UI.Mvc5.Areas.Users.Models
{
	public class AccountSignUpViewModel : BaseViewModel
	{
		[LocalizedDisplayName("Email")]
		public string Email { get; set; }

		[LocalizedDisplayName("Name")]
		public string Name { get; set; }

		[LocalizedDisplayName("Password")]
		public string Password { get; set; }

		[LocalizedDisplayName("Password2")]
		public string Password2 { get; set; }

		[LocalizedDisplayName("TermsAcceptance")]
		public bool TermsAcceptance { get; set; }
	}
}