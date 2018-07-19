using System;

namespace Domain.Core.Entities
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
		/// Gets or sets the level type.
		/// </summary>
		public string Level { get; set; }

		/// <summary>
		/// Gets or sets the message.
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// Gets or sets the exception.
		/// </summary>
		public string Exception { get; set; }

		/// <summary>
		/// Gets or sets the properties.
		/// </summary>
		public string Properties { get; set; }

		/// <summary>
		/// Gets or sets the creation date.
		/// </summary>
		public DateTime TimeStamp { get; set; }

		/// <summary>
		/// Gets or sets the associated activity id.
		/// </summary>
		public Guid ActivityId { get; set; }
	}
}
