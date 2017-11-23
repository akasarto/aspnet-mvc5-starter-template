using System;

namespace Sarto.Extensions
{
	public static partial class TimeSpanExtensions
	{
		/// <summary>
		/// Convert the time span to a culture based time format.
		/// </summary>
		/// <param name="this">The extended <see cref="TimeSpan"/> instance.</param>
		/// <returns>A culture based time representation.</returns>
		public static string ToLocalizedLabel(this TimeSpan? @this)
		{
			if (!@this.HasValue)
			{
				return null;
			}

			return @this.Value.ToLocalizedLabel();
		}

		/// <summary>
		/// Convert the time span to a culture based time format.
		/// </summary>
		/// <param name="this">The extended <see cref="TimeSpan"/> instance.</param>
		/// <returns>A culture based time representation.</returns>
		public static string ToLocalizedLabel(this TimeSpan @this)
		{
			var utc = DateTime.UtcNow;

			return new DateTime(utc.Year, utc.Month, utc.Day, @this.Hours, @this.Minutes, @this.Seconds).ToString("t");
		}
	}
}
