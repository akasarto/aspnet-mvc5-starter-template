using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace App.UI.Mvc5.Infrastructure
{
	public static partial class WebsiteExtensions
	{
		/// <summary>
		/// Renders a checkbox without an associated hidden field to avoid conflict with theme CSS rules.
		/// </summary>
		public static MvcHtmlString CheckBoxForTheme<TModel>(this HtmlHelper<TModel> @this, Expression<Func<TModel, bool>> expression, object htmlAttributes = null)
		{
			var pattern = "(<.*input.*type=\"checkbox\".*>)(<.*input.*type=\"hidden\".*>)";

			var input = @this.CheckBoxFor(expression, htmlAttributes).ToString();

			var output = Regex.Replace(input, pattern, "$1");

			return new MvcHtmlString(output);
		}
	}
}