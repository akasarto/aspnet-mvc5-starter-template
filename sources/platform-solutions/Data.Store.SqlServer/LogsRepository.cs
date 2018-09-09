using Dapper;
using Data.Core;
using Domain.Core.Entities;
using Domain.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data;

namespace Data.Store.SqlServer
{
	public partial class LogsRepository : ILogsRepository
	{
		internal readonly IDbConnectionFactory _dbConnectionFactory = null;

		/// <summary>
		/// Contructor method.
		/// </summary>
		public LogsRepository(IDbConnectionFactory dbConnectionFactory)
		{
			_dbConnectionFactory = dbConnectionFactory ?? throw new ArgumentNullException(nameof(dbConnectionFactory), nameof(UsersRepository));
		}

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
