using System.Text.RegularExpressions;

namespace Shared.Extensions
{
	public static partial class StringExtensions
	{
		public static readonly Regex MultiWhiteSpaces = new Regex(@"\s+", RegexOptions.Compiled);

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
