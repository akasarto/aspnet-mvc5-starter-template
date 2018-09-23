using App.UI.Mvc5.Models;
using System.Web.Mvc;

namespace App.UI.Mvc5.Areas.Features.Controllers
{
	[RoutePrefix("menu")]
	public class _MenuController : __AreaBaseController
	{
		[Route("top-menu-item", Name = "FeaturesTopMenuItem")]
		public ActionResult TopMenuItem()
		{
			var model = new EmptyPartialViewModel();

			return PartialView(model);
		}
	}
}
