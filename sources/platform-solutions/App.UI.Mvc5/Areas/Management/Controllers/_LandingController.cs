using System.Web.Mvc;

namespace App.UI.Mvc5.Areas.Management.Controllers
{
	[RoutePrefix("")]
	public class _LandingController : __AreaBaseController
	{
		[HttpGet]
		[Route(Name = "Management_Landing_Index_Get")]
		public ActionResult Index() => RedirectToAction("Index", "Logs");
	}
}
