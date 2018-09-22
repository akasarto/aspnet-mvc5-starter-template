using App.UI.Mvc5.Models;
using System.Web.Mvc;

namespace App.UI.Mvc5.Areas.Docs.Controllers
{
	[RoutePrefix("entry-point")]
	public class _EntryPointController : __AreaBaseController
	{
		[Route("main-menu-item", Name = "DocsRootMainMenuItemGet")]
		public ActionResult MainMenuItem()
		{
			var model = new EmptyViewModel();

			return PartialView(model);
		}

		[HttpGet]
		[Route(Name = "DocsLandingIndexGet")]
		public ActionResult Index() => RedirectToAction("Index", "Architecture");
	}
}
