using System;

namespace Shared.Extensions
{
	public static partial class StringExtensions
	{
		/// <summary>
		/// Removes the 'Controller' suffix from the input string, when available.
		/// </summary>
		/// <param name="this">The extended string.</param>
		/// <returns>The transformed string.</returns>
		public static string RemoveControllerSuffix(this string @this)
		{
			if (string.IsNullOrWhiteSpace(@this))
			{
				return string.Empty;
			}

			return @this.Substring(
				0,
				@this.LastIndexOf("Controller", StringComparison.InvariantCultureIgnoreCase)
			);
		}
	}
}
