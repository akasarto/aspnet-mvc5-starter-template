using System.Web.Mvc;

namespace App.UI.Mvc5.Areas.Users.Controllers
{
	[RoutePrefix("")]
	public class _LandingController : __AreaBaseController
	{
		[HttpGet]
		[Route(Name = "Users_Landing_Index_Get")]
		public ActionResult Index() => RedirectToAction("Edit", "Profile");
	}
}
