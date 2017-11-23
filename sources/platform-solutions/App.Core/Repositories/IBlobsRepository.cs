using App.Core.Entities;

namespace App.Core.Repositories
{
	/// <summary>
	/// Blobs repository interface.
	/// </summary>
	public interface IBlobsRepository
	{
		/// <summary>
		/// Create a blob.
		/// </summary>
		/// <param name="blobEntity">The instance with data for the new blob.</param>
		/// <param name="userId">The if of the user creating this blob.</param>
		/// <returns>A updated <see cref="BlobEntity"/> instance.</returns>
		BlobEntity Create(BlobEntity blobEntity, int userId);
	}
}
