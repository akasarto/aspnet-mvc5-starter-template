using Domain.Core.Entities;
using Domain.Core.Repositories;
using Dapper;
using System;
using System.Data;

namespace Data.Core
{
	/// <summary>
	/// Blobs repository implementation.
	/// </summary>
	public partial class BlobsRepository : IBlobsRepository
	{
		internal readonly IDbConnectionFactory _dbConnectionFactory = null;

		/// <summary>
		/// Contructor method.
		/// </summary>
		/// <param name="dbConnectionFactory">The current connection factory instance.</param>
		public BlobsRepository(IDbConnectionFactory dbConnectionFactory)
		{
			_dbConnectionFactory = dbConnectionFactory ?? throw new ArgumentNullException(nameof(dbConnectionFactory), nameof(BlobsRepository));
		}

		/// <summary>
		/// Create a blob.
		/// </summary>
		/// <param name="blobEntity">The instance with data for the new blob.</param>
		/// <param name="userId">The if of the user creating this blob.</param>
		/// <returns>A updated <see cref="BlobEntity"/> instance.</returns>
		public BlobEntity Create(BlobEntity blobEntity, int userId)
		{
			using (var connection = _dbConnectionFactory.CreateConnection())
			{
				connection.Open();

				using (var transaction = connection.BeginTransaction())
				{
					var parameters = new DynamicParameters();

					parameters.Add("@Id", blobEntity.Id, DbType.Guid);
					parameters.Add("@Folder", blobEntity.Folder, DbType.String);
					parameters.Add("@Extension", blobEntity.Extension, DbType.String);
					parameters.Add("@ContentType", blobEntity.ContentType, DbType.String);
					parameters.Add("@ContentLength", blobEntity.ContentLength, DbType.Int64);
					parameters.Add("@SourceFileName", blobEntity.SourceFileName, DbType.String);
					parameters.Add("@UTCCreated", blobEntity.UTCCreated, DbType.DateTime2);

					parameters.Add("@UserId", userId, DbType.Int32);

					int result = connection.Execute(
						sql: "BlobsInsert",
						commandType: CommandType.StoredProcedure,
						transaction: transaction,
						param: parameters
					);

					transaction.Commit();

					return blobEntity;
				}
			}
		}
	}
}
