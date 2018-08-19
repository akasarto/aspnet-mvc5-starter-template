//using FluentMigrator;

//namespace Data.Core.Migrations
//{
//	[Migration(20180525071506, "Create 'Logs' table.")]
//	public class _20180525071506_Create_Logs_Table : Migration
//	{
//		public override void Down()
//		{
//			Delete.Table("Logs");
//		}

//		public override void Up()
//		{
//			Create
//				.Table("Logs")
//				.WithColumn("Id").AsInt32().PrimaryKey().Identity()
//				.WithColumn("Message").AsString(int.MaxValue).Nullable()
//				.WithColumn("MessageTemplate").AsString(int.MaxValue).Nullable()
//				.WithColumn("Level").AsString(128).Nullable()
//				.WithColumn("TimeStamp").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
//				.WithColumn("Exception").AsString(int.MaxValue).Nullable()
//				.WithColumn("Properties").AsString(int.MaxValue).Nullable();
//		}
//	}
//}
