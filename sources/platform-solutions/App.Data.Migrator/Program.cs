using App.Data.Configs;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace App.Data.Migrator
{
	class Program
	{
		static void Main(string[] args)
		{
			//if ("up".Equals("args[0]"))
			//{

			//}

			var service = new SqlServerMigrationService("Server=(localdb)\\mssqllocaldb;Database=starterTemplateMVC5;Trusted_Connection=True;");

			service.MigrateUp();

			//var serviceProvider = CreateServices();

			//// Put the database update into a scope to ensure
			//// that all resources will be disposed.
			//using (var scope = serviceProvider.CreateScope())
			//{
			//	UpdateDatabase(scope.ServiceProvider);
			//}

			//Console.ReadLine();
		}

		/// <summary>
		/// Configure the dependency injection services
		/// </sumamry>
		private static IServiceProvider CreateServices()
		{
			return new ServiceCollection()
				// Add common FluentMigrator services
				.AddFluentMigratorCore()
				.ConfigureRunner(rb => rb
					// Add SQLite support to FluentMigrator
					.AddSqlServer()
					// Set the connection string
					.WithGlobalConnectionString("Server=(localdb)\\mssqllocaldb;Database=starterTemplateMVC5;Trusted_Connection=True;")
					// Define the assembly containing the migrations
					.ScanIn(typeof(IMigrationService).Assembly).For.Migrations())
				// Enable logging to console in the FluentMigrator way
				.AddLogging(lb => lb.AddFluentMigratorConsole())
				// Build the service provider
				.BuildServiceProvider(false);
		}

		/// <summary>
		/// Update the database
		/// </sumamry>
		private static void UpdateDatabase(IServiceProvider serviceProvider)
		{
			// Instantiate the runner
			var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

			// Execute the migrations
			runner.MigrateUp();
		}
	}
}
