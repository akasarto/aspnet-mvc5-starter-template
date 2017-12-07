using App.UI.Mvc5.Models;
using App.UI.Mvc5.Infrastructure;
using System.Web.Mvc;
using System;

namespace App.UI.Mvc5.Areas.Features.Controllers
{
	[RoutePrefix("realtime")]
	[TrackMenuItem("features.realtime")]
	public partial class RealtimeController : __AreaBaseController
	{
		private IRealtimeService _realtimeService = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		public RealtimeController(IRealtimeService realtimeService)
		{
			_realtimeService = realtimeService ?? throw new ArgumentNullException(nameof(realtimeService), nameof(RealtimeController));
		}

		[Route(Name = "RealtimeIndexGet")]
		public ActionResult Index()
		{
			var model = new EmptyViewModel();

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route("broadcast-data", Name = "RealtimeBroadcastDataPost")]
		public ActionResult BroadcastData(string payload)
		{
			if (string.IsNullOrWhiteSpace(payload))
			{
				return Json(new { success = false });
			}

			_realtimeService.Broadcast(User.Id, payload);

			return Json(new { success = true });
		}
	}
}