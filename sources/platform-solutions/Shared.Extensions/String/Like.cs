using System;

namespace Shared.Extensions
{
	public static partial class StringExtensions
	{
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
