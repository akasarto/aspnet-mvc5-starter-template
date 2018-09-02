//using FluentMigrator;

//namespace Data.Core.Migrations
//{
//	[Migration(20180525071502, "Create 'UserRoles' table.")]
//	public class _20180525071502_Create_UserRoles_Table : Migration
//	{
//		public override void Down()
//		{
//			Delete.Table("UserRoles");
//		}

//		public override void Up()
//		{
//			Create
//				.Table("UserRoles")
//				.WithColumn("UserId").AsInt32().PrimaryKey().ForeignKey("FK_UserRoles.UserId_Users.Id", "Users", "Id")
//				.WithColumn("Role").AsString(128).PrimaryKey();
//		}
//	}
//}
