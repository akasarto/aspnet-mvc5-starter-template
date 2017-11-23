using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Sarto.Extensions
{
	public static partial class EnumExtensions
	{
		/// <summary>
		/// Gets the decorated <see cref="DisplayAttribute"/> or <see cref="DisplayNameAttribute"/> inforamtion from the <see cref="Enum"/> value.
		/// </summary>
		/// <param name="this">The extended <see cref="Enum"/>.</param>
		/// <returns>The string value from any of the attributes of the <see cref="Enum"/> value as string.</returns>
		public static string GetDisplayName(this Enum @this)
		{
			if (@this == null)
			{
				return string.Empty;
			}

			var attributes = @this.GetType().GetMember(@this.ToString())?.FirstOrDefault()?.GetCustomAttributes();

			if (attributes != null)
			{
				foreach (var attribute in attributes)
				{
					if (attribute is DisplayAttribute)
					{
						return ((DisplayAttribute)attribute).GetName();
					}

					if (attribute is DisplayNameAttribute)
					{
						return ((DisplayNameAttribute)attribute).DisplayName;
					}
				}
			}

			return @this.ToString();
		}
	}
}
