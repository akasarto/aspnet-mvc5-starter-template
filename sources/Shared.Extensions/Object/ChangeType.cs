using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Shared.Extensions
{
	public static partial class ObjectExtensions
	{
		public static readonly Regex InvalidEnumValueChars = new Regex(@"[^a-zA-Z0-9_]", RegexOptions.Compiled);

		public static T ChangeType<T>(this object @this, T defaultValue = default(T))
		{
			try
			{
				if (@this == null)
				{
					return defaultValue;
				}

				if (@this is T)
				{
					return (T)@this;
				}

				Type conversionType = typeof(T);

				// Nullables
				if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
				{
					conversionType = (new NullableConverter(conversionType)).UnderlyingType;
				}

				if (conversionType.IsEnum)
				{
					var enumStr = @this.ToString();

					// Clean the input string before attempting to convert.
					if (!string.IsNullOrWhiteSpace(enumStr))
					{
						enumStr = InvalidEnumValueChars.Replace(enumStr, string.Empty);
					}

					return (T)Enum.Parse(conversionType, enumStr, ignoreCase: true);
				}

				// String
				if (conversionType.Equals(typeof(string)))
				{
					return (T)((object)Convert.ToString(@this));
				}

				// Guid
				if (conversionType.Equals(typeof(Guid)))
				{
					var input = @this.ToString();

					if (string.IsNullOrWhiteSpace(input))
					{
						return defaultValue;
					}

					Guid output;
					if (Guid.TryParse(input, out output))
					{
						return (T)((object)output);
					}
				}

				// Bool
				if (conversionType.Equals(typeof(bool)))
				{
					return (T)((object)Convert.ToBoolean(@this));
				}

				// Datetime
				if (conversionType.Equals(typeof(DateTime)))
				{
					return (T)((object)DateTime.Parse(@this.ToString(), CultureInfo.CurrentCulture));
				}

				// TimeSpan
				if (conversionType.Equals(typeof(TimeSpan)))
				{
					return (T)((object)TimeSpan.Parse(@this.ToString(), CultureInfo.CurrentCulture));
				}

				// General
				return (T)Convert.ChangeType(@this, conversionType);
			}
			catch
			{
				return defaultValue;
			}
		}
	}
}
