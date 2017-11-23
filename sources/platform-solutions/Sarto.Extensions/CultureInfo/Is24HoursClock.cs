using System;
using System.Globalization;

/// <summary>
/// Provide general purpose extensions for .net primitive types.
/// </summary>
namespace Sarto.Extensions
{
	public static partial class CultureInfoExtensions
	{
		/// <summary>
		/// Checks if the curren culture time represents the time in 24h format or meridiem (AM/PM).
		/// </summary>
		/// <param name="this">The <see cref="CultureInfo"/> instance being extended.</param>
		/// <returns>A boolean indicating if it is 24h clock.</returns>
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
