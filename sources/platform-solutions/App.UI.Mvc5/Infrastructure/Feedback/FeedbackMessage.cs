using Shared.Extensions;
using System;

namespace App.UI.Mvc5.Infrastructure
{
	public class FeedbackMessage
	{
		/// <summary>
		/// Constructor method.
		/// </summary>
		public FeedbackMessage(FeedbackMessageType type, string content, string title = null)
		{
			if (string.IsNullOrWhiteSpace(content))
			{
				throw new ArgumentNullException(nameof(content));
			}

			Content = content;
			Title = title.WhenNullOrWhiteSpace(type.GetDisplayName());
			Type = type;
		}

		public string Content { get; private set; }

		public string Title { get; private set; }

		public FeedbackMessageType Type { get; private set; }
	}
}