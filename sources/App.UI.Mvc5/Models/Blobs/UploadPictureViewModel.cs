using System.Web;

namespace App.UI.Mvc5.Models
{
	public class UploadPictureViewModel
	{
		public HttpPostedFileBase Picture { get; set; }

		public int PreviewThumbWidth { get; set; }

		public int PreviewThumbHeight { get; set; }
	}
}
