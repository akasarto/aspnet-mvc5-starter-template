using App.UI.Mvc5.Models;

namespace App.UI.Mvc5.Infrastructure
{
	public interface IRealtimeService
	{
		void NotifyAlertCreated(int senderUserId, AlertViewModel model);

		void NotifyAlertRead(int senderUserId, AlertViewModel model);
	}
}