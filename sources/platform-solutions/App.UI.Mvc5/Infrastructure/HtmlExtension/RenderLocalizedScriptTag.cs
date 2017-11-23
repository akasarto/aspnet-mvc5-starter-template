using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace App.UI.Mvc5.Infrastructure
{
	public static partial class WebsiteExtensions
	{
		public static IHtmlString RenderLocalizedScriptTag(this HtmlHelper @this, string virtualSrcTemplate, CultureInfo culture)
		{
			var virtualSrc = string.Empty;
			var scriptTagBuilder = new TagBuilder("script");

			var virtualCulturePath = string.Format(virtualSrcTemplate, culture.Name);
			var virtualCultureISOPath = string.Format(virtualSrcTemplate, culture.TwoLetterISOLanguageName);

			var cultureFile = new FileInfo(HostingEnvironment.MapPath(virtualCulturePath));
			var cultureISOFile = new FileInfo(HostingEnvironment.MapPath(virtualCultureISOPath));

			virtualSrc = cultureFile.Exists ? virtualCulturePath : cultureISOFile.Exists ? virtualCultureISOPath : string.Empty;

			if (string.IsNullOrWhiteSpace(virtualSrc))
			{
				return MvcHtmlString.Empty;
			}

			scriptTagBuilder.MergeAttribute("src", VirtualPathUtility.ToAbsolute(virtualSrc));

			return MvcHtmlString.Create(scriptTagBuilder.ToString());
		}
	}
}
