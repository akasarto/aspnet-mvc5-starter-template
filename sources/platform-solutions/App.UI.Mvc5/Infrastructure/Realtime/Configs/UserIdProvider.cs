using App.Identity;
using Microsoft.AspNet.SignalR;

namespace App.UI.Mvc5.Infrastructure
{
	public class UserIdProvider : IUserIdProvider
	{
		public string GetUserId(IRequest request)
		{
			var user = new AppPrincipal(request?.User);

			return (user?.Id ?? 0).ToString();
		}
	}
}
