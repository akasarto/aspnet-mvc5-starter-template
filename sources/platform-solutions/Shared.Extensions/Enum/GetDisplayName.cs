using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Shared.Extensions
{
	public static partial class EnumExtensions
	{
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
