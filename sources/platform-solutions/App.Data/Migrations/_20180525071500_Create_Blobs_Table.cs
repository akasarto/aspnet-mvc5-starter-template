using FluentMigrator;

namespace App.Data.Migrator
{
	[Migration(20180525071500, "Create 'Blobs' table.")]
	public class _20180525071500_Create_Blobs_Table : Migration
	{
		public override void Down()
		{
			Delete.Table("Blobs");
		}

		public override void Up()
		{
			Create
				.Table("Blobs")
				.WithColumn("Id").AsGuid().PrimaryKey()
				.WithColumn("Folder").AsString(10).NotNullable()
				.WithColumn("Extension").AsString(10).NotNullable()
				.WithColumn("ContentType").AsString(100).NotNullable()
				.WithColumn("ContentLength").AsInt64().NotNullable()
				.WithColumn("SourceFileName").AsString(1000).NotNullable()
				.WithColumn("UTCCreated").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
				.WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false)
				.WithColumn("IsPurged").AsBoolean().NotNullable().WithDefaultValue(false);
		}
	}
}
