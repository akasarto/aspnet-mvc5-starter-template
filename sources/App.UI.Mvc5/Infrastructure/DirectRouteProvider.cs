using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Routing;

namespace App.UI.Mvc5.Infrastructure
{
	/// <summary>
	/// Allow attribute route to be inherited from base classes.
	/// </summary>
	public class DirectRouteProvider : DefaultDirectRouteProvider
	{
		protected override IReadOnlyList<IDirectRouteFactory> GetActionRouteFactories(ActionDescriptor actionDescriptor)
		{
			return actionDescriptor.GetCustomAttributes(typeof(IDirectRouteFactory), inherit: true).Cast<IDirectRouteFactory>().ToArray();
		}
	}
}
