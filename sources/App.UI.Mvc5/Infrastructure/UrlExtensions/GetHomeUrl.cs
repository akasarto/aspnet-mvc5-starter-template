using System.Web.Mvc;

namespace App.UI.Mvc5.Infrastructure
{
	public static partial class BlobExtensions
	{
		public static string GetHomeUrl(this UrlHelper @this)
		{
			return @this.Action("Index", "Home", new { area = AppAreas.GetAreaName(Area.Root) });
		}
	}
}
