using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace App.UI.Mvc5.Infrastructure
{
	[HubName("alerts")]
	public class AlertsHub : Hub
	{
		public void Hello()
		{
			Clients.All.hello();
		}

		public string Test()
		{
			return "Urrp!";
		}
	}
}