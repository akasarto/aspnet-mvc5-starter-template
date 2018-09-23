using App.UI.Mvc5.Infrastructure;
using App.UI.Mvc5.Models;
using System.Web.Mvc;

namespace App.UI.Mvc5.Controllers
{
	[RoutePrefix("")]
	[TrackMenuItem("root.landing")]
	public class HomeController : __BaseController
	{
		[HttpGet]
		[Route(Name = "Root_Home_Index_Get")]
		public ActionResult Index()
		{
			var model = new EmptyViewModel();

			return View(model);
		}
	}
}
