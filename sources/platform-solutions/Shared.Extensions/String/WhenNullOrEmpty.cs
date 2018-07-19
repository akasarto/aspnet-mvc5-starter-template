namespace Shared.Extensions
{
	public static partial class StringExtensions
	{
		/// <summary>
		/// Returns a placeholder text when the input string is null or empty.
		/// </summary>
		/// <param name="this">The extended string.</param>
		/// <param name="placeholderText">The placeholder string.</param>
		/// <returns>The input string or the placeholder.</returns>
		public static string WhenNullOrEmpty(this string @this, string placeholderText)
		{
			if (string.IsNullOrEmpty(@this))
			{
				return placeholderText ?? string.Empty;
			}

			return @this;
		}
	}
}
