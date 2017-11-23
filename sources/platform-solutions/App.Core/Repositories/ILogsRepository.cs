using App.Core.Entities;
using System.Collections.Generic;

namespace App.Core.Repositories
{
	/// <summary>
	/// Logs repository interface.
	/// </summary>
	public partial interface ILogsRepository
	{
		/// <summary>
		/// Get a list of log entities.
		/// </summary>
		/// <param name="top">The maximum numbers expected in the resulting list.</param>
		/// <returns>A collection of <see cref="LogEntity"/> instances.</returns>
		IEnumerable<LogEntity> GetAll(int top = 30);
	}
}
