using App.UI.Mvc5.Infrastructure;
using App.UI.Mvc5.Models;
using System.Web.Mvc;

namespace App.UI.Mvc5.Areas.Blank.Controllers
{
	[RoutePrefix("overview")]
	[TrackMenuItem("blank.overview")]
	public partial class OverviewController : __AreaBaseController
	{
		[Route(Name = "Blank_Overview_Index_Get")]
		public ActionResult Index()
		{
			var model = new EmptyViewModel();

			return View(model);
		}
	}
}
