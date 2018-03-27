using App.UI.Mvc5.Controllers;
using App.UI.Mvc5.Infrastructure;
using Serilog;
using System;
using System.Diagnostics;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace App.UI.Mvc5
{
	/// <summary>
	/// Base configuration class.
	/// </summary>
	public class MvcApplication : HttpApplication
	{
		/// <summary>
		/// Tracks if the application started successfully.
		/// </summary>
		private static bool Initialized { get; set; } = false;

		/// <summary>
		/// Called whenever a new request starts.
		/// </summary>
		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			// Create a new id for the current request.
			// This id will spread to child threads and can be used to track errors and user activity.
			Trace.CorrelationManager.ActivityId = Guid.NewGuid();
		}

		/// <summary>
		/// Called once when the application starts.
		/// </summary>
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

		/// <summary>
		/// Called each time an exception is not correctly handled by the application
		/// </summary>
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

				if (httpException != null && !string.IsNullOrWhiteSpace(httpException.GetHtmlErrorMessage()))
				{
					Response.Write(httpException.GetHtmlErrorMessage());
					Response.Write("<hr />");
				}
				Response.Write("<br />");
				Response.Write(exception.ToString().WithHtmlLineBreaks());
				Response.Write("<br />");
				Response.Write("<br />");
				Response.Write("<br />");

				Response.End();

				/*
				var data = new RouteData();

				data.Values["ex"] = exception;
				data.Values["code"] = httpStatusCode;
				data.Values["action"] = nameof(ErrorsController.Index);
				data.Values["controller"] = "Errors";

				var controller = DependencyResolver.Current.GetService<ErrorsController>() as IController;

				var requestContext = new RequestContext(new HttpContextWrapper(Context), data);

				controller.Execute(requestContext);
				*/
			}
		}
	}
}
