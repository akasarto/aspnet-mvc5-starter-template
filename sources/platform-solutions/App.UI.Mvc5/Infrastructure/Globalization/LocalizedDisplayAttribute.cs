using System;
using System.ComponentModel;

namespace App.UI.Mvc5.Infrastructure
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public class LocalizedDisplayNameAttribute : DisplayNameAttribute
	{
		private readonly string _resourceKey = null;

		public LocalizedDisplayNameAttribute(string resourceKey) : base(resourceKey)
		{
			_resourceKey = resourceKey;
		}

		public override string DisplayName => GlobalizationManager.GetLocalizedString(_resourceKey);
	}
}
