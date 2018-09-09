using System.IO;

namespace Shared.Extensions
{
	public static partial class StreamExtensions
	{
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
