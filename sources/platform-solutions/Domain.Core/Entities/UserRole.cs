namespace Domain.Core.Entities
{
	/// <summary>
	/// Represents a role associated with the user.
	/// </summary>
	public class UserRole
	{
		/// <summary>
		/// Gets or sets the user id.
		/// </summary>
		public int UserId { get; set; }

		/// <summary>
		/// Gets or sets the user role.
		/// </summary>
		public Role Role { get; set; }
	}
}
