namespace Domain.Core.Entities
{
	public class UserClaim
	{
		public int Id { get; set; }
		public string Type { get; set; }
		public int UserId { get; set; }
		public string Value { get; set; }
	}
}
