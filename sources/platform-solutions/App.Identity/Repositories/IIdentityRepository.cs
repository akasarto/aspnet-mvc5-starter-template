namespace App.Identity
{
	public interface IIdentityRepository
	{
		AdminUserEntity Create(AdminUserEntity adminUser);

		AdminUserEntity GetById(int userId);

		AdminUserEntity GetByEmail(string userEmail);

		AdminUserEntity GetByUserName(string userName);

		void UpdateIdentity(AdminUserEntity adminUser);

		void UpdateProfile(AdminUserEntity adminUser);
	}
}
