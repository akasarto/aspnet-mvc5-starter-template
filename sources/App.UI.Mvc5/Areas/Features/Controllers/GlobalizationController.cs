using App.UI.Mvc5.Areas.Features.Models;
using App.UI.Mvc5.Infrastructure;
using Omu.ValueInjecter;
using System.Web.Mvc;

namespace App.UI.Mvc5.Areas.Features.Controllers
{
	[RoutePrefix("globalization")]
	[TrackMenuItem("features.globalization")]
	public partial class GlobalizationController : __AreaBaseController
	{
		[HttpGet]
		[Route(Name = "Features_Globalization_Index_Post_Get")]
		public ActionResult Index()
		{
			var model = BuildGlobalizationViewModel();

			return View(model);
		}

		private GlobalizationViewModel BuildGlobalizationViewModel(GlobalizationViewModel postedModel = null)
		{
			var model = new GlobalizationViewModel();

			if (postedModel != null)
			{
				// Use a mapper to associate the previously posted information to the new model.
				model.InjectFrom(postedModel);
			}

			return model;
		}
	}
}
