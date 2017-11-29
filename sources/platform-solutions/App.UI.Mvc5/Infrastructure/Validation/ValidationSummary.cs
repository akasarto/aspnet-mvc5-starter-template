using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace App.UI.Mvc5.Infrastructure
{
	public enum VisualizationMode : byte
	{
		/// <summary>
		/// #0 - Bootstrap alerts.
		/// </summary>
		Alert = 0
	}

	public static partial class WebsiteExtensions
	{
		/// <summary>
		/// Renders a custom validation summary for server results.
		/// </summary>
		public static MvcHtmlString ValidationSummary(this HtmlHelper @this, VisualizationMode visualizationMode)
		{
			//if (visualizationMode == ...)

			return @this.Partial("ValidationSummaryAlerts");
		}
	}
}