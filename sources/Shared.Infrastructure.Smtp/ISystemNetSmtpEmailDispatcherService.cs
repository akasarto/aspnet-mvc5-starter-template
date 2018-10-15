using System.Net.Mail;

namespace Shared.Infrastructure.Smtp
{
	public interface ISystemNetSmtpEmailDispatcherService : IEmailDispatcherService
	{
		void Send(MailMessage message);
	}
}
