using App.UI.Mvc5.Infrastructure;
using App.UI.Mvc5.Models;
using System.Web.Mvc;

namespace App.UI.Mvc5.Controllers
{
	[RoutePrefix("menu")]
	public class _MenuController : __BaseController
	{
		[Route(Name = "Root_Menu_TopMenu")]
		public ActionResult TopMenu()
		{
			var model = new EmptyPartialViewModel();

			return PartialView(model);
		}
	}
}
