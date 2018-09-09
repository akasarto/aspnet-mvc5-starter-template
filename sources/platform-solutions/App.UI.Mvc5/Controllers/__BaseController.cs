using App.Identity;
using App.UI.Mvc5.Infrastructure;
using App.UI.Mvc5.Models;
using FluentValidation;
using Shared.Extensions;
using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace App.UI.Mvc5.Controllers
{
	public abstract class __BaseController : Controller
	{
		public FeedbackContext Feedback
		{
			get
			{
				string key = nameof(FeedbackContext);

				var manager = TempData.Peek(key) as FeedbackContext;

				if (manager == null)
				{
					manager = new FeedbackContext();
					TempData.Add(key, manager);
				}

				return manager;
			}
		}

		public MenuHelperControllers Menu => new MenuHelperControllers(ControllerContext);

		public new AppPrincipal User => Thread.CurrentPrincipal as AppPrincipal;

		public void ConfigureGlobalizationContext(HttpContextBase httpContext)
		{
			var cultureCookieName = AppSettings.Globalization.CultureCookieName;
			var uiCultureCookieName = AppSettings.Globalization.UICultureCookieName;
			var timeZoneCookieName = AppSettings.Globalization.TimeZoneCookieName;

			// Get current
			var cultureCookie = httpContext.GetCookie(cultureCookieName);
			var uiCultureCookie = httpContext.GetCookie(uiCultureCookieName);
			var timeZoneCookie = httpContext.GetCookie(timeZoneCookieName);

			// User -> Current -> Default
			var cultureName = User?.CultureId ?? cultureCookie?.Value ?? AppSettings.Globalization.DefaultCultureId;
			var uiCultureName = User?.UICultureId ?? uiCultureCookie?.Value ?? AppSettings.Globalization.DefaultUICultureId;
			var timeZoneId = User?.TimeZoneId ?? timeZoneCookie?.Value ?? AppSettings.Globalization.DefaultTimeZoneId;

			// Update current
			httpContext.SetCookie(cultureCookieName, cultureName);
			httpContext.SetCookie(uiCultureCookieName, uiCultureName);
			httpContext.SetCookie(timeZoneCookieName, timeZoneId);

			User.Culture = CultureInfo.GetCultureInfo(cultureName);
			User.UICulture = CultureInfo.GetCultureInfo(uiCultureName);
			User.TimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

			ValidatorOptions.LanguageManager.Enabled = true;
			ValidatorOptions.LanguageManager.Culture = User.UICulture;

			Thread.CurrentThread.CurrentCulture = User.Culture;
			Thread.CurrentThread.CurrentUICulture = User.UICulture;
		}

		[NonAction]
		public ActionResult ErrorResult(HttpStatusCode statusCode, string message = null, Exception exception = null)
		{
			var code = statusCode.ChangeType<string>();
			var isDebug = HttpContext.IsDebuggingEnabled;
			var errorViewModel = new ErrorViewModel(statusCode, errorMessage: message);

			Response.StatusCode = errorViewModel.Code;

			if (!Request.IsAjaxRequest())
			{
				return View(nameof(ErrorsController.Index), errorViewModel);
			}

			ModelState.AddModelError(code, errorViewModel.Message);

			return JsonError(ModelState);
		}

		public string GetLocalizedString(string resourceKey, params object[] formatParams)
		{
			return GlobalizationManager.GetLocalizedString(resourceKey, formatParams);
		}

		[NonAction]
		public JsonResult JsonError(ModelStateDictionary modelState, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
		{
			Response.StatusCode = (int)statusCode;

			return Json(modelState);
		}

		[NonAction]
		public string RenderPartialViewToString(string viewName, object model = null)
		{
			ViewData.Model = model;

			using (var sw = new StringWriter())
			{
				var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
				var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);

				viewResult.View.Render(viewContext, sw);
				viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);

				return sw.GetStringBuilder().ToString();
			}
		}

		[NonAction]
		public string RenderViewToString(string viewName, string masterName = null, object model = null)
		{
			ViewData.Model = model;

			using (var sw = new StringWriter())
			{
				var viewResult = ViewEngines.Engines.FindView(ControllerContext, viewName, masterName);
				var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);

				viewResult.View.Render(viewContext, sw);
				viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);

				return sw.GetStringBuilder().ToString();
			}
		}

		protected override ITempDataProvider CreateTempDataProvider()
		{
			//ToDo: [Feedback related] Implement cloud ready provider in susbtitution to the native one that use sessions.
			var provider = base.CreateTempDataProvider();

			return provider;
		}

		protected override void Initialize(System.Web.Routing.RequestContext requestContext)
		{
			base.Initialize(requestContext);

			Thread.CurrentPrincipal = new AppPrincipal(base.User);
			requestContext.HttpContext.User = Thread.CurrentPrincipal;

			ConfigureGlobalizationContext(requestContext.HttpContext);
		}

		protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding)
		{
			return new AppJsonResult()
			{
				Data = data,
				ContentType = contentType,
				ContentEncoding = contentEncoding
			};
		}

		protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
		{
			return new AppJsonResult()
			{
				Data = data,
				ContentType = contentType,
				ContentEncoding = contentEncoding,
				JsonRequestBehavior = behavior
			};
		}
	}
}
