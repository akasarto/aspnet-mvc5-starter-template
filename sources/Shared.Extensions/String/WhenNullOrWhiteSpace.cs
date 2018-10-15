namespace Shared.Extensions
{
	public static partial class StringExtensions
	{
		public static string WhenNullOrWhiteSpace(this string @this, string placeholderText)
		{
			if (string.IsNullOrWhiteSpace(@this))
			{
				return placeholderText ?? string.Empty;
			}

			return @this;
		}
	}
}
