using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace App.UI.Mvc5.Infrastructure
{
	public class GlobalizationService : IGlobalizationService
	{
		static private IEnumerable<CultureInfo> _specificCultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

		/// <summary>
		/// Constructor method.
		/// </summary>
		public GlobalizationService()
		{
		}

		public IEnumerable<CultureInfo> GetCultures()
		{
			var supportedCultures = AppSettings.Globalization.SupportedCultures;

			var cultures = _specificCultures.Where(rc =>
				supportedCultures.Split(",".ToCharArray()
			).Any(
					ac => rc.Name.Equals(ac, StringComparison.InvariantCultureIgnoreCase)
			)).ToList();

			return cultures;
		}

		public IEnumerable<TimeZoneInfo> GetTimeZones()
		{
			return TimeZoneInfo.GetSystemTimeZones();
		}

		public IEnumerable<CultureInfo> GetUICultures()
		{
			var supportedUICultures = AppSettings.Globalization.SupportedUICultures;

			var uiCultures = _specificCultures.Where(rc =>
				supportedUICultures.Split(",".ToCharArray()
			).Any(
					ac => rc.Name.Equals(ac, StringComparison.InvariantCultureIgnoreCase)
			)).ToList();

			return uiCultures;
		}
	}
}
