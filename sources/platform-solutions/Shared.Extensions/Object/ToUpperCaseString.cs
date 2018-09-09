namespace Shared.Extensions
{
	public static partial class ObjectExtensions
	{
		public static string ToUpperCaseString(this object @this)
		{
			if (@this == null)
			{
				return string.Empty;
			}

			return @this.ToString().ToUpper();
		}
	}
}
