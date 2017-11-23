using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace App.UI.Mvc5.Infrastructure
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class MenuTrackingAgentAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var itemsKey = nameof(MenuData);

			var menuData = filterContext.HttpContext.Items[itemsKey] as MenuData;

			if (menuData == null)
			{
				menuData = new MenuData();
			}

			menuData.Items.AddRange(GetControllerItems(filterContext));
			menuData.Items.AddRange(GetActionItems(filterContext));

			filterContext.HttpContext.Items[itemsKey] = menuData;
		}

		public List<string> GetControllerItems(ActionExecutingContext filterContext)
		{
			var items = new List<string>();

			var controllerDescriptor = filterContext?.ActionDescriptor?.ControllerDescriptor;
			var attributes = controllerDescriptor?.GetCustomAttributes(typeof(TrackMenuItemAttribute), true).Cast<TrackMenuItemAttribute>().ToList();

			if (attributes != null && attributes.Count > 0)
			{
				foreach (var attribute in attributes)
				{
					items.Add(attribute.Key);
				}
			}

			return items;
		}

		public List<string> GetActionItems(ActionExecutingContext filterContext)
		{
			var items = new List<string>();

			var actionDescriptor = filterContext?.ActionDescriptor;
			var attributes = actionDescriptor?.GetCustomAttributes(typeof(TrackMenuItemAttribute), false).Cast<TrackMenuItemAttribute>().ToList();

			if (attributes != null && attributes.Count > 0)
			{
				foreach (var attribute in attributes)
				{
					items.Add(attribute.Key);
				}
			}

			return items;
		}
	}
}
