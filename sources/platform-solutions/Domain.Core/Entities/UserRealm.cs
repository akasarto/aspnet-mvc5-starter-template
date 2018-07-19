namespace Domain.Core.Entities
{
	/// <summary>
	/// Represents a system realm associated with the user.
	/// </summary>
	public class UserRealm
	{
		/// <summary>
		/// Gets or sets the user id.
		/// </summary>
		public int UserId { get; set; }

		/// <summary>
		/// Gets or sets the realm.
		/// </summary>
		public Realm Realm { get; set; }
	}
}
