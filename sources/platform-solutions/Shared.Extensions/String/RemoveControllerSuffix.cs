using System;

namespace Shared.Extensions
{
	public static partial class StringExtensions
	{
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
