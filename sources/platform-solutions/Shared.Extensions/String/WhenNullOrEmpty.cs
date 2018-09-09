namespace Shared.Extensions
{
	public static partial class StringExtensions
	{
		public static string WhenNullOrEmpty(this string @this, string placeholderText)
		{
			if (string.IsNullOrEmpty(@this))
			{
				return placeholderText ?? string.Empty;
			}

			return @this;
		}
	}
}
