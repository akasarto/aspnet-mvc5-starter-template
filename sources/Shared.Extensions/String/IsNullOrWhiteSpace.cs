namespace Shared.Extensions
{
	public static partial class StringExtensions
	{
		public static bool IsNullOrWhiteSpace(this string @this)
		{
			return string.IsNullOrWhiteSpace(@this);
		}
	}
}
