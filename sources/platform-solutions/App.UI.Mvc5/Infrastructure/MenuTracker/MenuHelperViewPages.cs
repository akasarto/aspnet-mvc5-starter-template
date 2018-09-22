using Shared.Extensions;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace App.UI.Mvc5.Infrastructure
{
	public class MenuHelperViewPages
	{
		private HtmlHelper _htmlHelper = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		public MenuHelperViewPages(HtmlHelper htmlHelper)
		{
			_htmlHelper = htmlHelper;
		}

		public T IfActiveItem<T>(string key, T thenTrueResult, T elseFalseResult = default(T))
		{
			var menuData = _htmlHelper.ViewContext.HttpContext.Items[nameof(MenuData)] as MenuData;

			if (menuData == null)
			{
				menuData = new MenuData();
			}

			var result = menuData.Items.Any(i => Regex.IsMatch(i, key, RegexOptions.IgnoreCase)) ? thenTrueResult : elseFalseResult;

			return result;
		}
	}
}
