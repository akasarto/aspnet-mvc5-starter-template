using App.UI.Mvc5.Infrastructure;
using App.UI.Mvc5.Models;
using System.Web.Mvc;

namespace App.UI.Mvc5.Areas.Features.Controllers
{
	[RoutePrefix("overview")]
	[TrackMenuItem("features.overview")]
	public partial class OverviewController : __AreaBaseController
	{
		/// <summary>
		/// Constructor method.
		/// </summary>
		public OverviewController()
		{
		}

		[HttpGet]
		[Route(Name = "Features_Overview_Index_Get")]
		public ActionResult Index()
		{
			var model = new EmptyViewModel();

			return View(model);
		}
	}
}
