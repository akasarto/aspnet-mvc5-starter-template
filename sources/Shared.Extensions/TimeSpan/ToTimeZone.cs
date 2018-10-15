using System;

namespace Shared.Extensions
{
	public static partial class TimeSpanExtensions
	{
		public static DateTime ToTimeZone(this TimeSpan @this, string toTimeZoneId)
		{
			var toTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(toTimeZoneId);

			return @this.ToTimeZone(toTimeZoneInfo);
		}

		public static DateTime ToTimeZone(this TimeSpan @this, string toTimeZoneId, string fromTimeZoneId)
		{
			var toTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(toTimeZoneId);
			var fromTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(fromTimeZoneId);

			return @this.ToTimeZone(toTimeZoneInfo, fromTimeZoneInfo);
		}

		public static DateTime ToTimeZone(this TimeSpan @this, TimeZoneInfo toTimeZoneInfo)
		{
			var fromTimeZone = TimeZoneInfo.Utc;

			return @this.ToTimeZone(toTimeZoneInfo, fromTimeZone);
		}

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
