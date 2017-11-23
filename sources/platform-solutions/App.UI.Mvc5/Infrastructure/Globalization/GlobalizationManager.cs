namespace App.UI.Mvc5.Infrastructure
{
	public static class GlobalizationManager
	{
		public static string GetLocalizedString(string resourceKey, params object[] formatParams)
		{
			if (string.IsNullOrWhiteSpace(resourceKey))
			{
				return "[missing-key]";
			}

			var result = AdminResources.ResourceManager.GetString(resourceKey) ?? $"<{resourceKey}>";

			if ((formatParams?.Length ?? 0) > 0)
			{
				result = string.Format(result, formatParams);
			}

			return result;
		}
	}
}
