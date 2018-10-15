using System;

namespace Shared.Extensions
{
	public static partial class TimeSpanExtensions
	{
		public static string ToLocalizedLabel(this TimeSpan? @this)
		{
			if (!@this.HasValue)
			{
				return null;
			}

			return @this.Value.ToLocalizedLabel();
		}

		public static string ToLocalizedLabel(this TimeSpan @this)
		{
			var utc = DateTime.UtcNow;

			return new DateTime(utc.Year, utc.Month, utc.Day, @this.Hours, @this.Minutes, @this.Seconds).ToString("t");
		}
	}
}
