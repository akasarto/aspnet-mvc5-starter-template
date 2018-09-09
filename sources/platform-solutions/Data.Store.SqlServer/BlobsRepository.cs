using Dapper;
using Data.Core;
using Domain.Core.Entities;
using Domain.Core.Repositories;
using System;
using System.Data;

namespace Data.Store.SqlServer
{
	public partial class BlobsRepository : IBlobsRepository
	{
		internal readonly IDbConnectionFactory _dbConnectionFactory = null;

		/// <summary>
		/// Contructor method.
		/// </summary>
		public BlobsRepository(IDbConnectionFactory dbConnectionFactory)
		{
			_dbConnectionFactory = dbConnectionFactory ?? throw new ArgumentNullException(nameof(dbConnectionFactory), nameof(BlobsRepository));
		}

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
