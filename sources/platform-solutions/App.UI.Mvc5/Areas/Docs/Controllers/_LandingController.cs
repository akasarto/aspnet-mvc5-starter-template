using System.Web.Mvc;

namespace App.UI.Mvc5.Areas.Docs.Controllers
{
	[RoutePrefix("")]
	public class _LandingController : __AreaBaseController
	{
		[HttpGet]
		[Route(Name = "Docs_Landing_Index_Get")]
		public ActionResult Index() => RedirectToAction("Index", "Architecture");
	}
}
