namespace App.UI.Mvc5.Infrastructure
{
	public interface IRealtimeService
	{
		void NotifyAlertCreated(int senderUserId, object model);

		void NotifyAlertRead(int senderUserId, object model);
	}
}