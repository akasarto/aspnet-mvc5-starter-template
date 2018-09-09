using Data.Core;
using FluentMigrator;

namespace Data.Tools.Migrator
{
	[Migration(20180901150100, "Create initial super user account.")]
	public class _20180901150100_Create_Initial_SuperUserAccount : Migration
	{
		private readonly IMigrationScriptPathProvider _migrationScriptPathProvider;

		/// <summary>
		/// Constructor method.
		/// </summary>
		public _20180901150100_Create_Initial_SuperUserAccount(IMigrationScriptPathProvider migrationScriptPathProvider)
		{
			_migrationScriptPathProvider = migrationScriptPathProvider;
		}

		public override void Down()
		{
		}

		public override void Up()
		{
			var script = _migrationScriptPathProvider.GetPath("Create_Initial_SuperUser_Account.sql");

			IfDatabase(_Constants.SqlServer).Execute.Script(script);
		}
	}
}
