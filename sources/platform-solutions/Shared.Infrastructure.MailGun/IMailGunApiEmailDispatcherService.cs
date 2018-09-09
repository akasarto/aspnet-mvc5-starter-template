using RestSharp;
using System.Net.Mail;

namespace Shared.Infrastructure.MailGun
{
	public interface IMailGunApiEmailDispatcherService : IEmailDispatcherService
	{
		IRestResponse SendRequest(string body, string subject, MailAddress toAddress, MailAddress fromAddress = null);

		IRestResponse SendRequest(RestRequest request);
	}
}
