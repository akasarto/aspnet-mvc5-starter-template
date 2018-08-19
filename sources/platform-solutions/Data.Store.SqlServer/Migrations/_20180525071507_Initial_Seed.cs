//using Domain.Core;
//using FluentMigrator;

//namespace Data.Core.Migrations
//{
//	[Migration(20180525071507, "Initial data seed.")]
//	public class _20180525071507_Initial_Seed : Migration
//	{
//		public override void Down()
//		{
//		}

//		public override void Up()
//		{
//			Execute.Sql($@"
//				DECLARE @UserId INT = 0

//				INSERT INTO [Users]
//				(
//					[EmailConfirmed],
//					[LockoutEnabled],
//					[PasswordHash],
//					[SecurityStamp],
//					[UserName],
//					[Email],
//					[FullName],
//					[CultureId],
//					[UICultureId]
//				)
//					VALUES
//				(
//					1,
//					0,
//					'AINCgbCLUSYjSnDrVGtCJCNphNWOAtcGScdSIOXYpGoezJoyFBEuZ7cTq3jpdVCEwg==', /*password*/
//					'79df49ad-983c-4680-8cd7-15c2058fd8e3',
//					'admin',
//					'admin@yourdomain.com',
//					'Administrator [Super User]',
//					'en-US',
//					'en-US'
//				)

//				SET @UserId = SCOPE_IDENTITY()

//				INSERT INTO UserRealms ([UserId], [Realm]) Values (@UserId, {(int)Realm.AdminWebsite})

//				INSERT INTO UserRoles ([UserId], [Role]) Values (@UserId, '{Role.SuperUser.ToString()}')
//			");
//		}
//	}
//}
