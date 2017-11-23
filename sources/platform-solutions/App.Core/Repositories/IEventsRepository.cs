using App.Core.Entities;
using System.Collections.Generic;

namespace App.Core.Repositories
{
	/// <summary>
	/// Events repository interface.
	/// </summary>
	public interface IEventsRepository
	{
		/// <summary>
		/// Create an event.
		/// </summary>
		/// <param name="eventEntity">The instance with data for the new event.</param>
		/// <param name="userId">The id of the user creating the event.</param>
		/// <returns>An <see cref="EventEntity"/> instance with the new id set.</returns>
		EventEntity Create(EventEntity eventEntity, int? userId = null);

		/// <summary>
		/// Delete an event.
		/// </summary>
		/// <param name="eventId">The id of the event to be deleted.</param>
		void Delete(int eventId);

		/// <summary>
		/// Get a list of events.
		/// </summary>
		/// <param name="userId">The option user that the events are associated with.</param>
		/// <returns>A collection of <see cref="EventEntity"/>.</returns>
		IEnumerable<EventEntity> GetAll(int? userId = null);

		/// <summary>
		/// Get an event by id.
		/// </summary>
		/// <param name="eventId">The id of the event to lookup.</param>
		/// <returns>A single <see cref="EventEntity"/> instance.</returns>
		EventEntity GetById(int eventId);

		/// <summary>
		/// Update an event.
		/// </summary>
		/// <param name="eventEntity">The instance with updated data.</param>
		void Update(EventEntity eventEntity);
	}
}
