namespace App.Core.Entities
{
	/// <summary>
	/// Represents an event blob in the system.
	/// </summary>
	public class EventBlobEntity : BlobEntity
	{
		/// <summary>
		/// Gets or sets the event id.
		/// </summary>
		public int EventId { get; set; }

		/// <summary>
		/// Gets or sets the order index.
		/// </summary>
		public int OrderIndex { get; set; }

		/// <summary>
		/// Gets or sets the label.
		/// </summary>
		public string Label { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		public string Description { get; set; }
	}
}
