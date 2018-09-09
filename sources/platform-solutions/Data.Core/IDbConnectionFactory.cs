using System.Data;

namespace Data.Core
{
	public partial interface IDbConnectionFactory
	{
		IDbConnection CreateConnection();
	}
}
