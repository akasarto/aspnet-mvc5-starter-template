using System;
using System.Net;
using System.Web.Mvc;

namespace App.UI.Mvc5.Controllers
{
	[RoutePrefix("error")]
	public partial class ErrorsController : __BaseController
	{
		[Route(Name = "Root_Errors_Index")]
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
