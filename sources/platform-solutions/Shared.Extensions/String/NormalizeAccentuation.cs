using System.Globalization;
using System.Text;

namespace Shared.Extensions
{
	public static partial class StringExtensions
	{
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
