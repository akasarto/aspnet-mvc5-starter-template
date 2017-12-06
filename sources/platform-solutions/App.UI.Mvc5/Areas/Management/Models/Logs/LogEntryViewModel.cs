using Sarto.Extensions;
using System;
using System.Collections.Generic;

namespace App.UI.Mvc5.Areas.Management.Models
{
	public class LogEntryViewModel
	{
		public long Id { get; set; }

		public string Level { get; set; }

		public string Message { get; set; }

		public string Exception { get; set; }

		public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();

		public DateTime TimeStamp { get; set; }

		public string Icon
		{
			get
			{
				var type = Level.ToLowerCaseString();

				if ("fatal".Equals(type))
				{
					return "fa fa-ban text-danger";
				}

				if ("error".Equals(type))
				{
					return "fa fa-times-circletext-danger";
				}

				if ("warn".Equals(type))
				{
					return "fa fa-warning text-warning";
				}

				if ("information".Equals(type))
				{
					return "fa fa-info text-info";
				}

				if ("debug".Equals(type))
				{
					return "fa fa-bug text-muted";
				}

				return "fa fa-paw text-muted";
			}
		}
	}
}
