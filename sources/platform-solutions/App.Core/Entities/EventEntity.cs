using System;
using System.Collections.Generic;

namespace App.Core.Entities
{
	/// <summary>
	/// Represents an entity in the system.
	/// </summary>
	public class EventEntity
	{
		/// <summary>
		/// Gets or sets the id.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the creation date.
		/// </summary>
		public DateTime UTCCreation { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the highlight color.
		/// </summary>
		public string Color { get; set; }

		/// <summary>
		/// Gets or sets an indication if it is an all day event.
		/// </summary>
		public bool AllDay { get; set; }

		/// <summary>
		/// Gets or sets the event start date (UTC with offset).
		/// </summary>
		public DateTimeOffset StartDate { get; set; }

		/// <summary>
		/// Gets or sets the event end date (UTC with offset).
		/// </summary>
		public DateTimeOffset EndDate { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the event time zone.
		/// </summary>
		public string TimeZoneId { get; set; }

		/// <summary>
		/// Gets or sets the associated blobs.
		/// </summary>
		public List<EventBlobEntity> Blobs { get; set; } = new List<EventBlobEntity>();
	}
}
