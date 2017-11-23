using System.Web.Mvc;

namespace App.UI.Mvc5.Infrastructure
{
	public class AppViewEngine : RazorViewEngine
	{
		public AppViewEngine()
		{
			// I.e.: "~/Areas/{area_name}/Views/{controller_name}/{view_name}.cshtml"
			//       "~/Areas/{area_name}/Views/{view_name}.cshtml"
			var areasViewSeachPaths = new string[] {
				"~/Areas/{2}/Views/{1}/{0}.cshtml",
				"~/Areas/{2}/Views/{0}.cshtml",
			};

			// I.e.: "~/Views/{controller_name}/{view_name}.cshtml"
			//       "~/Views/{view_name}.cshtml"
			var viewSeachPaths = new string[] {
				"~/Views/{1}/{0}.cshtml",
				"~/Views/{0}.cshtml"
			};

			AreaViewLocationFormats = areasViewSeachPaths;
			AreaPartialViewLocationFormats = areasViewSeachPaths;

			ViewLocationFormats = viewSeachPaths;
			PartialViewLocationFormats = viewSeachPaths;
		}

		public static void MakeDefault()
		{
			ViewEngines.Engines.Clear();
			ViewEngines.Engines.Add(new AppViewEngine());
		}
	}
}
