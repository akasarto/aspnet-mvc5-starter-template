using Domain.Core.Entities;

namespace Domain.Core.Repositories
{
	public interface IBlobsRepository
	{
		BlobEntity Create(BlobEntity blobEntity, int userId);
	}
}
