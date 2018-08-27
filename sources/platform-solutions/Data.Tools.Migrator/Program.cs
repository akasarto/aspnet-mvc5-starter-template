using System;

namespace Data.Tools.Migrator
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length > 0)
			{
				var connectionString = args[0];
				ApplySqlServerMigrations(connectionString);
			}
		}

		private static void ApplySqlServerMigrations(string connectionString)
		{
			var fgColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("SQL Server migrations:");
			Console.ForegroundColor = fgColor;
			Console.WriteLine("");

			Console.WriteLine(connectionString);

			//var service = new SqlServerMigrationService(@"Server=(localdb)\mssqllocaldb;Database=starterTemplateMVC5;Trusted_Connection=True;");

			//service.MigrateUp();
		}
	}
}
