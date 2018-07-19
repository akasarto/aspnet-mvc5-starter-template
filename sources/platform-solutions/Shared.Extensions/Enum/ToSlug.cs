using System;
using System.Text.RegularExpressions;

namespace Shared.Extensions
{
	public static partial class EnumExtensions
	{
		/// <summary>
		/// Regular expression that detects upper cased chars in the text.
		/// </summary>
		public static readonly Regex NonInitialUpperCaseChars = new Regex(@"(?<!^)(?=[A-Z])", RegexOptions.Compiled);

		/// <summary>
		/// Convert the enum value to a URL friendly representation (slug).
		/// </summary>
		/// <param name="this">The extended <see cref="Enum"/>.</param>
		/// <returns>The transformed <see cref="Enum"/> value as string.</returns>
		public static string ToSlug(this Enum @this)
		{
			if (@this == null)
			{
				return string.Empty;
			}

			var enumString = NonInitialUpperCaseChars.Replace(@this.ToString(), ".");

			return enumString.ToSlug();
		}
	}
}
