using App.UI.Mvc5.Infrastructure;
using App.UI.Mvc5.Models;
using System.Web.Mvc;

namespace App.UI.Mvc5.Areas.Docs.Controllers
{
	[RoutePrefix("overview")]
	[TrackMenuItem("docs.overview")]
	public partial class OverviewController : __AreaBaseController
	{
		[Route(Name = "Docs_Overview_Index_Get")]
		public ActionResult Index()
		{
			var model = new EmptyViewModel();

			return View(model);
		}
	}
}
