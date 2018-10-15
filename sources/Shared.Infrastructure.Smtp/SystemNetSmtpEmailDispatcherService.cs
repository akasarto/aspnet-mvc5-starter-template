using System;
using System.IO;
using System.Net.Mail;

namespace Shared.Infrastructure.Smtp
{
	public class SystemNetSmtpEmailDispatcherService : ISystemNetSmtpEmailDispatcherService
	{
		private readonly MailAddress _defaultFromAddress = null;
		private readonly SmtpClient _smtpClient = null;

		public SystemNetSmtpEmailDispatcherService(MailAddress defaultFromAddress, SmtpClient smtpClient = null)
		{
			_defaultFromAddress = defaultFromAddress ?? throw new ArgumentNullException(nameof(defaultFromAddress), nameof(SystemNetSmtpEmailDispatcherService));
			_smtpClient = smtpClient;
		}

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
