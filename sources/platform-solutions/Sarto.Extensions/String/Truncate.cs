namespace Sarto.Extensions
{
	public static partial class StringExtensions
	{
		/// <summary>
		/// Truncate the input string to the specified size.
		/// </summary>
		/// <param name="this">The extended string.</param>
		/// <param name="maxLength">The max length before truncating.</param>
		/// <param name="truncatedSuffix">The trucation indicating suffix.</param>
		/// <returns>The original or truncated string.</returns>
		public static string Truncate(this string @this, int maxLength, string truncatedSuffix = null)
		{
			if (string.IsNullOrEmpty(@this))
			{
				return string.Empty;
			}

			if (string.IsNullOrWhiteSpace(truncatedSuffix))
			{
				truncatedSuffix = string.Empty;
			}

			int inputLen = @this.Length;

			if (inputLen <= maxLength)
			{
				return @this;
			}

			int breakPosition = @this.IndexOf("\n");

			if (breakPosition < 0)
			{
				breakPosition = @this.IndexOf(".");
			}

			if (breakPosition < 0 || breakPosition > maxLength)
			{
				breakPosition = maxLength;
			}

			int suffixSize = truncatedSuffix.Length;

			if (breakPosition < 0)
			{
				return string.Concat(@this.Substring(0, inputLen - suffixSize), truncatedSuffix);
			}

			if (breakPosition > @this.Length)
			{
				breakPosition = @this.Length;
			}

			breakPosition = breakPosition - suffixSize;

			return string.Concat(@this.Substring(0, breakPosition).Trim(), truncatedSuffix);
		}
	}
}
