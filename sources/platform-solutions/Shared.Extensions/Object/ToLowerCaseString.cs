namespace Shared.Extensions
{
	public static partial class ObjectExtensions
	{
		public static string ToLowerCaseString(this object @this)
		{
			if (@this == null)
			{
				return string.Empty;
			}

			return @this.ToString().ToLower();
		}
	}
}
