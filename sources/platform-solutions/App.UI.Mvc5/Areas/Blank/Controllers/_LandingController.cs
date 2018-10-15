using System.Web.Mvc;

namespace App.UI.Mvc5.Areas.Blank.Controllers
{
	[RoutePrefix("")]
	public class _LandingController : __AreaBaseController
	{
		[HttpGet]
		[Route(Name = "Blank_Landing_Index_Get")]
		public ActionResult Index() => RedirectToAction("Index", "Overview");
	}
}
