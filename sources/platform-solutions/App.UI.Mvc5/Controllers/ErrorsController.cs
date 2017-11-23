using System;
using System.Net;
using System.Web.Mvc;

namespace App.UI.Mvc5.Controllers
{
	[RoutePrefix("")]
	public partial class ErrorsController : BaseController
	{
		/// <summary>
		/// Constructor method.
		/// </summary>
		public ErrorsController()
		{
		}

		public ActionResult Index(int? code)
		{
			// Check code
			code = code ?? 500;

			// Get unhandled exception
			// Can be set from controllers or in global.asax
			var exception = RouteData.Values["ex"] as Exception;

			// Return result from base controller
			return ErrorResult((HttpStatusCode)code, exception: exception);
		}
	}
}
