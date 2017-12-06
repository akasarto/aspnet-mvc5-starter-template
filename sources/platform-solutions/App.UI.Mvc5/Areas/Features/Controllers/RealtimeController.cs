using App.UI.Mvc5.Models;
using App.UI.Mvc5.Infrastructure;
using System.Web.Mvc;
using System;
using Sarto.Extensions;

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

		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//[Route("create-alert", Name = "RealtimeSamplePost")]
		//public ActionResult CreateAlert()
		//{
		//	var alertModel = new AlertViewModel()
		//	{
		//		ActionLink = "#",
		//		Message = "Sample Alert",
		//		Type = Core.Entities.AlertType.Info,
		//		UserTimeZoneCreationDate = DateTime.UtcNow.ToTimeZone(User.TimeZone)
		//	};

		//	alertModel.UserIds.Add(User.Id);

		//	_realtimeService.NotifyAlertCreated(User.Id, alertModel);

		//	return Json(new { success = true });
		//}
	}
}