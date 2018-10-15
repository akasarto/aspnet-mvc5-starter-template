using Data.Core;
using FluentMigrator;

namespace Data.Tools.Migrator
{
	[Migration(20180901150000, "Create initial database structure.")]
	public class _20180901150000_Create_Initial_Db_Structure : Migration
	{
		private readonly IMigrationScriptPathProvider _migrationScriptPathProvider;

		/// <summary>
		/// Constructor method.
		/// </summary>
		public _20180901150000_Create_Initial_Db_Structure(IMigrationScriptPathProvider migrationScriptPathProvider)
		{
			_migrationScriptPathProvider = migrationScriptPathProvider;
		}

		public override void Down()
		{
		}

		public override void Up()
		{
			var script = _migrationScriptPathProvider.GetPath("Create_Initial_Db_Structure.sql");

			IfDatabase(_Constants.SqlServer).Execute.Script(script);
		}
	}
}
