using System.Web.Mvc;

namespace App.UI.Mvc5.Areas.Users.Controllers
{
	[RoutePrefix("landing")]
	public class _LandingController : __AreaBaseController
	{
		[HttpGet]
		[Route(Name = "UsersLandingIndexGet")]
		public ActionResult Index() => RedirectToAction("Edit", "Profile");
	}
}
