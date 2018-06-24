using FluentMigrator;

namespace App.Migrations
{
	[Migration(20180624081300, "Create the Users table.")]
	public class _20180624081300_Create_Users_Table : Migration
	{
		public override void Down()
		{
			Delete.Table("Users");
		}

		public override void Up()
		{
			Create
				.Table("Users")
				.WithColumn("Id").AsInt32().PrimaryKey()
				.WithColumn("IsBlocked").AsBoolean().NotNullable().WithDefaultValue(false)
				.WithColumn("UTCCreation").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime);
		}
	}
}
