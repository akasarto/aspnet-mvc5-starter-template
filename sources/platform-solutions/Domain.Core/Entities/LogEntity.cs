using System;

namespace Domain.Core.Entities
{
	public class LogEntity
	{
		public Guid ActivityId { get; set; }
		public string Exception { get; set; }
		public long Id { get; set; }
		public string Level { get; set; }
		public string Message { get; set; }
		public string Properties { get; set; }
		public DateTime TimeStamp { get; set; }
	}
}
