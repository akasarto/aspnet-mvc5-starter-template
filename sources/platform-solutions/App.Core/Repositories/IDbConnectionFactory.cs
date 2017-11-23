using System.Data;

namespace App.Core.Repositories
{
	/// <summary>
	/// Database connections factory interface.
	/// </summary>
	public partial interface IDbConnectionFactory
	{
		/// <summary>
		/// Create a new database connection instance.
		/// </summary>
		/// <returns>A <see cref="IDbConnection"/> instance.</returns>
		IDbConnection CreateConnection();
	}
}
