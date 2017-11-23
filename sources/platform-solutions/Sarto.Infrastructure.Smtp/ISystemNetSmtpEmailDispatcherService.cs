using System.Net.Mail;

namespace Sarto.Infrastructure.Smtp
{
	/// <summary>
	/// Specialized interface for system.net smtp services.
	/// </summary>
	public interface ISystemNetSmtpEmailDispatcherService : IEmailDispatcherService
	{
		/// <summary>
		/// Send a message using the specified information.
		/// </summary>
		/// <param name="message">The previously build message instance.</param>
		void Send(MailMessage message);
	}
}
