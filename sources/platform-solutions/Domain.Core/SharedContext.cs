using Domain.Core.Interfaces;

namespace Domain.Core
{
	public class SharedContext : ISharedContext
	{
		/// <summary>
		/// Private constructor method.
		/// Used byt the null pattern implementation.
		/// </summary>
		private SharedContext() { }

		/// <summary>
		/// Constructor method.
		/// </summary>
		public SharedContext(int userId)
		{
			UserId = userId;
		}

		public static SharedContext Null => new NullAppExecutionContext();
		public int UserId { get; } = 0;

		public class NullAppExecutionContext : SharedContext
		{
		}
	}
}
