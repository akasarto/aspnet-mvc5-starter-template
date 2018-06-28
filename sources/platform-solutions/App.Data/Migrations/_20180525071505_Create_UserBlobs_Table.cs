using FluentMigrator;

namespace App.Data.Migrator
{
	[Migration(20180525071505, "Create 'UserBlobs' table.")]
	public class _20180525071505_Create_UserBlobs_Table : Migration
	{
		public override void Down()
		{
			Delete.Table("UserBlobs");
		}

		public override void Up()
		{
			Create
				.Table("UserBlobs")
				.WithColumn("UserId").AsInt32().NotNullable().PrimaryKey().ForeignKey("FK_UserBlobs.UserId_Users.Id", "Users", "Id")
				.WithColumn("BlobId").AsGuid().NotNullable().PrimaryKey().ForeignKey("FK_UserBlobs.BlobId_Blobs.Id", "Blobs", "Id");
		}
	}
}
