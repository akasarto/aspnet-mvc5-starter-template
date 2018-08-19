//using FluentMigrator;

//namespace Data.Core.Migrations
//{
//	[Migration(20180525071503, "Create 'UserClaims' table.")]
//	public class _20180525071503_Create_UserClaims_Table : Migration
//	{
//		public override void Down()
//		{
//			Delete.Table("UserClaims");
//		}

//		public override void Up()
//		{
//			Create
//				.Table("UserClaims")
//				.WithColumn("Id").AsInt32().PrimaryKey().Identity()
//				.WithColumn("UserId").AsInt32().NotNullable().ForeignKey("FK_UserClaims.UserId_Users.Id", "Users", "Id")
//				.WithColumn("Type").AsString(int.MaxValue).NotNullable()
//				.WithColumn("Value").AsString(int.MaxValue).NotNullable();
//		}
//	}
//}
