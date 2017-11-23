using System;

namespace Sarto.Extensions
{
	public static partial class TimeSpanExtensions
	{
		/// <summary>
		/// Convert to the specified time zone id/name from UTC.
		/// </summary>
		/// <param name="this">The extended <see cref="TimeSpan"/> instance.</param>
		/// <param name="toTimeZoneId">The time zone id/name to convert to.</param>
		/// <returns>A converted <see cref="DateTime"/> instance.</returns>
		public static DateTime ToTimeZone(this TimeSpan @this, string toTimeZoneId)
		{
			var toTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(toTimeZoneId);

			return @this.ToTimeZone(toTimeZoneInfo);
		}

		/// <summary>
		/// Convert between the specified time zones ids/names.
		/// </summary>
		/// <param name="this">The extended <see cref="TimeSpan"/> instance.</param>
		/// <param name="toTimeZoneId">The time zone id/name to convert to.</param>
		/// <param name="fromTimeZoneId">The time zone id/name to convert from.</param>
		/// <returns>A converted <see cref="DateTime"/> instance.</returns>
		public static DateTime ToTimeZone(this TimeSpan @this, string toTimeZoneId, string fromTimeZoneId)
		{
			var toTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(toTimeZoneId);
			var fromTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(fromTimeZoneId);

			return @this.ToTimeZone(toTimeZoneInfo, fromTimeZoneInfo);
		}

		/// <summary>
		/// Convert to the specified time zone instance from UTC.
		/// </summary>
		/// <param name="this">The extended <see cref="TimeSpan"/> instance.</param>
		/// <param name="toTimeZoneInfo">The time zone instance to convert to.</param>
		/// <returns>A converted <see cref="DateTime"/> instance.</returns>
		public static DateTime ToTimeZone(this TimeSpan @this, TimeZoneInfo toTimeZoneInfo)
		{
			var fromTimeZone = TimeZoneInfo.Utc;

			return @this.ToTimeZone(toTimeZoneInfo, fromTimeZone);
		}

		/// <summary>
		/// Convert between the specified time zones instances.
		/// </summary>
		/// <param name="this">The extended <see cref="TimeSpan"/> instance.</param>
		/// <param name="toTimeZoneInfo">The time zone instance to convert to.</param>
		/// <param name="fromTimeZoneInfo">The time zone instance to convert from.</param>
		/// <returns>A converted <see cref="DateTime"/> instance.</returns>
		public static DateTime ToTimeZone(this TimeSpan @this, TimeZoneInfo toTimeZoneInfo, TimeZoneInfo fromTimeZoneInfo)
		{
			if (toTimeZoneInfo == null)
			{
				throw new Exception(nameof(DateTimeExtensions), new ArgumentException(nameof(ToTimeZone), new ArgumentNullException(nameof(toTimeZoneInfo))));
			}

			if (fromTimeZoneInfo == null)
			{
				throw new Exception(nameof(DateTimeExtensions), new ArgumentException(nameof(ToTimeZone), new ArgumentNullException(nameof(fromTimeZoneInfo))));
			}

			var utc = DateTime.UtcNow;

			var date = new DateTime(utc.Year, utc.Month, utc.Day, @this.Hours, @this.Minutes, @this.Seconds, DateTimeKind.Unspecified);

			return TimeZoneInfo.ConvertTime(date, fromTimeZoneInfo, toTimeZoneInfo);
		}
	}
}
