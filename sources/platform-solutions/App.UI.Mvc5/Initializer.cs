using App.Identity;
using App.UI.Mvc5.Infrastructure;
using Data.Core;
using Data.Store.SqlServer;
using Domain.Core;
using Domain.Core.Repositories;
using FluentValidation;
using FluentValidation.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Newtonsoft.Json;
using Owin;
using Serilog;
using Shared.Infrastructure;
using Shared.Infrastructure.Azure;
using Shared.Infrastructure.Cloudinary;
using Shared.Infrastructure.FileSystem;
using Shared.Infrastructure.MailGun;
using Shared.Infrastructure.Smtp;
using SimpleInjector;
using SimpleInjector.Advanced;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using SqlServer = Data.Store.SqlServer;

[assembly: OwinStartup(typeof(App.UI.Mvc5.Initializer), "Initialize")]

namespace App.UI.Mvc5
{
	public partial class Initializer
	{
		public void Initialize(IAppBuilder appBuilder)
		{
			// Set global json serialization
			JsonConvert.DefaultSettings = () => new SharedJsonSettings();

			// Build composition root
			var container = ConfigureDependencyInjectionContainer(appBuilder);

			// Configure application
			SetupApplicationRuntime(appBuilder, container);
		}

		private Container ConfigureDependencyInjectionContainer(IAppBuilder appBuilder)
		{
			var container = GetContainer();
			var domainAssemblies = GetKnownDomainAssemblies();

			//
			container.Register<AdminStore>();
			container.Register<AdminUserManager>();
			container.Register<AdminSignInManager>();
			container.Register(() => appBuilder.GetDataProtectionProvider());

			container.Register(() =>
			{
				if (AdvancedExtensions.IsVerifying(container) || HttpContext.Current == null)
				{
					return new OwinContext().Authentication;
				}

				return HttpContext.Current.GetOwinContext().Authentication;
			});

			//
			var cloudName = AppSettings.Cloudinary.CloudName;
			var cloudApiKey = AppSettings.Cloudinary.CloudApiKey;
			var cloudApiSecret = AppSettings.Cloudinary.CloudApiSecret;
			var azureBlobsConnection = AppSettings.Azure.BlobsConnection;
			var azureBlobsDefaultContainer = AppSettings.Azure.BlobsDefaultContainer;
			var fileSystemStorageFolder = AppSettings.FileSystem.StorageFolder;
			var activeStorageService = AppSettings.ActiveStorageService;

			var cloudinaryStorage = Lifestyle.Singleton.CreateRegistration<IBlobStorageService>(
				() => new CloudinaryStorageService(cloudName, cloudApiKey, cloudApiSecret), container);

			var azureBlobsStorage = Lifestyle.Singleton.CreateRegistration<IAzureStorageService>(
				() => new AzureStorageService(azureBlobsConnection, azureBlobsDefaultContainer), container);

			var fileSystemStorage = Lifestyle.Singleton.CreateRegistration<IBlobStorageService>(
				() => new FileSystemStorageService(fileSystemStorageFolder), container);

			container.RegisterConditional(typeof(IBlobStorageService), cloudinaryStorage, context => "cloudinary".Equals(activeStorageService));
			container.RegisterConditional(typeof(IBlobStorageService), azureBlobsStorage, context => "azurestorage".Equals(activeStorageService));
			container.RegisterConditional(typeof(IBlobStorageService), fileSystemStorage, context => !context.Handled); // Defaults to filesystem

			var cloudinaryThumbService = Lifestyle.Singleton.CreateRegistration<ICloudinaryThumbService>(
				() => new CloudinaryThumbService(cloudName), container);

			var imageResizerThumbService = Lifestyle.Singleton.CreateRegistration<IImageResizerThumbService>(
				() => new ImageResizerThumbService(), container);

			container.RegisterConditional(typeof(IBlobThumbService), cloudinaryThumbService, context => "cloudinary".Equals(activeStorageService));
			container.RegisterConditional(typeof(IBlobThumbService), imageResizerThumbService, context => !context.Handled); // Defaults to image resizer

			//
			var connectionString = WebConfigurationManager.ConnectionStrings["SqlServerConnection"].ConnectionString;

			container.Register<IDbConnectionFactory>(() => new SqlConnectionFactory(connectionString));

			container.Register<IBlobsRepository, SqlServer.BlobsRepository>();
			container.Register<ILogsRepository, SqlServer.LogsRepository>();

			container.Register<IIdentityRepository, IdentityRepository>();
			container.Register<IUsersRepository, SqlServer.UsersRepository>();

			//
			var mailGunApiKey = AppSettings.MailGun.ApiKey;
			var mailGunDomainName = AppSettings.MailGun.DomainName;
			var defaultFromAddress = new MailAddress(
				AppSettings.Emails.DefaultEmailFromAddress,
				AppSettings.Emails.DefaultEmailFromDisplayName
			);
			var activeEmailDispatcherService = AppSettings.ActiveEmailDispatcherService;

			var mailGunDispatcherService = container.Options.DefaultLifestyle.CreateRegistration<IMailGunApiEmailDispatcherService>(
				() => new MailGunApiEmailDispatcherService(mailGunApiKey, mailGunDomainName, defaultFromAddress), container);

			var netSmtpDispatcherService = container.Options.DefaultLifestyle.CreateRegistration<ISystemNetSmtpEmailDispatcherService>(
				() => new SystemNetSmtpEmailDispatcherService(defaultFromAddress), container);

			container.RegisterConditional(typeof(IEmailDispatcherService), mailGunDispatcherService, context => "mailgun".Equals(activeEmailDispatcherService));
			container.RegisterConditional(typeof(IEmailDispatcherService), netSmtpDispatcherService, context => !context.Handled); // Default

			//
			container.RegisterSingleton(() => new BlobServiceConfigs()
			{
				DefaultThumbBackgroundHexColor = AppSettings.Blobs.DefaultThumbBackgroundHexColor,
				DefaultThumbForegroundHexColor = AppSettings.Blobs.DefaultThumbForegroundHexColor
			});

			container.Register<IBlobService, BlobService>();
			container.Register<IGlobalizationService, GlobalizationService>();
			container.Register<IRealtimeService, RealtimeService>();

			//
			container.Register(typeof(IValidator<>), domainAssemblies);
			container.Register(() =>
			{
				if (AdvancedExtensions.IsVerifying(container) || HttpContext.Current == null)
				{
					return SharedContext.Null;
				}

				var currentPrincipal = new AdminPrincipal(HttpContext.Current.User);

				return new SharedContext(
					userId: currentPrincipal.Id
				);
			});

			//
			container.Register<IPasswordHasher, PasswordHasher>();

			//
			container.RegisterSingleton<ILogger>(() =>
			{
				var logger = new LoggerConfiguration();

				var loggerFilePath = Path.Combine(
					AppDomain.CurrentDomain.BaseDirectory,
					AppSettings.Logger.StorageFolder.Trim('~').Trim('\\', '/').Replace("/", "\\"),
					"log_.txt"
				);

				logger = logger.Enrich.With<SerilogActivityIdEnricher>();

				logger = logger.WriteTo.Async((log) => log.File(
					new SerilogTextFormatter(),
					loggerFilePath,
					rollingInterval: RollingInterval.Day,
					shared: true
				));

				logger = logger.WriteTo.Async((log) => log.MSSqlServer(
					connectionString,
					tableName: "Logs",
					autoCreateSqlTable: true
				));

				return logger.CreateLogger();
			});

			//
			//container.Register<IMigrationService>(() => new SqlServerMigrationService(connectionString));

			//
			container.Register<DatabusHub>();

			//
			container.Verify();

			return container;
		}

