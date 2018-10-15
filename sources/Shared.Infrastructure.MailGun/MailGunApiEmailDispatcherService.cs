using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Net.Mail;

namespace Shared.Infrastructure.MailGun
{
	public class MailGunApiEmailDispatcherService : IMailGunApiEmailDispatcherService
	{
		private readonly string _apiKey = null;
		private RestClient _client = null;
		private readonly MailAddress _defaultFromAddress = null;
		private readonly string _domainName = null;

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

		public void Dispatch(string body, string subject, MailAddress toAddress, MailAddress fromAddress = null)
		{
			var restponse = SendRequest(body, subject, toAddress, fromAddress);
		}

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
