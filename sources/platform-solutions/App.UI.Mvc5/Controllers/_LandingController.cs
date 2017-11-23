using App.UI.Mvc5.Infrastructure;
using System.Web.Mvc;

namespace App.UI.Mvc5.Controllers
{
	[RoutePrefix("")]
	public partial class _LandingController : __BaseController
	{
		[HttpGet]
		[TrackMenuItem("root.landing")]
		[Route("", Name = "LandingIndexGet")]
		public ActionResult Index()
		{
			if (HttpContext.Request.IsAuthenticated)
			{
				return RedirectToAction("Index", "_Landing", new { area = AppAreas.GetAreaName(Area.Dash) });
			}

			return RedirectToAction("LogIn", "Account", new { area = AppAreas.GetAreaName(Area.Users) });
		}
	}
}
