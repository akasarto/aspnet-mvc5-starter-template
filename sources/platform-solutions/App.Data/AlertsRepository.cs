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
	/// Alert repository implementation.
	/// </summary>
	public partial class AlertsRepository : IAlertsRepository
	{
		internal readonly IDbConnectionFactory _dbConnectionFactory = null;

		/// <summary>
		/// Contructor method.
		/// </summary>
		/// <param name="dbConnectionFactory">The current connection factory instance.</param>
		public AlertsRepository(IDbConnectionFactory dbConnectionFactory)
		{
			_dbConnectionFactory = dbConnectionFactory ?? throw new ArgumentNullException(nameof(dbConnectionFactory), nameof(AlertsRepository));
		}

		/// <summary>
		/// Create an alert.
		/// </summary>
		/// <param name="alertEntity">The instance with data for the new alert.</param>
		/// <returns>An <see cref="AlertEntity"/> instance with the new id set.</returns>
		public AlertEntity Create(AlertEntity alertEntity)
		{
			using (var connection = _dbConnectionFactory.CreateConnection())
			{
				connection.Open();

				using (var transaction = connection.BeginTransaction())
				{
					var parameters = new DynamicParameters();

					parameters.Add("@Type", alertEntity.Type, DbType.Byte);
					parameters.Add("@Message", alertEntity.Message, DbType.String);

					alertEntity.Id = connection.ExecuteScalar<int>(
						sql: "AlertsInsert",
						commandType: CommandType.StoredProcedure,
						transaction: transaction,
						param: parameters
					);

					AlertUsersSet(connection, transaction, users: alertEntity.Users, alert: alertEntity, cleanup: false);

					transaction.Commit();

					return alertEntity;
				}
			}
		}

		/// <summary>
		/// Delete an alert.
		/// </summary>
		/// <param name="alertId">The id of the alert to be deleted.</param>
		public void Delete(int alertId)
		{
			using (var connection = _dbConnectionFactory.CreateConnection())
			{
				var parameters = new DynamicParameters();

				parameters.Add("@Id", alertId, DbType.Int32);

				connection.Execute(
					sql: @"UPDATE [Alerts] SET IsDeleted = 1 WHERE [Id] = @Id",
					commandType: CommandType.Text,
					param: parameters
				);
			}
		}

		/// <summary>
		/// Get a list of alerts.
		/// </summary>
		/// <param name="userId">The optional user id that the alerts are associated with.</param>
		/// <returns>A collection of <see cref="AlertEntity"/>.</returns>
		public IEnumerable<AlertEntity> GetAll(int? userId = null)
		{
			using (var connection = _dbConnectionFactory.CreateConnection())
			{
				var parameters = new DynamicParameters();

				parameters.Add("@UserId", userId, DbType.Int32);

				using (var reader = connection.QueryMultiple(sql: "AlertsSelect", commandType: CommandType.StoredProcedure, param: parameters))
				{
					return BuildEntities(reader);
				}
			}
		}

		/// <summary>
		/// Get an alert by id.
		/// </summary>
		/// <param name="alertId">The id of the alert to lookup.</param>
		/// <returns>A single <see cref="AlertEntity"/> instance.</returns>
		public AlertEntity GetById(int alertId)
		{
			using (var connection = _dbConnectionFactory.CreateConnection())
			{
				var parameters = new DynamicParameters();

				parameters.Add("@AlertId", alertId, DbType.Int32);

				using (var reader = connection.QueryMultiple(sql: "AlertsSelect", commandType: CommandType.StoredProcedure, param: parameters))
				{
					return BuildEntity(reader);
				}
			}
		}

		/// <summary>
		/// Update an alert.
		/// </summary>
		/// <param name="alertEntity">The instance with updated data.</param>
		public void Update(AlertEntity alertEntity)
		{
			using (var connection = _dbConnectionFactory.CreateConnection())
			{
				connection.Open();

				using (var transaction = connection.BeginTransaction())
				{
					var parameters = new DynamicParameters();

					parameters.Add("@Id", alertEntity.Id, DbType.Int32);
					parameters.Add("@Type", alertEntity.Type, DbType.Byte);
					parameters.Add("@Message", alertEntity.Message, DbType.String);

					int result = connection.Execute(
						sql: "AlertsUpdate",
						commandType: CommandType.StoredProcedure,
						transaction: transaction,
						param: parameters
					);

					AlertUsersSet(connection, transaction, users: alertEntity.Users, alert: alertEntity, cleanup: true);

					transaction.Commit();
				}
			}
		}

		/// <summary>
		/// Updates the date that the alert was marked as read.
		/// </summary>
		/// <param name="alertId">The alert id.</param>
		/// <param name="userId">The id of the user marking it as read.</param>
		/// <param name="utcRead">The actual date (UTC) it was read.</param>
		public void UpdateUTCRead(int alertId, int userId, DateTime utcRead)
		{
			using (var connection = _dbConnectionFactory.CreateConnection())
			{
				connection.Open();

				using (var transaction = connection.BeginTransaction())
				{
					var parameters = new DynamicParameters();

					parameters.Add("@AlertId", alertId, DbType.Int32);
					parameters.Add("@UserId", userId, DbType.Int32);
					parameters.Add("@UTCRead", utcRead, DbType.DateTime2);

					int result = connection.Execute(
						sql: @"UPDATE [Alerts] SET [UTCRead] = @UTCRead WHERE [Id] = @AlertId ",
						commandType: CommandType.Text,
						transaction: transaction,
						param: parameters
					);

					transaction.Commit();
				}
			}
		}

		/// <summary>
		/// Build a full alert instance.
		/// </summary>
		/// <param name="reader">The reader from the executed query.</param>
		/// <returns>Returns a single <see cref="AlertEntity"/> instance.</returns>
		private AlertEntity BuildEntity(SqlMapper.GridReader reader)
		{
			var entity = reader.Read<AlertEntity>().SingleOrDefault();

			if (entity != null)
			{
				entity.Users = reader.Read<UserAlert>().ToList();
			}

			return entity;
		}

		/// <summary>
		/// Buils a list of full alert instances.
		/// </summary>
		/// <param name="reader">The reader from the executed query.</param>
		/// <returns>Returns a collection of <see cref="AlertEntity"/> instances.</returns>
		private IEnumerable<AlertEntity> BuildEntities(SqlMapper.GridReader reader)
		{
			var entities = reader.Read<AlertEntity>().ToList();

			var usersCollection = reader
				.Read<UserAlert>()
				.GroupBy(uAlert => uAlert.UserId)
				.ToDictionary(group => group.Key, group => group.ToList());

			foreach (var entity in entities)
			{
				List<UserAlert> users;

				if (!usersCollection.TryGetValue(entity.Id, out users))
				{
					users = new List<UserAlert>();
				}

				entity.Users = users;
			}

			return entities;
		}

		/// <summary>
		/// Set the associated users list.
		/// </summary>
		/// <param name="dbConnection">The current connection isntance.</param>
		/// <param name="transaction">The current transaction instance.</param>
		/// <param name="users">The associated users.</param>
		/// <param name="alert">The current alert entity.</param>
		/// <param name="cleanup">Flags if previously associated users should be removed before adding the new ones.</param>
		private void AlertUsersSet(IDbConnection dbConnection, IDbTransaction transaction, List<UserAlert> users, AlertEntity alert, bool cleanup)
		{
			if (cleanup)
			{
				var parameters = new DynamicParameters();

				parameters.Add(@"AlertId", alert.Id, DbType.Int32);

				dbConnection.Execute(
					sql: "DELETE FROM [UserAlerts] WHERE AlertId = @AlertId",
					commandType: CommandType.Text,
					transaction: transaction,
					param: parameters
				);
			}

			foreach (var user in users)
			{
				var parameters = new DynamicParameters();

				parameters.Add("@AlertId", alert.Id, DbType.Int32);
				parameters.Add("@UserId", user.UserId, DbType.Int32);

				dbConnection.Query(
					sql: "INSERT INTO [UserAlerts] (UserId, [AlertId]) VALUES (@UserId, @AlertId)",
					commandType: CommandType.Text,
					transaction: transaction,
					param: parameters
				);
			}
		}
	}
}