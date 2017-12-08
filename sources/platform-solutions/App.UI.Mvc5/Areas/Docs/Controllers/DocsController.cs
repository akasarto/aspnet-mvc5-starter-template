using App.UI.Mvc5.Models;
using App.UI.Mvc5.Infrastructure;
using System.Web.Mvc;

namespace App.UI.Mvc5.Areas.Docs.Controllers
{
	[RoutePrefix("docs")]
	[TrackMenuItem("docs.docs")]
	public partial class DocsController : __AreaBaseController
	{
		[Route(Name = "DocsIndexGet")]
		public ActionResult Index()
		{
			var model = new EmptyViewModel();

			return View(model);
		}
	}
}