		private void SetupApplicationRuntime(IAppBuilder appBuilder, Container container)
		{
			//
			appBuilder.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				CookieName = AppSettings.Auth.CookieName,
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
				LoginPath = new PathString(AppSettings.Auth.LogInPath),

				Provider = new CookieAuthenticationProvider
				{
					OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<AdminUserManager, AdminUserEntity, int>(
						validateInterval: TimeSpan.FromMinutes(0),
						regenerateIdentityCallback: (manager, user) =>
						{
							var currentPricipal = HttpContext.Current.User;
							var currentAdminPrincipal = new AdminPrincipal(currentPricipal);

							var currentIsPersistentState = currentAdminPrincipal.IsPersistent;
							var currentScreenLockState = currentAdminPrincipal.ScreenLocked;

							return user.GenerateUserIdentityAsync(
								manager,
								isPersistentState: currentIsPersistentState,
								screenLockedState: currentScreenLockState
							);
						},
						getUserIdCallback: (user) =>
						{
							return user.GetUserId<int>();
						}
					)
				}
			});

			//
			var imageResizerBlobsInfraPlugin = new ImageResizerThumbPlugin(
				blobService: container.GetInstance<IBlobStorageService>()
			);

			imageResizerBlobsInfraPlugin.Install(ImageResizer.Configuration.Config.Current);

			//
			ValidatorOptions.LanguageManager = new ValidationLanguageManager();

			FluentValidationModelValidatorProvider.Configure(config =>
			{
				config.ValidatorFactory = new SimpleInjectorValidatorFactory(container);
			});

			//
			DependencyResolver.SetResolver(
				new SimpleInjectorDependencyResolver(container)
			);

			//
			var serializer = JsonSerializer.Create(new SharedJsonSettings()
			{
				ContractResolver = new SignalRCamelCaseJsonResolver()
			});

			//
			GlobalHost.DependencyResolver.Register(typeof(JsonSerializer), () => serializer);
			GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => new UserIdProvider());
			GlobalHost.DependencyResolver.Register(typeof(IHubActivator), () => new HubsActivator());

			//
			appBuilder.MapSignalR();
		}

		private static IEnumerable<Assembly> GetKnownDomainAssemblies()
		{
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();

			return assemblies.Where(assembly =>
				assembly.FullName.StartsWith("Sarto.")
			).ToList();
		}

		private static Container GetContainer()
		{
			var container = new Container();

			container.Options.DefaultLifestyle = Lifestyle.CreateHybrid(
				lifestyleSelector: () => HttpContext.Current != null,
				trueLifestyle: new WebRequestLifestyle(),
				falseLifestyle: Lifestyle.Transient
			);

			container.RegisterMvcIntegratedFilterProvider();
			container.RegisterMvcControllers();

			return container;
		}
	}
}
