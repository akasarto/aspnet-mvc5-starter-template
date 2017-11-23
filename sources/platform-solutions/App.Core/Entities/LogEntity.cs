using System;

namespace App.Core.Entities
{
	/// <summary>
	/// Represents a log entry in the system.
	/// </summary>
	public class LogEntity
	{
		/// <summary>
		/// Gets or sets the id.
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		/// Gets or sets the message.
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// Gets or sets the properties.
		/// </summary>
		public string Properties { get; set; }

		/// <summary>
		/// Gets or sets the creation date.
		/// </summary>
		public DateTime UTCCreation { get; set; }

		/// <summary>
		/// Gets or sets the associated activity id.
		/// </summary>
		public Guid ActivityId { get; set; }
	}
}