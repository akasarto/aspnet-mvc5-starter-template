using App.UI.Mvc5.Models;
using App.UI.Mvc5.Infrastructure;
using System.Web.Mvc;

namespace App.UI.Mvc5.Areas.Features.Controllers
{
	[RoutePrefix("globalization")]
	[TrackMenuItem("features.globalization")]
	public partial class GlobalizationController : __AreaBaseController
	{
		/// <summary>
		/// Constructor method.
		/// </summary>
		public GlobalizationController()
		{
		}

		[Route(Name = "GlobalizationIndexGet")]
		public ActionResult Index()
		{
			var model = new EmptyViewModel();

			return View(model);
		}
	}
}