using System.Net.Mail;

namespace Sarto.Infrastructure
{
	/// <summary>
	/// Email dispatcher service interface.
	/// </summary>
	public interface IEmailDispatcherService
	{
		/// <summary>
		/// Dispatch email messages.
		/// </summary>
		/// <param name="body">The message body.</param>
		/// <param name="subject">The message subject.</param>
		/// <param name="toAddress">The targed recipient address.</param>
		/// <param name="fromAddress">The sender address.</param>
		void Dispatch(string body, string subject, MailAddress toAddress, MailAddress fromAddress = null);
	}
}
