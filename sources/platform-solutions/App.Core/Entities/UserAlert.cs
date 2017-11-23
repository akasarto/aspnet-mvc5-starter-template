namespace App.Core.Entities
{
	/// <summary>
	/// Represents a system alert associated with the user.
	/// </summary>
	public class UserAlert
	{
		/// <summary>
		/// Gets or sets the user id.
		/// </summary>
		public int UserId { get; set; }

		/// <summary>
		/// Gets or sets the alert id.
		/// </summary>
		public int AlertId { get; set; }
	}
}
