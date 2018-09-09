using System;
using System.Collections.Generic;
using System.Globalization;

namespace App.UI.Mvc5.Infrastructure
{
	public interface IGlobalizationService
	{
		IEnumerable<CultureInfo> GetCultures();

		IEnumerable<TimeZoneInfo> GetTimeZones();

		IEnumerable<CultureInfo> GetUICultures();
	}
}
