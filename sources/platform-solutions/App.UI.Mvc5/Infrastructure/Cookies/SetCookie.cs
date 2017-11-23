using System;
using System.Web;

namespace App.UI.Mvc5.Infrastructure
{
	public static partial class WebExtensions
	{
		public static void SetCookie(this HttpContextBase @this, string cookieName, string cookieValue, DateTime? expirationDate = null)
		{
			var requestCookies = @this.Request.Cookies;
			var responseCookies = @this.Response.Cookies;

			if (expirationDate == null)
			{
				expirationDate = DateTime.UtcNow.AddYears(1);
			}

			var httpCookie = @this.GetCookie(cookieName);

			if (httpCookie == null)
			{
				httpCookie = new HttpCookie(cookieName);
			}

			httpCookie.Value = cookieValue;
			httpCookie.Expires = expirationDate.Value;

			responseCookies.Add(httpCookie);
		}
	}
}
