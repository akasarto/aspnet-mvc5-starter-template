using RestSharp;
using System.Net.Mail;

namespace Sarto.Infrastructure.MailGun
{
	/// <summary>
	/// Specialized interface for MailGun services.
	/// </summary>
	public interface IMailGunApiEmailDispatcherService : IEmailDispatcherService
	{
		/// <summary>
		/// Send an email dispatch request to the MailGun api.
		/// </summary>
		/// <param name="body"></param>
		/// <param name="subject"></param>
		/// <param name="toAddress"></param>
		/// <param name="fromAddress"></param>
		/// <returns>An <see cref="IRestResponse"/> instance.</returns>
		IRestResponse SendRequest(string body, string subject, MailAddress toAddress, MailAddress fromAddress = null);

		/// <summary>
		/// Send an email dispatch request to the MailGun api.
		/// </summary>
		/// <param name="request">The previously build request information to be sent.</param>
		/// <returns>An <see cref="IRestResponse"/> instance.</returns>
		IRestResponse SendRequest(RestRequest request);
	}
}
