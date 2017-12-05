using Serilog.Formatting;
using Serilog.Events;
using System.IO;
using Newtonsoft.Json;

namespace App.UI.Mvc5.Infrastructure
{
	public class SerilogTextFormatter : ITextFormatter
	{
		public void Format(LogEvent logEvent, TextWriter output)
		{
			output.WriteLine();
			output.WriteLine("------------------------------------------------------------------------");
			output.WriteLine($" => {logEvent.RenderMessage()}");
			output.WriteLine();
			output.WriteLine($"Level: {logEvent.Level}");
			output.WriteLine($"Timestamp: {logEvent.Timestamp}");
			output.WriteLine();

			foreach (var prop in logEvent.Properties)
			{
				output.WriteLine($" - {prop.Key}: {prop.Value}");
			}

			if (logEvent.Exception != null)
			{
				output.WriteLine();
				output.WriteLine(logEvent.Exception);
			}
		}
	}
}
