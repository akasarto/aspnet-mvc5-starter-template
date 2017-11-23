using App.Core.Entities;
using System;
using System.Collections.Generic;

namespace App.Core.Repositories
{
	/// <summary>
	/// Alerts repository interface.
	/// </summary>
	public interface IAlertsRepository
	{
		/// <summary>
		/// Create an alert.
		/// </summary>
		/// <param name="alertEntity">The instance with data for the new alert.</param>
		/// <returns>An <see cref="AlertEntity"/> instance with the new id set.</returns>
		AlertEntity Create(AlertEntity alertEntity);

		/// <summary>
		/// Delete an alert.
		/// </summary>
		/// <param name="alertId">The id of the alert to be deleted.</param>
		void Delete(int alertId);

		/// <summary>
		/// Get a list of alerts.
		/// </summary>
		/// <param name="userId">The optional user id that the alerts are associated with.</param>
		/// <returns>A collection of <see cref="AlertEntity"/>.</returns>
		IEnumerable<AlertEntity> GetAll(int? userId = null);

		/// <summary>
		/// Get an alert by id.
		/// </summary>
		/// <param name="alertId">The id of the alert to lookup.</param>
		/// <returns>A single <see cref="AlertEntity"/> instance.</returns>
		AlertEntity GetById(int alertId);

		/// <summary>
		/// Update an alert.
		/// </summary>
		/// <param name="alertEntity">The instance with updated data.</param>
		void Update(AlertEntity alertEntity);

		/// <summary>
		/// Updates the date that the alert was marked as read.
		/// </summary>
		/// <param name="alertId">The alert id.</param>
		/// <param name="userId">The id of the user marking it as read.</param>
		/// <param name="utcRead">The actual date (UTC) it was read.</param>
		void UpdateUTCRead(int alertId, int userId, DateTime utcRead);
	}
}
