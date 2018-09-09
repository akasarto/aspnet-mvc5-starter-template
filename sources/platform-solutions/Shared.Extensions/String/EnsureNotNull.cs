namespace Shared.Extensions
{
	public static partial class StringExtensions
	{
		public static string EnsureNotNull(this string @this)
		{
			return @this ?? string.Empty;
		}
	}
}
