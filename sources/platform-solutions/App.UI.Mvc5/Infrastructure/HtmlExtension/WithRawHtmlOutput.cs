using System.Web;
using System.Web.Mvc;

namespace App.UI.Mvc5.Infrastructure
{
	public static partial class WebExtensions
	{
		public static IHtmlString WithRawHtmlOutput(this IHtmlString @this)
		{
			var input = @this.ToHtmlString();
			var decodedInput = HttpUtility.HtmlDecode(input);
			return MvcHtmlString.Create(decodedInput.WithHtmlLineBreaks());
		}

		public static IHtmlString WithRawHtmlOutput(this string @this)
		{
			return MvcHtmlString.Create(@this.WithHtmlLineBreaks());
		}
	}
}