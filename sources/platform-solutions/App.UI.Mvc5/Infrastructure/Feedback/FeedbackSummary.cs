using Shared.Extensions;
using System.Web.Mvc;

namespace App.UI.Mvc5.Infrastructure
{
	public static partial class WebExtensions
	{
		public static MvcHtmlString FeedbackSummary(this HtmlHelper @this)
		{
			var _feedbackContext = @this.ViewContext?.TempData[nameof(FeedbackContext)] as FeedbackContext ?? new FeedbackContext();

			var wrapperDiv = new TagBuilder("div");

			wrapperDiv.AddCssClass("feedback-messages");

			foreach (var message in _feedbackContext.Messages)
			{
				var messageDiv = new TagBuilder("div");

				messageDiv.MergeAttribute("data-type", message.Type.ToLowerCaseString());
				messageDiv.MergeAttribute("data-content", message.Content);
				messageDiv.MergeAttribute("data-title", message.Title);

				wrapperDiv.InnerHtml += messageDiv;
			}

			return MvcHtmlString.Create(wrapperDiv.ToString());
		}
	}
}