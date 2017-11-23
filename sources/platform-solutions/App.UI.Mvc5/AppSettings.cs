using Sarto.Extensions;
using System.Collections.Specialized;
using System.Web.Configuration;

namespace App.UI.Mvc5
{
	public class AppSettings
	{
		private static NameValueCollection _provider => WebConfigurationManager.AppSettings;

		public static string ActiveStorageService => _provider.Get("app.activeStorageService");
		public static string ActiveEmailDispatcherService => _provider.Get("app.activeEmailDispatcherService");

		public class Auth
		{
			public static string CookieName => _provider.Get("auth.cookieName");
			public static string LogInPath => _provider.Get("auth.logInPath");
		}

		public class Azure
		{
			public static string BlobsConnection => _provider.Get("azure.blobsConnection");
			public static string BlobsDefaultContainer => _provider.Get("azure.blobsDefaultContainer");
		}

		public class Blobs
		{
			public static long FileUploadMaxLengthInBytes => _provider.Get("blobs.fileUploadMaxLengthInBytes").ChangeType<long>();
			public static string DefaultThumbBackgroundHexColor => _provider.Get("blobs.defaultThumbBackgroundHexColor");
			public static string DefaultThumbForegroundHexColor => _provider.Get("blobs.defaultThumbForegroundHexColor");
		}

		public class Cloudinary
		{
			public static string CloudName => _provider.Get("cloudinary.cloudName");
			public static string CloudApiKey => _provider.Get("cloudinary.cloudApiKey");
			public static string CloudApiSecret => _provider.Get("cloudinary.cloudApiSecret");
		}

		public class Emails
		{
			public static string DefaultEmailFromAddress => _provider.Get("emails.defaultEmailFromAddress");
			public static string DefaultEmailFromDisplayName => _provider.Get("emails.defaultEmailFromDisplayName");
		}

		public class FileSystem
		{
			public static string StorageFolder => _provider.Get("fileSystem.storageFolder");
		}

		public class Globalization
		{
			public static string DefaultCultureId => _provider.Get("globalization.defaultCultureId");
			public static string DefaultUICultureId => _provider.Get("globalization.defaultUICultureId");
			public static string DefaultTimeZoneId => _provider.Get("globalization.defaultTimeZoneId");

			public static string CultureCookieName => _provider.Get("globalization.cultureCookieName");
			public static string UICultureCookieName => _provider.Get("globalization.uiCultureCookieName");
			public static string TimeZoneCookieName => _provider.Get("globalization.timeZoneCookieName");

			public static string SupportedCultures => _provider.Get("globalization.supportedCultures");
			public static string SupportedUICultures => _provider.Get("globalization.supportedUICultures");
		}

		public class MailGun
		{
			public static string ApiKey => _provider.Get("mailGun.ApiKey");
			public static string DomainName => _provider.Get("mailGun.domainName");
		}
	}
}
