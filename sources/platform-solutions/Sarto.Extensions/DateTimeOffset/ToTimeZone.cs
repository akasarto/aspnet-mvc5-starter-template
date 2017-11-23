using System;

namespace Sarto.Extensions
{
	public static partial class DateTimeOffsetExtensions
	{
		/// <summary>
		/// Convert to the specified time zone id/name based on the extended <see cref="DateTimeOffset"/> instance information.
		/// </summary>
		/// <param name="this">The extended <see cref="DateTimeOffset"/> instance.</param>
		/// <param name="toTimeZoneId">The time zone id/name to convert to.</param>
		/// <returns>A converted <see cref="DateTimeOffset"/> instance.</returns>
		public static DateTimeOffset ToTimeZone(this DateTimeOffset @this, string toTimeZoneId)
		{
			var toTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(toTimeZoneId);

			return @this.ToTimeZone(toTimeZoneInfo);
		}

		/// <summary>
		/// Convert to the specified time zone instance based on the extended <see cref="DateTimeOffset"/> instance information.
		/// </summary>
		/// <param name="this">The extended <see cref="DateTimeOffset"/> instance.</param>
		/// <param name="toTimeZoneInfo">The time zone instance to convert to.</param>
		/// <returns>A converted <see cref="DateTimeOffset"/> instance.</returns>
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
