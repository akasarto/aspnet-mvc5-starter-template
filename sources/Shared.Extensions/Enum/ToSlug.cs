using System;
using System.Text.RegularExpressions;

namespace Shared.Extensions
{
	public static partial class EnumExtensions
	{
		public static readonly Regex NonInitialUpperCaseChars = new Regex(@"(?<!^)(?=[A-Z])", RegexOptions.Compiled);

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
