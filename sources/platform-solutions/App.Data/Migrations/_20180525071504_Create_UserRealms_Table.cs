using FluentMigrator;

namespace App.Data.Migrator
{
	[Migration(20180525071504, "Create 'UserRealms' table.")]
	public class _20180525071504_Create_UserRealms_Table : Migration
	{
		public override void Down()
		{
			Delete.Table("UserRealms");
		}

		public override void Up()
		{
			Create
				.Table("UserRealms")
				.WithColumn("UserId").AsInt32().NotNullable().PrimaryKey().ForeignKey("FK_UserRealms.UserId_Users.Id", "Users", "Id")
				.WithColumn("Realm").AsInt32().NotNullable().PrimaryKey();
		}
	}
}
