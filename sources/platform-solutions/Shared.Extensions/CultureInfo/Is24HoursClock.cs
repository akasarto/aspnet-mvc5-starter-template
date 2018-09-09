using System;
using System.Globalization;

namespace Shared.Extensions
{
	public static partial class CultureInfoExtensions
	{
		public static bool Is24HoursClock(this CultureInfo @this)
		{
			if (@this == null)
			{
				throw new ArgumentNullException(nameof(CultureInfo));
			}

			return @this.DateTimeFormat.ShortTimePattern.Contains("H");
		}
	}
}
