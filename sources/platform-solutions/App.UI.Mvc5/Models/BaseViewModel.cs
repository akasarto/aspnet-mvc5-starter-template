using System.Text;

namespace App.UI.Mvc5.Models
{
	public abstract class BaseViewModel
	{
		public string PageTitle { get; set; } = string.Empty;

		public LayoutOptions LayoutOptions { get; set; }

		public string GetHtmlClasses()
		{
			var result = new StringBuilder();

			return result.ToString();
		}
	}
}
