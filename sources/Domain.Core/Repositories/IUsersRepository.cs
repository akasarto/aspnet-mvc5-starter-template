using Domain.Core.Entities;
using System.Collections.Generic;

namespace Domain.Core.Repositories
{
	public interface IUsersRepository
	{
		UserEntity Create(UserEntity userEntity);

		IEnumerable<UserEntity> GetAll();

		UserEntity GetByEmail(string userEmail);

		UserEntity GetById(int userId);

		UserEntity GetByUserName(string userName);

		void Update(UserEntity userEntity);
	}
}
