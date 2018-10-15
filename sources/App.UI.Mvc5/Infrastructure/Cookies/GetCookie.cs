using System.Web;

namespace App.UI.Mvc5.Infrastructure
{
	public static partial class WebExtensions
	{
		public static HttpCookie GetCookie(this HttpContextBase @this, string cookieName)
		{
			var requestCookies = @this.Request.Cookies;

			return requestCookies.Get(cookieName);
		}
	}
}
