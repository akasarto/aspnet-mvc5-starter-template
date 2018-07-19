namespace Shared.Extensions
{
	public static partial class StringExtensions
	{
		/// <summary>
		/// Check if the string is null, empty or consist of white spaces.
		/// </summary>
		/// <remarks>This is a shortcut for the native method.</remarks>
		/// <param name="this">The extended string.</param>
		/// <returns><c>True</c> or <c>false</c>.</returns>
		public static bool IsNullOrWhiteSpace(this string @this)
		{
			return string.IsNullOrWhiteSpace(@this);
		}
	}
}
