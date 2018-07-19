using FluentMigrator;

namespace Data.Core.Migrations
{
	[Migration(20180525071501, "Create 'Users' table.")]
	public class _20180525071501_Create_Users_Table : Migration
	{
		public override void Down()
		{
			Delete.Table("Users");
		}

		public override void Up()
		{
			Create
				.Table("Users")
				.WithColumn("Id").AsInt32().PrimaryKey().Identity()
				.WithColumn("IsBlocked").AsBoolean().NotNullable().WithDefaultValue(false)
				.WithColumn("UTCCreation").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
				.WithColumn("MobilePhone").AsString(30).Nullable()
				.WithColumn("LockoutEnabled").AsBoolean().NotNullable().WithDefaultValue(false)
				.WithColumn("TwoFactorEnabled").AsBoolean().NotNullable().WithDefaultValue(false)
				.WithColumn("MobilePhoneConfirmed").AsBoolean().NotNullable().WithDefaultValue(false)
				.WithColumn("LockoutEndDateUtc").AsDateTimeOffset().Nullable()
				.WithColumn("AccessFailedCount").AsByte().NotNullable().WithDefaultValue(0)
				.WithColumn("EmailConfirmed").AsBoolean().NotNullable().WithDefaultValue(false)
				.WithColumn("PasswordHash").AsString(int.MaxValue).Nullable()
				.WithColumn("SecurityStamp").AsString(int.MaxValue).Nullable()
				.WithColumn("UserName").AsString(256).NotNullable()
				.WithColumn("FullName").AsString(256).NotNullable()
				.WithColumn("Email").AsString(256).NotNullable()
				.WithColumn("CultureId").AsString(10).NotNullable()
				.WithColumn("UICultureId").AsString(10).NotNullable()
				.WithColumn("TimeZoneId").AsString(100).NotNullable().WithDefaultValue("UTC")
				.WithColumn("PictureBlobId").AsGuid().Nullable().ForeignKey("FK_Users_Blobs", "Blobs", "Id");
		}
	}
}
