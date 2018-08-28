using System;
using System.Text.RegularExpressions;

namespace Data.Tools.Migrator
{
	internal class Program
	{
		private const string DATA_EXTRACT_PATTERN = @"^\[(.*)\]\[(.*)\]$";

		private static void Main(string[] args)
		{
			if (args.Length > 0)
			{
				var input = args[0];
				var providerName = GetProviderName(input);
				var connectionString = GetConnectionString(input);

				switch (providerName)
				{
					case "System.Data.SqlClient":
						ApplySqlServerMigrations(connectionString);
						break;
				}
			}
		}

		private static void ApplySqlServerMigrations(string connectionString)
		{
			WriteDataInfo("SQL Server");

			Console.WriteLine(connectionString);

			//var service = new SqlServerMigrationService(@"Server=(localdb)\mssqllocaldb;Database=starterTemplateMVC5;Trusted_Connection=True;");

			//service.MigrateUp();
		}

		private static string GetProviderName(string connectionData) => Regex.Replace(connectionData, DATA_EXTRACT_PATTERN, "$1");

		private static string GetConnectionString(string connectionData) => Regex.Replace(connectionData, DATA_EXTRACT_PATTERN, "$2");

		private static void WriteDataInfo(string runnerType)
		{
			var fgColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine($"##### Migrating [{runnerType}]");
			Console.ForegroundColor = fgColor;
			Console.WriteLine("");
		}
	}
}
