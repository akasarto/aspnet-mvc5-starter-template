using Humanizer;
using System.Web.Mvc;

namespace App.UI.Mvc5.Infrastructure
{
	public static partial class WebExtensions
	{
		public static string ToByteSizeLabel(this HtmlHelper @this, long length, string format = "#.#")
		{
			return length.Bytes().ToString(format);
		}
	}
}
