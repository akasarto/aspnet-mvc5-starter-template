namespace App.UI.Mvc5.Infrastructure
{
	public enum FeedbackMessageType : byte
	{
		/// <summary>
		/// #0 - Error color.
		/// </summary>
		[LocalizedDisplayName("MessageTypeError")]
		Error = 0,

		/// <summary>
		/// #1 - Information color.
		/// </summary>
		[LocalizedDisplayName("MessageTypeInfo")]
		Info = 1,

		/// <summary>
		/// #2 - Success color.
		/// </summary>
		[LocalizedDisplayName("MessageTypeSuccess")]
		Success = 2,

		/// <summary>
		/// #3 - Warning color.
		/// </summary>
		[LocalizedDisplayName("MessageTypeWarning")]
		Warning = 3
	}
}
