using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Net.Mail;

namespace Shared.Infrastructure.MailGun
{
	/// <summary>
	/// MailGun email dispatcher service.
	/// </summary>
	public class MailGunApiEmailDispatcherService : IMailGunApiEmailDispatcherService
	{
		private string _apiKey = null;
		private string _domainName = null;
		private MailAddress _defaultFromAddress = null;
		private RestClient _client = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		/// <param name="apiKey">The account api key.</param>
		/// <param name="domainName">The account target domain name.</param>
		/// <param name="defaultFromAddress">The default address to be used when no sender is specified.</param>
		/// <param name="client">A custom rest client to be used.</param>
		public MailGunApiEmailDispatcherService(string apiKey, string domainName, MailAddress defaultFromAddress, RestClient client = null)
		{
			_apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey), nameof(MailGunApiEmailDispatcherService));
			_domainName = domainName ?? throw new ArgumentNullException(nameof(domainName), nameof(MailGunApiEmailDispatcherService));
			_defaultFromAddress = defaultFromAddress ?? throw new ArgumentNullException(nameof(defaultFromAddress), nameof(MailGunApiEmailDispatcherService));

			_client = client ?? new RestClient("https://api.mailgun.net/v3")
			{
				Authenticator = new HttpBasicAuthenticator("api", _apiKey)
			};
		}

		/// <summary>
		/// Dispatch email messages.
		/// </summary>
		/// <param name="body">The message body.</param>
		/// <param name="subject">The message subject.</param>
		/// <param name="toAddress">The targed recipient address.</param>
		/// <param name="fromAddress">The sender address.</param>
		public void Dispatch(string body, string subject, MailAddress toAddress, MailAddress fromAddress = null)
		{
			var restponse = SendRequest(body, subject, toAddress, fromAddress);
		}

		/// <summary>
		/// Send an email dispatch request to the MailGun api.
		/// </summary>
		/// <param name="body"></param>
		/// <param name="subject"></param>
		/// <param name="toAddress"></param>
		/// <param name="fromAddress"></param>
		/// <returns>An <see cref="IRestResponse"/> instance.</returns>
		public IRestResponse SendRequest(string body, string subject, MailAddress toAddress, MailAddress fromAddress = null)
		{
			body = body ?? throw new ArgumentNullException(nameof(body), nameof(Dispatch));
			subject = subject ?? throw new ArgumentNullException(nameof(subject), nameof(Dispatch));
			toAddress = toAddress ?? throw new ArgumentNullException(nameof(toAddress), nameof(Dispatch));
			fromAddress = fromAddress ?? _defaultFromAddress;

			var request = new RestRequest("{domain}/messages", Method.POST);

			request.AddParameter("html", body);
			request.AddParameter("subject", subject);
			request.AddParameter("to", toAddress);
			request.AddParameter("from", fromAddress);

			return SendRequest(request);
		}

		/// <summary>
		/// Send an email dispatch request to the MailGun api.
		/// </summary>
		/// <param name="request">The previously build request information to be sent.</param>
		/// <returns>An <see cref="IRestResponse"/> instance.</returns>
		public IRestResponse SendRequest(RestRequest request)
		{
			request = request ?? throw new ArgumentNullException(nameof(request), nameof(SendRequest));

			var hasDomain = request.Parameters.Exists(p => p.Name.Equals("domain"));

			if (!hasDomain)
			{
				request.AddParameter("domain", _domainName, ParameterType.UrlSegment);
			}

			return _client.Execute(request);
		}
	}
}
