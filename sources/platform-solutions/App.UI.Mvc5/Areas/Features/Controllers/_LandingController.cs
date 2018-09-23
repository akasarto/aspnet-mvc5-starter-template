using App.UI.Mvc5.Models;
using System.Web.Mvc;

namespace App.UI.Mvc5.Areas.Features.Controllers
{
	[RoutePrefix("")]
	public class _LandingController : __AreaBaseController
	{
		[Route("top-menu-item", Name = "Features_Menu_TopMenuItem")]
		public ActionResult TopMenuItem()
		{
			var model = new EmptyPartialViewModel();

			return PartialView(model);
		}

		[HttpGet]
		[Route(Name = "Features_Landing_Index_Get")]
		public ActionResult Index() => RedirectToAction("Index", "Overview");
	}
}
