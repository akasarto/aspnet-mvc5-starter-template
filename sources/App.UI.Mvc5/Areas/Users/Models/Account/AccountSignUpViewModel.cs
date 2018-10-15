using App.UI.Mvc5.Infrastructure;
using App.UI.Mvc5.Models;

namespace App.UI.Mvc5.Areas.Users.Models
{
	public class AccountSignUpViewModel : BaseViewModel
	{
		[LocalizedDisplayName("Email", ResourceType = typeof(AreaResources))]
		public string Email { get; set; }

		[LocalizedDisplayName("Name", ResourceType = typeof(AreaResources))]
		public string Name { get; set; }

		[LocalizedDisplayName("Password", ResourceType = typeof(AreaResources))]
		public string Password { get; set; }

		[LocalizedDisplayName("Password2", ResourceType = typeof(AreaResources))]
		public string Password2 { get; set; }

		[LocalizedDisplayName("TermsAcceptance", ResourceType = typeof(AreaResources))]
		public bool TermsAcceptance { get; set; }
	}
}
