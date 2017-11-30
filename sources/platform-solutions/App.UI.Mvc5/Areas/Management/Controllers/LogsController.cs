using App.UI.Mvc5.Areas.Management.Models;
using Omu.ValueInjecter;
using App.UI.Mvc5.Infrastructure;
using System;
using System.Linq;
using System.Web.Mvc;
using App.Core.Repositories;
using App.Core.Entities;

namespace App.UI.Mvc5.Areas.Management.Controllers
{
	[RoutePrefix("logs")]
	[TrackMenuItem("management.logs")]
	public partial class LogsController : __AreaBaseController
	{
		private ILogsRepository _logsRepository = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		public LogsController(ILogsRepository logsRepository)
		{
			_logsRepository = logsRepository ?? throw new ArgumentNullException(nameof(logsRepository), nameof(LogsController));
		}

		[Route(Name = "ManagementLogsIndexGet")]
		public ActionResult Index()
		{
			var model = new LogsIndexViewModel();

			model.Entries = _logsRepository.GetAll().Select(entry => BuildLogEntryViewModel(entry)).ToList();

			return View(model);
		}

		private LogEntryViewModel BuildLogEntryViewModel(LogEntity entity)
		{
			var model = new LogEntryViewModel();

			if (entity != null)
			{
				model.InjectFrom(entity);
			}

			return model;
		}
	}
}
