namespace Domain.Core.Entities
{
	/// <summary>
	/// Represents a claim associated with the user.
	/// </summary>
	public class UserClaim
	{
		/// <summary>
		/// Gets or sets the claim id.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the user id.
		/// </summary>
		public int UserId { get; set; }

		/// <summary>
		/// Gets or sets the claim type.
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		/// Gets or sets the claim value.
		/// </summary>
		public string Value { get; set; }
	}
}
