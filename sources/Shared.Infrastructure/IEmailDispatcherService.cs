using System.Net.Mail;

namespace Shared.Infrastructure
{
	public interface IEmailDispatcherService
	{
		void Dispatch(string body, string subject, MailAddress toAddress, MailAddress fromAddress = null);
	}
}
