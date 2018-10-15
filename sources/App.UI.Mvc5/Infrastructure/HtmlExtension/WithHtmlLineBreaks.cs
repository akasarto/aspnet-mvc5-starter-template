using System;

namespace App.UI.Mvc5.Infrastructure
{
	public static partial class WebExtensions
	{
		public static string WithHtmlLineBreaks(this string @this)
		{
			if (string.IsNullOrWhiteSpace(@this))
			{
				return string.Empty;
			}

			return @this.Replace("\n", "<br />").Replace(Environment.NewLine, "<br />");
		}
	}
}