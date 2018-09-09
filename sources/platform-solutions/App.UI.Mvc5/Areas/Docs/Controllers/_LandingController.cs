using System.Web.Mvc;

namespace App.UI.Mvc5.Areas.Docs.Controllers
{
	[RoutePrefix("landing")]
	public class _LandingController : __AreaBaseController
	{
		[HttpGet]
		[Route(Name = "DocsLandingIndexGet")]
		public ActionResult Index() => RedirectToAction("Index", "Docs");
	}
}