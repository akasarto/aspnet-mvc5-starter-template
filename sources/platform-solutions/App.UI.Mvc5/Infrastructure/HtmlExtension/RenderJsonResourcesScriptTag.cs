using Newtonsoft.Json;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.UI.Mvc5.Infrastructure
{
	public static partial class WebsiteExtensions
	{
		public static IHtmlString RenderAdminScriptResourcesTag(this HtmlHelper @this, CultureInfo culture, CultureInfo uiCulture)
		{
			var virtualSrc = string.Empty;
			var scriptTagBuilder = new TagBuilder("script");

			var resources = AdminScriptResources.ResourceManager.GetResourceSet(
				uiCulture,
				createIfNotExists: true,
				tryParents: true
			).Cast<DictionaryEntry>().ToDictionary(
				x => x.Key.ToString(),
				x => x.Value.ToString()
			);

			scriptTagBuilder.MergeAttribute("type", "text/javascript");

			var data = JsonConvert.SerializeObject(resources);

			scriptTagBuilder.InnerHtml = $"var resources = {data}";

			return MvcHtmlString.Create(scriptTagBuilder.ToString());
		}
	}
}
