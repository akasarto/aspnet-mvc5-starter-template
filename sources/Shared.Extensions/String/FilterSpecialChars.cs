using System.Text.RegularExpressions;

namespace Shared.Extensions
{
	public static partial class StringExtensions
	{
		public static readonly Regex SpecialChars = new Regex(@"[^a-zA-Z0-9]", RegexOptions.Compiled);

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
