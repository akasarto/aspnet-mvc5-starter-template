using App.UI.Mvc5.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace App.UI.Mvc5.Areas.Features.Models
{
	public class GlobalizationViewModel : BaseViewModel
	{
		[Display(Name = "Date")]
		public DateTime? Date { get; set; }

		public MultiSelectList SampleOptionsMulti { get; set; }

		public SelectList SampleOptionsSingle { get; set; }

		[Display(Name = "Multi Select")]
		public List<string> SelectedMulti { get; set; } = new List<string>();

		[Display(Name = "Single Select")]
		public string SelectedSingle { get; set; }

		[Display(Name = "Standard Upload")]
		public HttpPostedFileBase StandadUpload { get; set; }

		[DataType(DataType.Time)]
		[Display(Name = "Time")]
		public DateTime? Time { get; set; }

		public string UploadValidate { get; set; }
	}
}
