using App.Core.Entities;
using App.Core.Repositories;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace App.Data
{
	/// <summary>
	/// Event repository implementation.
	/// </summary>
	public class EventsRepository : IEventsRepository
	{
		internal readonly IDbConnectionFactory _dbConnectionFactory = null;

		/// <summary>
		/// Contructor method.
		/// </summary>
		/// <param name="dbConnectionFactory">The current connection factory instance.</param>
		public EventsRepository(IDbConnectionFactory dbConnectionFactory)
		{
			_dbConnectionFactory = dbConnectionFactory ?? throw new ArgumentNullException(nameof(dbConnectionFactory), nameof(EventsRepository));
		}

		/// <summary>
		/// Create an event.
		/// </summary>
		/// <param name="eventEntity">The instance with data for the new event.</param>
		/// <param name="userId">The id of the user creating the event.</param>
		/// <returns>An <see cref="EventEntity"/> instance with the new id set.</returns>
		public EventEntity Create(EventEntity eventEntity, int? userId = null)
		{
			using (var connection = _dbConnectionFactory.CreateConnection())
			{
				connection.Open();

				using (var transaction = connection.BeginTransaction())
				{
					var parameters = new DynamicParameters();

					parameters.Add("@Name", eventEntity.Name, DbType.String);
					parameters.Add("@Color", eventEntity.Color, DbType.AnsiString);
					parameters.Add("@StartDate", eventEntity.StartDate, DbType.DateTimeOffset);
					parameters.Add("@EndDate", eventEntity.EndDate, DbType.DateTimeOffset);
					parameters.Add("@Description", eventEntity.Description, DbType.String);
					parameters.Add("@TimeZoneId", eventEntity.TimeZoneId, DbType.String);

					parameters.Add("@UserId", userId, DbType.Int32);

					eventEntity.Id = connection.ExecuteScalar<int>(
						sql: "EventsInsert",
						commandType: CommandType.StoredProcedure,
						transaction: transaction,
						param: parameters
					);

					EventBlobsSet(connection, transaction, eventEntity.Blobs, eventId: eventEntity.Id, cleanup: false);

					transaction.Commit();

					return eventEntity;
				}
			}
		}

		/// <summary>
		/// Delete an event.
		/// </summary>
		/// <param name="eventId">The id of the event to be deleted.</param>
		public void Delete(int eventId)
		{
			using (var connection = _dbConnectionFactory.CreateConnection())
			{
				var parameters = new DynamicParameters();

				parameters.Add("@EventId", eventId, DbType.Int32);

				connection.Execute(
					sql: @"UPDATE  [Events] SET IsDeleted = 1 WHERE [Id] = @EventId",
					commandType: CommandType.Text,
					param: parameters
				);
			}
		}

		/// <summary>
		/// Get a list of events.
		/// </summary>
		/// <param name="userId">The option user that the events are associated with.</param>
		/// <returns>A collection of <see cref="EventEntity"/>.</returns>
		public IEnumerable<EventEntity> GetAll(int? userId = null)
		{
			using (var connection = _dbConnectionFactory.CreateConnection())
			{
				var parameters = new DynamicParameters();

				parameters.Add("@UserId", userId, DbType.Int32);

				using (var reader = connection.QueryMultiple(sql: "EventsSelect", commandType: CommandType.StoredProcedure, param: parameters))
				{
					return BuildEntitiesList(reader);
				}
			}
		}

		/// <summary>
		/// Get an event by id.
		/// </summary>
		/// <param name="eventId">The id of the event to lookup.</param>
		/// <returns>A single <see cref="EventEntity"/> instance.</returns>
		public EventEntity GetById(int eventId)
		{
			using (var connection = _dbConnectionFactory.CreateConnection())
			{
				var parameters = new DynamicParameters();

				parameters.Add("@EventId", eventId, DbType.Int32);

				using (var reader = connection.QueryMultiple(sql: "EventsSelect", commandType: CommandType.StoredProcedure, param: parameters))
				{
					return BuildEntity(reader);
				}
			}
		}

		/// <summary>
		/// Update an event.
		/// </summary>
		/// <param name="eventEntity">The instance with updated data.</param>
		public void Update(EventEntity eventEntity)
		{
			using (var connection = _dbConnectionFactory.CreateConnection())
			{
				connection.Open();

				using (var transaction = connection.BeginTransaction())
				{
					var parameters = new DynamicParameters();

					parameters.Add("@Id", eventEntity.Id, DbType.Int32);
					parameters.Add("@Name", eventEntity.Name, DbType.String);
					parameters.Add("@Color", eventEntity.Color, DbType.AnsiString);
					parameters.Add("@StartDate", eventEntity.StartDate, DbType.DateTimeOffset);
					parameters.Add("@EndDate", eventEntity.EndDate, DbType.DateTimeOffset);
					parameters.Add("@Description", eventEntity.Description, DbType.String);
					parameters.Add("@TimeZoneId", eventEntity.TimeZoneId, DbType.String);

					int result = connection.Execute(
						sql: "EventsUpdate",
						commandType: CommandType.StoredProcedure,
						transaction: transaction,
						param: parameters
					);

					EventBlobsSet(connection, transaction, eventEntity.Blobs, eventId: eventEntity.Id, cleanup: true);

					transaction.Commit();
				}
			}
		}

		/// <summary>
		/// Build a full event instance.
		/// </summary>
		/// <param name="reader">The reader from the executed query.</param>
		/// <returns>Returns a single <see cref="EventEntity"/> instance.</returns>
		private EventEntity BuildEntity(SqlMapper.GridReader reader)
		{
			var entity = reader.Read<EventEntity>().SingleOrDefault();

			if (entity != null)
			{
				entity.Blobs = reader.Read<EventBlobEntity>().ToList();
			}

			return entity;
		}

		/// <summary>
		/// Buils a list of full event instances.
		/// </summary>
		/// <param name="reader">The reader from the executed query.</param>
		/// <returns>Returns a collection of <see cref="EventEntity"/> instances.</returns>
		private IEnumerable<EventEntity> BuildEntitiesList(SqlMapper.GridReader reader)
		{
			var entities = reader.Read<EventEntity>().ToList();

			var blobsCollection = reader
				.Read<EventBlobEntity>()
				.GroupBy(eImg => eImg.EventId)
				.ToDictionary(group => group.Key, group => group.ToList());

			foreach (var entity in entities)
			{
				List<EventBlobEntity> blobs;

				if (!blobsCollection.TryGetValue(entity.Id, out blobs))
				{
					blobs = new List<EventBlobEntity>();
				}

				entity.Blobs = blobs;
			}

			return entities;
		}

		/// <summary>
		/// Set the associated blobs list.
		/// </summary>
		/// <param name="dbConnection">The current connection isntance.</param>
		/// <param name="transaction">The current transaction instance.</param>
		/// <param name="blobs">The associated blobs.</param>
		/// <param name="eventId">The current event id.</param>
		/// <param name="cleanup">Flags if previously associated blobs should be removed before adding the new ones.</param>
		private void EventBlobsSet(IDbConnection dbConnection, IDbTransaction transaction, List<EventBlobEntity> blobs, int eventId, bool cleanup)
		{
			if (cleanup)
			{
				var parameters = new DynamicParameters();

				parameters.Add(@"EventId", eventId, DbType.Int32);

				dbConnection.Execute(
					sql: "DELETE FROM [EventBlobs] WHERE EventId = @EventId",
					commandType: CommandType.Text,
					transaction: transaction,
					param: parameters
				);
			}

			foreach (var blob in blobs)
			{
				var parameters = new DynamicParameters();

				parameters.Add("@EventId", eventId, DbType.Int32);
				parameters.Add("@BlobId", blob.Id, DbType.Guid);
				parameters.Add("@OrderIndex", blob.OrderIndex, DbType.Int32);
				parameters.Add("@Label", blob.Label, DbType.String);
				parameters.Add("@Description", blob.Description, DbType.String);

				dbConnection.Query(
					sql: "EventBlobsInsert",
					commandType: CommandType.StoredProcedure,
					transaction: transaction,
					param: parameters
				);
			}
		}
	}
}
