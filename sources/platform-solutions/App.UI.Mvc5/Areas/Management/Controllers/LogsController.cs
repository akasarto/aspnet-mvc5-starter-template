using App.Core.Entities;
using App.Core.Repositories;
using App.UI.Mvc5.Areas.Management.Models;
using App.UI.Mvc5.Infrastructure;
using Omu.ValueInjecter;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Xml;

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

				if (!string.IsNullOrWhiteSpace(entity.Properties))
				{
					var xDoc = new XmlDocument();

					xDoc.LoadXml(entity.Properties);

					var nodes = xDoc.FirstChild.SelectNodes("property");

					foreach (XmlNode node in nodes)
					{
						model.Properties.Add(
							node.Attributes["key"].Value,
							node.InnerText
						);
					}
				}
			}

			return model;
		}
	}
}
