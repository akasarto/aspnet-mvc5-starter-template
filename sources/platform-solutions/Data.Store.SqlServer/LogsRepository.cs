using Dapper;
using Data.Core;
using Domain.Core.Entities;
using Domain.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data;

namespace Data.Store.SqlServer
{
	/// <summary>
	/// Logs repository implementation.
	/// </summary>
	public partial class LogsRepository : ILogsRepository
	{
		internal readonly IDbConnectionFactory _dbConnectionFactory = null;

		/// <summary>
		/// Contructor method.
		/// </summary>
		/// <param name="dbConnectionFactory">The current connection factory instance.</param>
		public LogsRepository(IDbConnectionFactory dbConnectionFactory)
		{
			_dbConnectionFactory = dbConnectionFactory ?? throw new ArgumentNullException(nameof(dbConnectionFactory), nameof(UsersRepository));
		}

		/// <summary>
		/// Get a list of log entities.
		/// </summary>
		/// <param name="top">The maximum numbers expected in the resulting list.</param>
		/// <returns>A collection of <see cref="LogEntity"/> instances.</returns>
		public IEnumerable<LogEntity> GetAll(int top = 30)
		{
			var parameters = new DynamicParameters();

			parameters.Add("@Top", top, DbType.Int32);

			using (var connection = _dbConnectionFactory.CreateConnection())
			{
				var reader = connection.Query<LogEntity>(
					sql: "LogsSelect",
					commandType: CommandType.StoredProcedure,
					param: parameters
				);

				return reader;
			}
		}
	}
}
