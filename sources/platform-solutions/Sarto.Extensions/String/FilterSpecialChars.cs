using System.Text.RegularExpressions;

namespace Sarto.Extensions
{
	public static partial class StringExtensions
	{
		/// <summary>
		/// Detects non alphanumeric chars.
		/// </summary>
		public static readonly Regex SpecialChars = new Regex(@"[^a-zA-Z0-9]", RegexOptions.Compiled);

		/// <summary>
		/// Removes all non alphanumeric chars from the input string.
		/// </summary>
		/// <param name="this">The extended string.</param>
		/// <param name="replacer">The value to substitute special chars with.</param>
		/// <param name="normalizeAccentuated">Determines if accentuated chars should be removed or normalized as ASCII chars.</param>
		/// <returns>The filtered input string.</returns>
		public static string FilterSpecialChars(this string @this, string replacer = "", bool normalizeAccentuated = true)
		{
			if (string.IsNullOrWhiteSpace(@this))
			{
				return string.Empty;
			}

			if (normalizeAccentuated)
			{
				@this = @this.NormalizeAccentuation();
			}

			return SpecialChars.Replace(@this, replacer);
		}
	}
}
