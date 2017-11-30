using System.Web.Mvc;

namespace App.UI.Mvc5.Areas.Management.Controllers
{
	[RoutePrefix("landing")]
	public class _LandingController : __AreaBaseController
	{
		[HttpGet]
		[Route(Name = "ManagementLandingIndexGet")]
		public ActionResult Index() => RedirectToAction("Index", "Logs");
	}
}
