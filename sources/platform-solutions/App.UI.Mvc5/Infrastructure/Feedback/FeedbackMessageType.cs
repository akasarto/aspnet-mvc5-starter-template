namespace App.UI.Mvc5.Infrastructure
{
	public enum FeedbackMessageType : byte
	{
		/// <summary>
		/// #0 - Error color.
		/// </summary>
		[LocalizedDisplayName("MessageType_Error")]
		Error = 0,

		/// <summary>
		/// #1 - Information color.
		/// </summary>
		[LocalizedDisplayName("MessageType_Info")]
		Info = 1,

		/// <summary>
		/// #2 - Success color.
		/// </summary>
		[LocalizedDisplayName("MessageType_Success")]
		Success = 2,

		/// <summary>
		/// #3 - Warning color.
		/// </summary>
		[LocalizedDisplayName("MessageType_Warning")]
		Warning = 3
	}
}