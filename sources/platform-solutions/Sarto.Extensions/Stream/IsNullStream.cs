using System.IO;

namespace Sarto.Extensions
{
	public static partial class StreamExtensions
	{
		/// <summary>
		/// Checks if the specified <see cref="Stream"/> is null or is a null object value.
		/// </summary>
		/// <param name="this">The extended <see cref="Stream"/> instance.</param>
		/// <returns><c>True</c> if null, otherwise <c>false</c>.</returns>
		public static bool IsNull(this Stream @this)
		{
			if (@this == null)
			{
				return true;
			}

			return @this == Stream.Null;
		}
	}
}
