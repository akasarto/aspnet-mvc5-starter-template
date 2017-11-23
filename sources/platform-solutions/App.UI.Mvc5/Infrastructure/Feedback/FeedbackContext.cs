using System.Collections.Generic;

namespace App.UI.Mvc5.Infrastructure
{
	public class FeedbackContext
	{
		private List<FeedbackMessage> _messages = new List<FeedbackMessage>();

		/// <summary>
		/// Constructor method.
		/// </summary>
		public FeedbackContext()
		{
		}

		public List<FeedbackMessage> Messages => _messages;

		private void AddMessage(FeedbackMessage message)
		{
			_messages.Add(message);
		}

		public void AddMessage(FeedbackMessageType type, string content, string title = null)
		{
			var message = new FeedbackMessage(type, content, title);

			AddMessage(message);
		}
	}
}