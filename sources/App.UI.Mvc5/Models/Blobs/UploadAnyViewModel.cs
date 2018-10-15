using System.Web;

namespace App.UI.Mvc5.Models
{
	public class UploadAnyViewModel
	{
		public HttpPostedFileBase Blob { get; set; }

		public int PreviewThumbWidth { get; set; }

		public int PreviewThumbHeight { get; set; }
	}
}
