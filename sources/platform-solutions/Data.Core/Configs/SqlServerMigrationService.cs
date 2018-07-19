using Dapper;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data.SqlClient;

namespace Data.Core.Configs
{
	public class SqlServerMigrationService : IMigrationService
	{
		private readonly string _connectionString = null;

		/// <summary>
		/// Contructor method.
		/// </summary>
		public SqlServerMigrationService(string connectionString)
		{
			_connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString), nameof(SqlServerMigrationService));
		}

		public void MigrateUp()
		{
			var provider = CreateProvider();

			using (var scope = provider.CreateScope())
			{
				if (!DatabaseExists())
				{
					CreateDatabase();
				}

				UpdateDatabase(scope.ServiceProvider);
			}
		}

		private IServiceProvider CreateProvider()
		{
			return new ServiceCollection()
				.AddFluentMigratorCore()
				.ConfigureRunner(rb => rb
					.AddSqlServer()
					.WithGlobalConnectionString(_connectionString)
					.ScanIn(typeof(SqlServerMigrationService).Assembly).For.Migrations())
				.AddLogging(lb => lb.AddFluentMigratorConsole())
				.BuildServiceProvider(false);
		}

		private bool DatabaseExists()
		{
			var connectionInfo = GetConnectionInfo();

			using (var connection = new SqlConnection(connectionInfo.baseConnectionString))
			{
				var databaseId = connection.ExecuteScalar<int>(
					"SELECT database_id FROM sys.databases WHERE Name = @databaseName",
					param: new { connectionInfo.databaseName }
				);

				return databaseId > 0;
			}
		}

		private void CreateDatabase()
		{
			var connectionInfo = GetConnectionInfo();

			using (var connection = new SqlConnection(connectionInfo.baseConnectionString))
			{
				connection.Execute(
					$"USE [master]; CREATE DATABASE [{connectionInfo.databaseName}]"
				);
			}
		}

		private (string baseConnectionString, string databaseName) GetConnectionInfo()
		{
			var connectionBuilder = new SqlConnectionStringBuilder(_connectionString);
			var initialCatalog = connectionBuilder.InitialCatalog;

			connectionBuilder.Remove("AttachDBFilename");
			connectionBuilder.Remove("Initial Catalog");
			connectionBuilder.Remove("Database");

			return (
				baseConnectionString: connectionBuilder.ConnectionString,
				databaseName: initialCatalog
			);
		}

		private static void UpdateDatabase(IServiceProvider serviceProvider)
		{
			var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

			runner.MigrateUp();
		}
	}
}
