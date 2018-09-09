using Data.Core;
using System.Data;
using System.Data.SqlClient;

namespace Data.Store.SqlServer.Infrastructure
{
	public class SqlConnectionFactory : IDbConnectionFactory
	{
		private readonly string _connectionString = null;

		/// <summary>
		/// Constructor method.
		/// </summary>
		public SqlConnectionFactory(string connectionString)
		{
			_connectionString = connectionString;
		}

		public virtual IDbConnection CreateConnection()
		{
			return new SqlConnection(_connectionString);
		}
	}
}
