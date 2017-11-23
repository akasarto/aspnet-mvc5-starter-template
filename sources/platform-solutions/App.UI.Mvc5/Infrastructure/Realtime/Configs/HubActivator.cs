using Microsoft.AspNet.SignalR.Hubs;
using System.Web.Mvc;

namespace App.UI.Mvc5.Infrastructure
{
	public class HubsActivator : IHubActivator
	{
		public IHub Create(HubDescriptor descriptor) => DependencyResolver.Current.GetService(descriptor.HubType) as IHub;
	}
}