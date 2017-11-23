namespace App.Core
{
	/// <summary>
	/// Represents the shared inforation available in the current execution context.
	/// </summary>
	public class SharedContext
	{
		private int _userId = 0;

		/// <summary>
		/// Constructor method.
		/// </summary>
		/// <param name="userId">The current user in this context.</param>
		public SharedContext(int userId)
		{
			_userId = userId;
		}

		/// <summary>
		/// Constructor method.
		/// </summary>
		internal SharedContext() { }

		/// <summary>
		/// Gets a null value.
		/// </summary>
		public static SharedContext Null => new NullAppExecutionContext();

		/// <summary>
		/// Gets the current user id.
		/// </summary>
		public int UserId => _userId;

		/// <summary>
		/// Represents a nulled intance of <see cref="SharedContext" />
		/// </summary>
		public class NullAppExecutionContext : SharedContext
		{
		}
	}
}
