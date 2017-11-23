namespace Sarto.Extensions
{
	public static partial class StringExtensions
	{
		/// <summary>
		/// Returns a placeholder text when the input string is null, empty or consist of white spaces.
		/// </summary>
		/// <param name="this">The extended string.</param>
		/// <param name="placeholderText">The placeholder string.</param>
		/// <returns>The input string or the placeholder.</returns>
		public static string WhenNullOrWhiteSpace(this string @this, string placeholderText)
		{
			if (string.IsNullOrWhiteSpace(@this))
			{
				return placeholderText ?? string.Empty;
			}

			return @this;
		}
	}
}
