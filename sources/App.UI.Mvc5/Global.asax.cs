using App.UI.Mvc5.Controllers;
using App.UI.Mvc5.Infrastructure;
using Serilog;
using Shared.Extensions;
using System;
using System.Diagnostics;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace App.UI.Mvc5
{
	public class MvcApplication : HttpApplication
	{
		private static bool Initialized { get; set; } = false;

		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			// Create a new id for the current request.
			// This id will spread to child threads and can be used to track errors and user activity.
			Trace.CorrelationManager.ActivityId = Guid.NewGuid();
		}

		protected void Application_Start()
		{
			//
			AppViewEngine.MakeDefault();

			//
			GlobalFilters.Filters.Add(new MenuTrackingAgentAttribute());

			//
			AreaRegistration.RegisterAllAreas();

			//
			RouteTable.Routes.LowercaseUrls = true;
			RouteTable.Routes.AppendTrailingSlash = true;

			RouteTable.Routes.MapMvcAttributeRoutes(new DirectRouteProvider());

			RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			RouteTable.Routes.MapRoute(
				name: "Error",
				url: "error/{code}",
				defaults: new {
					controller = "Errors",
					action = nameof(ErrorsController.Index),
					code = UrlParameter.Optional
				}
			);

			RouteTable.Routes.MapRoute(
				name: "Default",
				url: "{*path}", // Catch all
				defaults: new
				{
					controller = "Errors",
					action = nameof(ErrorsController.Index),
					code = 404
				}
			);

			Initialized = true;
		}

		protected void Application_Error()
		{
			var exception = Server.GetLastError();
			var httpException = exception as HttpException;
			var loggerInstance = DependencyResolver.Current.GetService<ILogger>();

			loggerInstance?.Fatal(exception, exception.Message);

			if (Initialized)
			{
				var httpStatusCode = httpException?.GetHttpCode() ?? 500;

				if (exception is HttpAntiForgeryException)
				{
					httpStatusCode = (int)HttpStatusCode.BadRequest;
				}

				Server.ClearError();

				Response.Clear();

				var data = new RouteData();

				data.Values["ex"] = exception;
				data.Values["code"] = httpStatusCode;
				data.Values["area"] = AppAreas.GetAreaName(Area.Root);
				data.Values["action"] = nameof(ErrorsController.Index);
				data.Values["controller"] = nameof(ErrorsController).RemoveControllerSuffix();

				var controller = DependencyResolver.Current.GetService<ErrorsController>() as IController;

				var requestContext = new RequestContext(new HttpContextWrapper(Context), data);

				controller.Execute(requestContext);
			}
		}
	}
}
