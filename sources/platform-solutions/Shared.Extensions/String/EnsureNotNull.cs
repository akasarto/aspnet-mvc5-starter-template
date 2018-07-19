namespace Shared.Extensions
{
	public static partial class StringExtensions
	{
		/// <summary>
		/// Ensures that the specified string will no be null.
		/// </summary>
		/// <param name="this">The extended string.</param>
		/// <returns>The input string or an empty string.</returns>
		public static string EnsureNotNull(this string @this)
		{
			return @this ?? string.Empty;
		}
	}
}
