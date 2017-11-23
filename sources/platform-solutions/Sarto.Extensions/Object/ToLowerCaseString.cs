namespace Sarto.Extensions
{
	public static partial class ObjectExtensions
	{
		/// <summary>
		/// Calls the .ToString() method of the object and convert the result to lower case.
		/// </summary>
		/// <param name="this">The extended object.</param>
		/// <returns>A lower cased string.</returns>
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
