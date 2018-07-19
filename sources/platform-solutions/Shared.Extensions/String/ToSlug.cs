using System.Text.RegularExpressions;

namespace Shared.Extensions
{
	public static partial class StringExtensions
	{
		/// <summary>
		/// Detects multiple consecutive white spaces.
		/// </summary>
		public static readonly Regex MultiWhiteSpaces = new Regex(@"\s+", RegexOptions.Compiled);

		/// <summary>
		/// Covnert the input string to a URL friendly representation (slug).
		/// </summary>
		/// <param name="this">The extended string.</param>
		/// <returns>The transformed string.</returns>
		public static string ToSlug(this string @this)
		{
			var wordSepparator = "-";

			if (string.IsNullOrWhiteSpace(@this))
			{
				return @this;
			}

			var filtered = @this.FilterSpecialChars(replacer: " ").ToLower();

			var slug = MultiWhiteSpaces.Replace(filtered, wordSepparator);

			return slug.Trim(wordSepparator.ToCharArray());
		}
	}
}
