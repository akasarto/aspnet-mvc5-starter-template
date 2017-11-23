namespace Sarto.Extensions
{
	public static partial class StringExtensions
	{
		/// <summary>
		/// Check if the string is null or empty.
		/// </summary>
		/// <remarks>This is a shortcut for the native method.</remarks>
		/// <param name="this">The extended string.</param>
		/// <returns><c>True</c> or <c>false</c>.</returns>
		public static bool IsNullOrEmpty(this string @this)
		{
			return string.IsNullOrEmpty(@this);
		}
	}
}
