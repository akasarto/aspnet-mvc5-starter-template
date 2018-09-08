using Domain.Core.Entities;
using System.Collections.Generic;

namespace Domain.Core.Repositories
{
	public partial interface ILogsRepository
	{
		IEnumerable<LogEntity> GetAll(int top = 30);
	}
}
