using System;

namespace Shared.Extensions
{
	public static partial class StringExtensions
	{
		/// <summary>
		/// Compare the input strings using invariant culture and ignoring casing.
		/// </summary>
		/// <param name="this">The extended string.</param>
		/// <param name="compareTo">The string to compare to.</param>
		/// <param name="compareNull">Determines if null values should be considered.</param>
		/// <returns><c>True</c> or <c>false</c>.</returns>
		public static bool Like(this string @this, string compareTo, bool compareNull = false)
		{
			if (@this == null || compareTo == null)
			{
				if (compareNull)
				{
					return @this == compareTo;
				}

				return false;
			}

			return @this.Trim().Equals(compareTo.Trim(), StringComparison.InvariantCultureIgnoreCase);
		}
	}
}
