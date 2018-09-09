namespace Domain.Core.Sessions
{
	public class UserSessionContext : ISessionContext
	{
		/// <summary>
		/// Private constructor method.
		/// Used byt the null pattern implementation.
		/// </summary>
		private UserSessionContext() { }

		/// <summary>
		/// Constructor method.
		/// </summary>
		public UserSessionContext(int userId)
		{
			UserId = userId;
		}

		public static UserSessionContext Null => new NullAppExecutionContext();
		public int UserId { get; } = 0;

		public class NullAppExecutionContext : UserSessionContext
		{
		}
	}
}
