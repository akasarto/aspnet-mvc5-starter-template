using System;
using System.Collections.Generic;

namespace App.Core.Entities
{
	/// <summary>
	/// Represents an alert in the sytem.
	/// </summary>
	public class AlertEntity
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
		/// Gets or sets the deleted state.
		/// </summary>
		public bool IsDeleted { get; set; }
		
		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		public AlertType Type { get; set; }

		/// <summary>
		/// Gets or sets the message.
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// Gets or sets the date that the user marked the alert as read.
		/// </summary>
		public DateTime? UTCRead { get; set; }

		/// <summary>
		/// Gets or sets the optional action URL.
		/// </summary>
		public string ActionUrl { get; set; }

		/// <summary>
		/// Gets or sets the users associated with this alert.
		/// </summary>
		public List<UserAlert> Users { get; set; } = new List<UserAlert>();
	}
}
