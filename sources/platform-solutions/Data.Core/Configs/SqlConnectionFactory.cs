using Domain.Core.Repositories;
using System.Data;
using System.Data.SqlClient;

namespace Data.Core.Configs
{
	/// <summary>
	/// Sql connection factory implementation.
	/// </summary>
	public class SqlConnectionFactory : IDbConnectionFactory
	{
		private string _connectionString = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		/// <param name="connectionString">The sql server connection string.</param>
		public SqlConnectionFactory(string connectionString)
		{
			_connectionString = connectionString;
		}

		/// <summary>
		/// Create a new database connection instance.
		/// </summary>
		/// <returns>A <see cref="IDbConnection"/> instance.</returns>
		public virtual IDbConnection CreateConnection()
		{
			return new SqlConnection(_connectionString);
		}
	}
}
