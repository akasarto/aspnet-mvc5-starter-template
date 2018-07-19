using System.Globalization;
using System.Text;

namespace Shared.Extensions
{
	public static partial class StringExtensions
	{
		/// <summary>
		/// Convert accentuated chars into ther ASCII correspondent.
		/// </summary>
		/// <param name="this">The extended string.</param>
		/// <returns>The filtered string.</returns>
		public static string NormalizeAccentuation(this string @this)
		{
			if (string.IsNullOrWhiteSpace(@this))
			{
				return string.Empty;
			}

			var stringBuilder = new StringBuilder();
			var normalizedString = @this.Normalize(NormalizationForm.FormD);

			foreach (var c in normalizedString)
			{
				var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);

				if (unicodeCategory != UnicodeCategory.NonSpacingMark)
				{
					stringBuilder.Append(c);
				}
			}

			return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
		}
	}
}
