using System;
using System.Collections.Generic;
using System.Globalization;

namespace App.UI.Mvc5.Infrastructure
{
	public interface IGlobalizationService
	{
		IEnumerable<CultureInfo> GetCultures();

		IEnumerable<CultureInfo> GetUICultures();

		IEnumerable<TimeZoneInfo> GetTimeZones();
	}
}
