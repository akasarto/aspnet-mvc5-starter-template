using System;
using System.Collections.Generic;
using System.Resources;

namespace App.UI.Mvc5.Infrastructure
{
	public static class GlobalizationManager
	{
		private static readonly Dictionary<Type, ResourceManager> _resourceManagers;

		static GlobalizationManager()
		{
			_resourceManagers = new Dictionary<Type, ResourceManager>();
		}

		public static string GetLocalizedString(string resourceKey, params object[] formatParams)
		{
			return GetLocalizedString<AppResources>(resourceKey, formatParams);
		}

		public static string GetLocalizedString<T>(string resourceKey, params object[] formatParams)
		{
			if (string.IsNullOrWhiteSpace(resourceKey))
			{
				return "[missing-key]";
			}

			var resourceManager = GetResourceManager(typeof(T));

			var result = resourceManager.GetString(resourceKey) ?? $"<<{resourceKey}>>";

			if ((formatParams?.Length ?? 0) > 0)
			{
				result = string.Format(result, formatParams);
			}

			return result;
		}

		private static ResourceManager GetResourceManager(Type type)
		{
			var manager = default(ResourceManager);

			if (!_resourceManagers.TryGetValue(type, out manager))
			{
				manager = new ResourceManager(type);
				_resourceManagers.Add(type, manager);
			}

			return manager;
		}
	}
}
