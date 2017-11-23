using System.Web.Mvc;

namespace App.UI.Mvc5.Infrastructure
{
	public enum Layouts
	{
		Base,
		Emails,
		InternalBase,
		InternalDefault,
		InternalSidebarPanel,
		InternalTabNavigation,
		Null
	}

	public static partial class WebsiteExtensions
	{
		private static string GetPath(Layouts layout)
		{
			string result = string.Empty;

			switch (layout)
			{
				case Layouts.Base:
					result = "~/Views/_Layout.cshtml";
					break;

				case Layouts.Emails:
					result = "~/Views/_LayoutEmails.cshtml";
					break;

				case Layouts.InternalBase:
					result = "~/Views/_LayoutInternalBase.cshtml";
					break;

				case Layouts.InternalDefault:
					result = "~/Views/_LayoutInternalDefault.cshtml";
					break;

				case Layouts.InternalSidebarPanel:
					result = "~/Views/_LayoutInternalSidebarPanel.cshtml";
					break;

				case Layouts.InternalTabNavigation:
					result = "~/Views/_LayoutInternalTabs.cshtml";
					break;

				default:
					result = null;
					break;
			}

			return result;
		}

		public static void SetLayout(this ViewStartPage @this, Layouts layout)
		{
			@this.Layout = GetPath(layout);
		}

		public static void SetLayout(this ViewStartPage @this, string layoutPath)
		{
			@this.Layout = layoutPath;
		}

		public static void SetLayout(this WebViewPage @this, Layouts layout)
		{
			@this.Layout = GetPath(layout);
		}

		public static void SetLayout(this WebViewPage @this, string layoutPath)
		{
			@this.Layout = layoutPath;
		}
	}
}