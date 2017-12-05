using Serilog.Core;
using Serilog.Events;
using System.Diagnostics;

namespace App.UI.Mvc5.Infrastructure
{
	public class SerilogActivityIdEnricher : ILogEventEnricher
	{
		public const string ActivityIdPropertyName = "ActivityId";

		public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
		{
			var activityId = Trace.CorrelationManager.ActivityId.ToString();

			var activityIdProperty = propertyFactory.CreateProperty(ActivityIdPropertyName, activityId);

			logEvent.AddPropertyIfAbsent(activityIdProperty);
		}
	}
}