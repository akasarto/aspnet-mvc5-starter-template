using Microsoft.AspNet.SignalR;

namespace App.UI.Mvc5.Infrastructure
{
	public class RealtimeService : IRealtimeService
	{
		private IHubContext _alerts = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		public RealtimeService()
		{
			_alerts = GlobalHost.ConnectionManager.GetHubContext("alerts");
		}

#warning review

		public void NotifyAlertCreated(int senderUserId, object model)
		{
			//var alertUsers = model.UserIds.Select(u => u.ToString()).ToList();

			//_alerts.Clients.Users(alertUsers).alertCreated(model);
		}

		public void NotifyAlertRead(int senderUserId, object model)
		{
			//var alertUsers = model.UserIds.Select(u => u.ToString()).ToList();

			//_alerts.Clients.Users(alertUsers).alertRead(model);
		}
	}
}