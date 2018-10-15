using App.UI.Mvc5.Models;
using System.Collections.Generic;

namespace App.UI.Mvc5.Areas.Management.Models
{
	public class LogsIndexViewModel : BaseViewModel
	{
		public List<LogEntryViewModel> Entries { get; set; } = new List<LogEntryViewModel>();
	}
}