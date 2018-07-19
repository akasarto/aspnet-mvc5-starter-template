using System;
using System.IO;
using System.Net.Mail;

namespace Shared.Infrastructure.Smtp
{
	/// <summary>
	/// System.Net SMTP service.
	/// </summary>
	public class SystemNetSmtpEmailDispatcherService : ISystemNetSmtpEmailDispatcherService
	{
		private SmtpClient _smtpClient = null;
		private MailAddress _defaultFromAddress = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		/// <param name="defaultFromAddress">The default address to be used when no sender is specified.</param>
		/// <param name="smtpClient">A custom rest client to be used.</param>
		public SystemNetSmtpEmailDispatcherService(MailAddress defaultFromAddress, SmtpClient smtpClient = null)
		{
			_defaultFromAddress = defaultFromAddress ?? throw new ArgumentNullException(nameof(defaultFromAddress), nameof(SystemNetSmtpEmailDispatcherService));
			_smtpClient = smtpClient;
		}

		/// <summary>
		/// Dispatch email messages.
		/// </summary>
		/// <param name="body">The message body.</param>
		/// <param name="subject">The message subject.</param>
		/// <param name="toAddress">The targed recipient address.</param>
		/// <param name="fromAddress">The sender address.</param>
		public virtual void Dispatch(string body, string subject, MailAddress toAddress, MailAddress fromAddress = null)
		{
			body = body ?? throw new ArgumentNullException(nameof(body), nameof(Dispatch));
			subject = subject ?? throw new ArgumentNullException(nameof(subject), nameof(Dispatch));
			toAddress = toAddress ?? throw new ArgumentNullException(nameof(toAddress), nameof(Dispatch));
			fromAddress = fromAddress ?? _defaultFromAddress;

			var message = new MailMessage()
			{
				Body = body,
				IsBodyHtml = true,
				Subject = subject,
				BodyEncoding = System.Text.Encoding.UTF8,
				SubjectEncoding = System.Text.Encoding.UTF8
			};

			message.To.Add(toAddress);
			message.From = fromAddress;

			Send(message);
		}

		/// <summary>
		/// Send a message using the specified information.
		/// </summary>
		/// <param name="message">The previously build message instance.</param>
		public virtual void Send(MailMessage message)
		{
			using (var client = _smtpClient ?? new SmtpClient())
			{
				switch (client.DeliveryMethod)
				{
					case SmtpDeliveryMethod.SpecifiedPickupDirectory:
						{
							if (!Path.IsPathRooted(client.PickupDirectoryLocation))
							{
								client.PickupDirectoryLocation = Path.Combine(
									AppDomain.CurrentDomain.BaseDirectory,
									client.PickupDirectoryLocation.Trim('~').Trim('\\', '/').Replace("/", "\\")
								);

								if (!Directory.Exists(client.PickupDirectoryLocation))
								{
									Directory.CreateDirectory(client.PickupDirectoryLocation);
								}
							}
						}
						break;
				}

				client.Send(message);
			}
		}
	}
}
