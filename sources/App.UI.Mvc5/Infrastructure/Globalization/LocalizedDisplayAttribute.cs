using System;
using System.ComponentModel;

namespace App.UI.Mvc5.Infrastructure
{
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
	public class LocalizedDisplayNameAttribute : DisplayNameAttribute
	{
		private readonly string _resourceKey = null;

		public LocalizedDisplayNameAttribute(string resourceKey) : base(resourceKey)
		{
			_resourceKey = resourceKey;
		}

		public override string DisplayName
		{
			get
			{
				if (ResourceType != null)
				{
					return GlobalizationManager.GetLocalizedString(_resourceKey, ResourceType);
				}

				return GlobalizationManager.GetLocalizedString(_resourceKey);
			}
		}

		public Type ResourceType { get; set; }
	}
}
