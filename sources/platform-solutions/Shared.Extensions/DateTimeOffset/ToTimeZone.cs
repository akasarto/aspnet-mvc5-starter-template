using System;

namespace Shared.Extensions
{
	public static partial class DateTimeOffsetExtensions
	{
		public static DateTimeOffset ToTimeZone(this DateTimeOffset @this, string toTimeZoneId)
		{
			var toTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(toTimeZoneId);

			return @this.ToTimeZone(toTimeZoneInfo);
		}

		public static DateTimeOffset ToTimeZone(this DateTimeOffset @this, TimeZoneInfo toTimeZoneInfo)
		{
			if (toTimeZoneInfo == null)
			{
				throw new Exception(nameof(DateTimeOffsetExtensions), new ArgumentException(nameof(ToTimeZone), new ArgumentNullException(nameof(toTimeZoneInfo))));
			}

			return TimeZoneInfo.ConvertTime(@this, toTimeZoneInfo);
		}
	}
}
