using App.UI.Mvc5.Models;
using Microsoft.AspNet.SignalR;
using System.Linq;

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

		public void NotifyAlertCreated(int senderUserId, AlertViewModel model)
		{
			var alertUsers = model.UserIds.Select(u => u.ToString()).ToList();

			_alerts.Clients.Users(alertUsers).alertCreated(model);
		}

		public void NotifyAlertRead(int senderUserId, AlertViewModel model)
		{
			var alertUsers = model.UserIds.Select(u => u.ToString()).ToList();

			_alerts.Clients.Users(alertUsers).alertRead(model);
		}
	}
}