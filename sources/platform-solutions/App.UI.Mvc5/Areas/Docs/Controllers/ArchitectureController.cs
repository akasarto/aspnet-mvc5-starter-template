using App.UI.Mvc5.Infrastructure;
using App.UI.Mvc5.Models;
using System.Web.Mvc;

namespace App.UI.Mvc5.Areas.Docs.Controllers
{
	[RoutePrefix("architecture")]
	[TrackMenuItem("docs.architecture")]
	public partial class ArchitectureController : __AreaBaseController
	{
		[Route(Name = "DocsArchitectureIndexGet")]
		public ActionResult Index()
		{
			var model = new EmptyViewModel();

			return View(model);
		}
	}
}
