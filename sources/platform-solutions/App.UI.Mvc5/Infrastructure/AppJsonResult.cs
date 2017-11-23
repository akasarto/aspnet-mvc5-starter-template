using Newtonsoft.Json;
using System;
using System.Web.Mvc;

namespace App.UI.Mvc5.Infrastructure
{
	public class AppJsonResult : JsonResult
	{
		public override void ExecuteResult(ControllerContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException(nameof(context));
			}

			var response = context.HttpContext.Response;

			response.ContentType = string.IsNullOrWhiteSpace(ContentType) ? "application/json" : ContentType;

			if (ContentEncoding != null)
			{
				response.ContentEncoding = ContentEncoding;
			}

			string json = JsonConvert.SerializeObject(Data);

			response.Write(json);
		}
	}
}