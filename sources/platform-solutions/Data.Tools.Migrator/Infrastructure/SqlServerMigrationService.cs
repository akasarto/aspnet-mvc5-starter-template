using Dapper;
using Data.Core;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data.SqlClient;

namespace Data.Tools.Migrator.Infrastructure
{
	public class SqlServerMigrationService
	{
		public static void MigrateUp(string connectionString)
		{
			var provider = CreateProvider(connectionString);

			using (var scope = provider.CreateScope())
			{
				if (!DatabaseExists(connectionString))
				{
					CreateDatabase(connectionString);
				}

				var runner = GetRunner(scope);

				runner.MigrateUp();
			}
		}

		private static void CreateDatabase(string connectionString)
		{
			var connectionInfo = GetConnectionInfo(connectionString);

			using (var connection = new SqlConnection(connectionInfo.baseConnectionString))
			{
				connection.Execute(
					$"USE [master]; CREATE DATABASE [{connectionInfo.databaseName}]"
				);
			}
		}

		private static IServiceProvider CreateProvider(string connectionString)
		{
			return
				new ServiceCollection()
				.AddSingleton<IMigrationScriptPathProvider, SqlServerScriptsPathProvider>()
				.AddFluentMigratorCore()
				.ConfigureRunner(runner => runner
					.AddSqlServer()
					.WithGlobalConnectionString(connectionString)
					.ScanIn(typeof(Program).Assembly).For.Migrations()
				)
				.AddLogging(logger => logger.AddFluentMigratorConsole())
				.BuildServiceProvider(validateScopes: false);
		}

		private static bool DatabaseExists(string connectionString)
		{
			var connectionInfo = GetConnectionInfo(connectionString);

			using (var connection = new SqlConnection(connectionInfo.baseConnectionString))
			{
				var databaseId = connection.ExecuteScalar<int>(
					"SELECT database_id FROM sys.databases WHERE Name = @databaseName",
					param: new { connectionInfo.databaseName }
				);

				return databaseId > 0;
			}
		}

		private static (string baseConnectionString, string databaseName) GetConnectionInfo(string connectionString)
		{
			var connectionBuilder = new SqlConnectionStringBuilder(connectionString);
			var initialCatalog = connectionBuilder.InitialCatalog;

			connectionBuilder.Remove("AttachDBFilename");
			connectionBuilder.Remove("Initial Catalog");
			connectionBuilder.Remove("Database");

			return (
				baseConnectionString: connectionBuilder.ConnectionString,
				databaseName: initialCatalog
			);
		}

		private static IMigrationRunner GetRunner(IServiceScope serviceScope)
		{
			var provider = serviceScope.ServiceProvider;
			return provider.GetRequiredService<IMigrationRunner>();
		}
	}
}
