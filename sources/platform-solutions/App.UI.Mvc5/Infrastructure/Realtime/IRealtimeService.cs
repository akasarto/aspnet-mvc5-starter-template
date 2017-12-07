namespace App.UI.Mvc5.Infrastructure
{
	public interface IRealtimeService
	{
		void Broadcast(int senderUserId, object payload);
	}
}