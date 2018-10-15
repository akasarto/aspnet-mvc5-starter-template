using System;
using System.Collections.Generic;
using System.Resources;

namespace App.UI.Mvc5.Infrastructure
{
	public static class GlobalizationManager
	{
		private static object _syncRoot = new object();

		private static readonly Dictionary<Type, ResourceManager> _resourceManagers;

		static GlobalizationManager()
		{
			_resourceManagers = new Dictionary<Type, ResourceManager>();
		}

		public static Type DefaultResourceType { get; set; } = typeof(AppResources);

		public static string GetLocalizedString(string resourceKey, params object[] formatParams)
		{
			return GetLocalizedString(resourceKey, null, formatParams);
		}

		public static string GetLocalizedString<TResourceType>(string resourceKey, params object[] formatParams)
		{
			return GetLocalizedString(resourceKey, typeof(TResourceType), formatParams);
		}

		public static string GetLocalizedString(string resourceKey, Type resourceType, params object[] formatParams)
		{
			if (string.IsNullOrWhiteSpace(resourceKey))
			{
				return "[missing-key]";
			}

			if (resourceType == null)
			{
				resourceType = DefaultResourceType;
			}

			var resourceManager = GetResourceManager(resourceType);

			var result = resourceManager.GetString(resourceKey);

			if (result == null)
			{
				resourceManager = GetResourceManager(DefaultResourceType);

				result = resourceManager.GetString(resourceKey) ?? $"_{resourceKey}_";
			}

			if ((formatParams?.Length ?? 0) > 0)
			{
				result = string.Format(result, formatParams);
			}

			return result;
		}

		private static ResourceManager GetResourceManager(Type type)
		{
			var manager = default(ResourceManager);

			lock (_syncRoot)
			{
				if (!_resourceManagers.TryGetValue(type, out manager))
				{
					manager = new ResourceManager(type);
					_resourceManagers.Add(type, manager);
				}
			}

			return manager;
		}
	}
}
