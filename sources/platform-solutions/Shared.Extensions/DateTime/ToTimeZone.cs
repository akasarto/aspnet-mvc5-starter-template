using System;

namespace Shared.Extensions
{
	public static partial class DateTimeExtensions
	{
		public static DateTime ToTimeZone(this DateTime @this, string toTimeZoneId)
		{
			var toTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(toTimeZoneId);

			return @this.ToTimeZone(toTimeZoneInfo);
		}

		public static DateTime ToTimeZone(this DateTime @this, string toTimeZoneId, string fromTimeZoneId)
		{
			var toTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(toTimeZoneId);
			var fromTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(fromTimeZoneId);

			return @this.ToTimeZone(toTimeZoneInfo, fromTimeZoneInfo);
		}

		public static DateTime ToTimeZone(this DateTime @this, TimeZoneInfo toTimeZoneInfo)
		{
			var fromTimeZone = TimeZoneInfo.Utc;

			return @this.ToTimeZone(toTimeZoneInfo, fromTimeZone);
		}

		public static DateTime ToTimeZone(this DateTime @this, TimeZoneInfo toTimeZoneInfo, TimeZoneInfo fromTimeZoneInfo)
		{
			if (toTimeZoneInfo == null)
			{
				throw new Exception(nameof(DateTimeExtensions), new ArgumentException(nameof(ToTimeZone), new ArgumentNullException(nameof(toTimeZoneInfo))));
			}

			if (fromTimeZoneInfo == null)
			{
				throw new Exception(nameof(DateTimeExtensions), new ArgumentException(nameof(ToTimeZone), new ArgumentNullException(nameof(fromTimeZoneInfo))));
			}

			return TimeZoneInfo.ConvertTime(@this, fromTimeZoneInfo, toTimeZoneInfo);
		}
	}
}
