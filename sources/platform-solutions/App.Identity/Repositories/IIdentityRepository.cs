namespace App.Identity.Repositories
{
	public interface IIdentityRepository
	{
		AppUserEntity Create(AppUserEntity adminUser);

		AppUserEntity GetById(int userId);

		AppUserEntity GetByEmail(string userEmail);

		AppUserEntity GetByUserName(string userName);

		void UpdateIdentity(AppUserEntity adminUser);

		void UpdateProfile(AppUserEntity adminUser);
	}
}
