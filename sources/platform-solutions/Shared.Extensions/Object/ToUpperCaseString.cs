namespace Shared.Extensions
{
	public static partial class ObjectExtensions
	{
		/// <summary>
		/// Calls the .ToString() method of the object and convert the result to upper case.
		/// </summary>
		/// <param name="this">The extended object.</param>
		/// <returns>An upper cased string.</returns>
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
