using Sarto.Extensions;
using System;

namespace App.UI.Mvc5.Areas.Management.Models
{
	public class LogEntryViewModel
	{
		public long Id { get; set; }

		public string Type { get; set; }

		public string Message { get; set; }

		public string Properties { get; set; }

		public DateTime UTCCreation { get; set; }

		public Guid ActivityId { get; set; }

		public string Icon
		{
			get
			{
				var type = Type.ToLowerCaseString();

				if ("fatal".Equals(type))
				{
					return "fa fa-ban fa-fw text-danger";
				}

				if ("error".Equals(type))
				{
					return "fa fa-times-circle fa-fw text-danger";
				}

				if ("warn".Equals(type))
				{
					return "fa fa-warning fa-fw text-warning";
				}

				if ("info".Equals(type))
				{
					return "fa fa-info fa-fw text-info";
				}

				if ("debug".Equals(type))
				{
					return "fa fa-bug fa-fw text-muted";
				}

				return "fa fa-paw fa-fw text-muted";
			}
		}
	}
}
