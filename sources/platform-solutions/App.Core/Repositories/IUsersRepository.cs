using App.Core.Entities;
using System.Collections.Generic;

namespace App.Core.Repositories
{
	/// <summary>
	/// Users repository interface.
	/// </summary>
	public interface IUsersRepository
	{
		/// <summary>
		/// Create an user.
		/// </summary>
		/// <param name="userEntity">The instance with data for the new user.</param>
		/// <returns>An <see cref="UserEntity"/> instance with the new id set.</returns>
		UserEntity Create(UserEntity userEntity);

		/// <summary>
		/// Get a list of users.
		/// </summary>
		/// <returns>A collection of <see cref="UserEntity"/> instances.</returns>
		IEnumerable<UserEntity> GetAll();

		/// <summary>
		/// Get an user by email.
		/// </summary>
		/// <param name="userEmail">The email of the user to lookup.</param>
		/// <returns>A single <see cref="UserEntity"/> instance.</returns>
		UserEntity GetByEmail(string userEmail);

		/// <summary>
		/// Get an user by id.
		/// </summary>
		/// <param name="userId">The id of the user to lookup.</param>
		/// <returns>A single <see cref="UserEntity"/> instance.</returns>
		UserEntity GetById(int userId);

		/// <summary>
		/// Get an user by username.
		/// </summary>
		/// <param name="userName">The username of the user to lookup.</param>
		/// <returns>A single <see cref="UserEntity"/> instance.</returns>
		UserEntity GetByUserName(string userName);

		/// <summary>
		/// Update an user.
		/// </summary>
		/// <param name="userEntity">The instance with updated data.</param>
		void Update(UserEntity userEntity);
	}
}
