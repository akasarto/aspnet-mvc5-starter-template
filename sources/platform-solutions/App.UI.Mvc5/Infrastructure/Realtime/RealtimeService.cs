using Microsoft.AspNet.SignalR;

namespace App.UI.Mvc5.Infrastructure
{
	public class RealtimeService : IRealtimeService
	{
		private IHubContext _databus = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		public RealtimeService()
		{
			_databus = GlobalHost.ConnectionManager.GetHubContext("databus");
		}

		public void Broadcast(int senderUserId, object payload)
		{
			_databus.Clients.All.dataReceived(payload);
		}
	}
}
